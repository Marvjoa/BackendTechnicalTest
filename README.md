# BackendTechnicalTest

## Instrucciones para ejecutar el proyecto

Paso 1 - Clona el repositorio

git clone https://github.com/Marvjoa/BackendTechnicalTest.git

cd BackendTechnicalTest

Paso 2 - Restaura paquetes

dotnet restore

Paso 3 - Aplica migraciones y crea la base de datos

dotnet ef database update --project BackendTechnicalTest.Infrastructure --startup-project BackendTechnicalTest

Paso 4 - Ejecuta la API

dotnet run --project BackendTechnicalTest

Paso 5 - Accede a Swagger

https://localhost:(use su puerto)/swagger

paso 6 - correr las pruebas unitarias

dotnet test
