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

            _connectionString = "Server=bd-asistencias.cx6u0ieuw6ab.us-east-1.rds.amazonaws.com,1433;Database=Ausencia;User Id=admin;Password=bd-asistencias;TrustServerCertificate=True;";
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
            //var filteredColumns = columns.Where(c => c.Key.ToLower() != "id").ToDictionary(k => k.Key, v => v.Value);
            var filteredColumns = columns.ToDictionary(k => k.Key, v => v.Value);

            var columnNames = string.Join(", ", filteredColumns.Keys);
            var parameterNames = string.Join(", ", GetParameterNames(filteredColumns.Keys));

            var query = $"INSERT INTO {TableName} ({columnNames})  VALUES ({parameterNames})";
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

        public async Task UpdateAsync(string key)
        {
            var columns = GetColumns();
            var setClause = string.Join(", ", GetSetClause(columns.Keys));

            var query = $"UPDATE {TableName} SET {setClause} WHERE {key} = @{key}";

            var propertyValue = GetType().GetProperty(key)?.GetValue(this);

            if (propertyValue == null)
            {
                throw new ArgumentException($"Property '{key}' not found or its value is null.");
            }

            var parameters = new Dictionary<string, object>
            {
                { $"@{key}", propertyValue }
            };

            await ExecuteNonQueryAsync(query, columns);
        }

        public async Task DeleteAsync()
        {
            var query = $"Update {TableName} set Estatus = 'Inactivo' WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
        {
            { "@Id", GetType().GetProperty("Id").GetValue(this) }
        };

            await ExecuteNonQueryAsync(query, parameters);
        }

        public async Task DeleteNoIdAsync(string key)
        {
            var query = $"UPDATE {TableName} SET Estatus = 'Inactivo' WHERE {key} = @{key}";
            var propertyValue = GetType().GetProperty(key)?.GetValue(this);

            if (propertyValue == null)
            {
                throw new ArgumentException($"Property '{key}' not found or its value is null.");
            }

            var parameters = new Dictionary<string, object>
            {
                { $"@{key}", propertyValue }
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
                catch (SqlException ex) when (ex.Number == 547) // Foreign Key violation error number
                {
                    // Log the exception or handle it accordingly
                    throw new Exception("Foreign key constraint violation. Please ensure that the related record exists.", ex);
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it accordingly
                    throw;
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
                        //var idParameter = new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        //command.Parameters.Add(idParameter);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        //var generatedId = (int)idParameter.Value;
                        return 1;
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
                        {
                            query += where + " and Estatus != 'Inactivo' or Estatus is null";
                        }
                        else
                        {
                            query += "where Estatus != 'Inactivo' or Estatus is null";
                        }
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

        public async Task<TEntity> GetByIdAsync<TEntity>(string id) where TEntity : Entity, new()
        {
            TEntity entity = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"SELECT * FROM {TableName} WHERE Id = @Id and Estatus != 'Inactivo' or Estatus is null";
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
                            if (value is string stringValue)
                            {
                                // Aplicar Trim si el valor es un string
                                property.SetValue(this, stringValue.Trim());
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
}
