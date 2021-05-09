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
    public class PedidoController : ControllerBase
    {
        
        [HttpPost]
        public int Post(Factura factura)
        {
            Pedido pedido = new Pedido();
            using (var db = new TuyaContext())
                {
                    
                    pedido.FacturaId = factura.FacturaId;
                    //pedido.Clienteid = factura.ClienteId;
                    //la fecha de entrega serian  dos dias despues de la compra
                    pedido.FechaEntrega = factura.FechaFactura.AddDays(2);
                    //crear primero la factura
                    db.Add(pedido);
                    db.SaveChanges();

                    
                }
            return pedido.PedidoId;
        }
    }
}
