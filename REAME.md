API de pagos - Test tecnico Tuya


## Installation BD

LA construccion del modelo de base de datos se hizo a travez de Entety Framework en una maquina Linux, por lo tanto es necesario correr los siguientes comandos en la terminal para que se cree nuestro esquema:

```bash
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Ejecucion

Al ejecutar el programa encontramos dos opciones:
1. Ingresar los datos a la BD y ejecutar el Servidor
2. Ejecutar el servidor

La opcion 1 permite ingresar los datos de prueba a la base de datos y ejecutar el servidor local. Si ya tenemos los datos demo podemos utilizar la opcion 2.

## Documentacion API
Documentacion es visible una vez se ejecuta el servidor en la ruta:

http://localhost:5000/swagger/index.html

Para la ejecucion de nuestro servicio maestro (http://localhost:5000/api/Pago/GenerarPago), podemos utilizar los siguientes datos de prueba:

```json
{
    "FechaFactura": "2019-01-06T17:16:40",
    "ClienteId": 1,
    "DescripcionFacturas": [
        {
            "ProductoId": 1,
            "Cantidad": 2
        },
        {
            "ProductoId": 2,
            "Cantidad": 1
        },
        {
            "ProductoId": 3,
            "Cantidad": 1
        }
    ]
}
```
