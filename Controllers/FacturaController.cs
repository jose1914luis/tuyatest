using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace TestTuya.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        
        public FacturaController (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        [HttpPost("GenerarFactura")]
        public ApiResponse GenerarFactura(Factura factura)
        {
            try{
                using (var db = new TuyaContext(Configuration))
                {
                    //crear primero la factura
                    db.Add(factura);
                    db.SaveChanges();
                    int total= 0;

                    //luego crear la descipcion de la factura                    
                    foreach (DescripcionFactura descripcionFactura in factura.DescripcionFacturas)
                    {
                        //Obtener el producto
                        Producto producto = db.Producto
                        .Where(b => b.ProductoId == descripcionFactura.ProductoId)
                        .FirstOrDefault();

                        descripcionFactura.FacturaId = factura.FacturaId;
                       descripcionFactura.Subtotal = descripcionFactura.Cantidad * producto.Precio;
                       total += descripcionFactura.Subtotal;

                       //db.Add(descripcionFactura);
                    }
                    factura.TotalFactura = total;

                    db.SaveChanges();
                    Console.WriteLine("Factura generada" + factura.FacturaId );
                    
                }
                return new ApiResponse{
                    code= 200,
                    message = "" + factura.FacturaId,
                    type = "Succes"            
                };

            }catch(Exception e){

                return new ApiResponse{
                    code= 201,
                    message = e.Message,
                    type = "Error"            
                };
            }
            
        }
    }
}
