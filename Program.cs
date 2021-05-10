using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;using System.Linq.Expressions;

namespace TestTuya
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //
            //Console.WriteLine();
            Console.WriteLine("Por favor ingresa una opcion:");
            Console.WriteLine("1. Ingresar los datos a la BD y ejecutar el Servidor");
            Console.WriteLine("2. Ejecutar el servidor");
            var opcion = Console.ReadLine();
            if(opcion.Equals("1")){

                using (var db = new TuyaContext())
                {
                    // Insertando valores demo
                    
                    Console.WriteLine("insertando productos");
                    db.Add(new Producto { Nombre="Reloj", Precio = 100000});
                    db.Add(new Producto { Nombre="Audifonos", Precio = 30000});
                    db.Add(new Producto { Nombre="Bascula", Precio = 30000});
                    db.Add(new Producto { Nombre="Tripode", Precio = 60000});
                    db.Add(new Producto { Nombre="Espejo", Precio = 30000});
                    db.SaveChanges();


                    Console.WriteLine("insertando Clientes");
                    db.Add(new Cliente { Nombre="Jose Luis Garcia", Direccion = "Cra 56 # 72 - 55", Telefono = "3197614377"});
                    db.Add(new Cliente { Nombre="Marcela Molina", Direccion = "Cra 72 # 48 - 02", Telefono = "3197614377"});
                    db.SaveChanges();
                }

                CreateHostBuilder(args).Build().Run();     

            }else if(opcion.Equals("2")){

                CreateHostBuilder(args).Build().Run();                
            }            
            
        }

        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
