using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TestTuya.Controllers
{   /// <summary>
    /// Clase controladora de pago.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class  PagoController: ControllerBase
    {
            

        private ApiResponse apiResponse;


        public PagoController (IConfiguration configuration)
        {
            Configuration = configuration;
            apiResponse = new ApiResponse();
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// permite generar una factura y posteriormente un pedido. En su conjunto procesan un pago
        /// </summary>
        /// <returns>Retorna un objecto tipo ApiResponse</returns>
        /// <response code="200">ApiResponse.code = 200. message = Pago generado y procesado </response>
        /// <response code="201">ApiResponse.code = 201. Error. message = Mensaje del error</response>        
        
        [HttpPost("GenerarPago")]
        public ActionResult<ApiResponse> GenerarPago(Factura factura)
        {

            GenerarFactura(factura).Wait();
            if(this.apiResponse.code == 200){
                GenerarPedido(factura).Wait();
                if(this.apiResponse.code == 200){
                    return new ApiResponse{
                        code= 200,
                        message = "Pago Generado y procesado",
                        type = "Succes"            
                    };
                }
            }
            
            return this.apiResponse;
            
        }

        /// <summary>
        /// permite generar una factura desde el servicio de pagos
        /// </summary>
        /// <returns>Retorna un objecto tipo ApiResponse</returns>
        /// <response code="200">ApiResponse.code = 200. Factura generada. message = FacturaId </response>
        /// <response code="201">ApiResponse.code = 201. Error. message = Mensaje del error</response>
       
        [HttpPost("GenerarFactura")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]  
        public async Task GenerarFactura( Factura factura){
            
            HttpServices httpServices = new HttpServices();
            this.apiResponse  = await httpServices.CallServices(factura, Configuration.GetValue<string>(
                "AppServicesSettings:serviceFacturaURL"));
             if(this.apiResponse.code == 200){
                 factura.FacturaId = int.Parse(this.apiResponse.message);
             }            

        }

         
        /// <summary>
        /// permite generar un pedido desde el servicio de pagos
        /// </summary>
        /// <returns>Retorna un objecto tipo ApiResponse</returns>
        /// <response code="200">ApiResponse.code = 200. Pedido generada. message = PedidoId </response>
        /// <response code="201">ApiResponse.code = 201. Error. message = Mensaje del error</response>        
        
        [HttpPost("GenerarPedido")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]  
        public async Task GenerarPedido( Factura factura){

            HttpServices httpServices = new HttpServices();
            this.apiResponse = await httpServices.CallServices(factura, Configuration.GetValue<string>(
                "AppServicesSettings:ServicePedidoURL"));                         
        }
        
    }
}
