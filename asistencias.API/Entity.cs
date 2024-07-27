using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ausencias.API
{
    public abstract class Entity
    {
        private static IConfiguration _configuration;

        public abstract string TableName { get; }

        private readonly string _connectionString;

        protected Entity(string connectionStringName)
        {
            _configuration = new ConfigurationBuilder()
                //.AddEnvironmentVariablesFromFile(Path.Combine(Directory.GetCurrentDirectory(), ".env"))
                .AddEnvironmentVariables()
                .Build();

            _connectionString = "Server=leo;Database=Ausencias;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        }

        protected Dictionary<string, object> GetColumns()
        {
            var columns = new Dictionary<string, object>();

            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null)
                {
                    var columnName = columnAttribute.Name;
                    var value = property.GetValue(this);
                    columns.Add(columnName, value);
                }
            }

            return columns;
        }

        public async Task<int> InsertAsync()
        {
            var columns = GetColumns();
            var filteredColumns = columns.Where(c => c.Key.ToLower() != "id").ToDictionary(k => k.Key, v => v.Value);
            var columnNames = string.Join(", ", filteredColumns.Keys);
            var parameterNames = string.Join(", ", GetParameterNames(filteredColumns.Keys));

            var query = $"INSERT INTO {TableName} ({columnNames}) OUTPUT INSERTED.ID VALUES ({parameterNames})";
            try
            {
                var newId = await ExecuteScalarAsync(query, filteredColumns);
                return (int)newId;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al insertar en la base de datos.", ex);
            }
        }

        public async Task UpdateAsync()
        {
            var columns = GetColumns();
            var setClause = string.Join(", ", GetSetClause(columns.Keys));

            var query = $"UPDATE {TableName} SET {setClause} WHERE Id = @Id";

            columns.Add("@Id", GetType().GetProperty("Id").GetValue(this));
            await ExecuteNonQueryAsync(query, columns);
        }

        public async Task DeleteAsync()
        {
            var query = $"DELETE FROM {TableName} WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
        {
            { "@Id", GetType().GetProperty("Id").GetValue(this) }
        };

            await ExecuteNonQueryAsync(query, parameters);
        }

        private async Task ExecuteNonQueryAsync(string query, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                try
                {
                    foreach (var param in parameters)
                    {
                        var value = param.Value;
                        if (value is bool boolValue)
                        {
                            value = boolValue ? 1 : 0;
                        }

                        command.Parameters.Add(new SqlParameter(param.Key, value ?? DBNull.Value));
                    }

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    var hola = ex.InnerException;
                }
            }
        }

        private async Task<object> ExecuteScalarAsync(string query, IDictionary<string, object> parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            var value = parameter.Value;
                            if (value is bool boolValue)
                            {
                                value = boolValue ? 1 : 0;
                            }
                            command.Parameters.Add(new SqlParameter(parameter.Key, value ?? DBNull.Value));
                        }
                        var idParameter = new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(idParameter);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        var generatedId = (int)idParameter.Value;
                        return generatedId;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private IEnumerable<string> GetParameterNames(IEnumerable<string> columnNames)
        {
            foreach (var columnName in columnNames)
            {
                yield return "@" + columnName;
            }
        }

        private IEnumerable<string> GetSetClause(IEnumerable<string> columnNames)
        {
            foreach (var columnName in columnNames)
            {
                yield return $"{columnName} = @{columnName}";
            }
        }

        public async Task<List<TEntity>> GetAllAsync<TEntity>(string where = null, string query = null) where TEntity : Entity, new()
        {
            var entities = new List<TEntity>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    if (String.IsNullOrEmpty(query))
                    {
                        query = $"SELECT * FROM {TableName} ";

                        if (!String.IsNullOrEmpty(where))
                            query += where;
                    }

                    // Log the query to debug
                    Console.WriteLine($"Query: {query}");

                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var entity = new TEntity();
                                entity.LoadFromReader(reader);
                                entities.Add(entity);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            return entities;
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : Entity, new()
        {
            TEntity entity = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"SELECT * FROM {TableName} WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            entity = new TEntity();
                            entity.LoadFromReader(reader);
                        }
                    }
                }
            }

            return entity;
        }

        protected void LoadFromReader(SqlDataReader reader)
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null)
                {
                    var columnName = columnAttribute.Name;
                    if (reader[columnName] != DBNull.Value)
                    {
                        var value = reader[columnName];
                        if (property.PropertyType == typeof(bool))
                        {
                            property.SetValue(this, Convert.ToInt32(value) == 1);
                        }
                        else if (property.PropertyType == typeof(int) && value is decimal decimalIntValue)
                        {
                            property.SetValue(this, Convert.ToInt32(decimalIntValue));
                        }
                        else if (property.PropertyType == typeof(DateTime) && value is string stringDateValue)
                        {
                            if (DateTime.TryParse(stringDateValue, out DateTime dateValue))
                            {
                                property.SetValue(this, dateValue);
                            }
                            else
                            {
                                throw new ArgumentException($"Cannot convert string '{stringDateValue}' to DateTime for property '{property.Name}'.");
                            }
                        }
                        else if (property.PropertyType == typeof(DateTime) && value is DateTime dateTimeValue)
                        {
                            property.SetValue(this, dateTimeValue);
                        }
                        else
                        {
                            property.SetValue(this, value);
                        }
                    }
                }
            }
        }
    }
}
