using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestTuya.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturaController : ControllerBase
    {
        
        [HttpPost]
        public int Post(Factura factura)
        {
            using (var db = new TuyaContext())
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
            return factura.FacturaId;
        }
    }
}
