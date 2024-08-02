# asistencias-cenfotec
PSWE-03 | Gestión de asistencias

## Front-End
### Levantar
`npm run serve`

### Preparar Build
`npm run build`

### Push a S3
`npm run deploy`

### URL
http://asistencias-cenfotec.s3-website-us-east-1.amazonaws.com

## Back-End
### Levantar (Docker)
`cd asistencias.API`
`docker build -t asistencia-api .`

### Correr
`docker run -d -p 8080:8080 -p 8081:8081 --name asistencia-api-container asistencia-api`

### Deploy
`zip -r asistencia-api.zip .`
- Subir el zip a ElasticBeanstalk

## URL
http://asistencias-api.us-east-1.elasticbeanstalk.com/api

### Probar con Postman
```bash
curl --location 'http://asistencias-api.us-east-1.elasticbeanstalk.com/api/administrativo/get'
```
Deberia devolver un 401.