using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace TestTuya.Controllers
{
    /// <summary>
    /// Clase controlador del pedido
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        /// <summary>
        /// permite generar un pedido
        /// </summary>
        /// <returns>Retorna un objecto tipo ApiResponse</returns>
        /// <response code="200">ApiResponse.code = 200. Pedido generada. message = PedidoId </response>
        /// <response code="201">ApiResponse.code = 201. Error. message = Mensaje del error</response>        
        [HttpPost("GenerarPedido")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]      
        public ApiResponse GenerarPedido(Factura factura)
        {
            try{
                
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
                return new ApiResponse{
                    code= 200,
                    message = ""+pedido.PedidoId,
                    type = "Pedido Generado"            
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
