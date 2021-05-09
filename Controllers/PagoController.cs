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
{
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

        [HttpPost("GenerarPago")]
        public ActionResult<ApiResponse> GenerarPago(Factura factura)
        {

            GenerarFactura(factura).Wait();
            if(this.apiResponse.code == 200){
                GenerarPedido(factura).Wait();
                if(this.apiResponse.code == 200){
                    return new ApiResponse{
                        code= 200,
                        message = "Factura Generada",
                        type = "Succes"            
                    };
                }
            }
            
            return this.apiResponse;
            
        }

        [HttpPost("GenerarFactura")]
        public async Task GenerarFactura( Factura factura){
            
            HttpServices httpServices = new HttpServices();
            this.apiResponse  = await httpServices.CallServices(factura, Configuration.GetValue<string>(
                "AppServicesSettings:serviceFacturaURL"));
                            

        }
        [HttpPost("GenerarPedido")]
        public async Task GenerarPedido( Factura factura){

            HttpServices httpServices = new HttpServices();
            this.apiResponse = await httpServices.CallServices(factura, Configuration.GetValue<string>(
                "AppServicesSettings:ServicePedidoURL"));                         
        }
        
    }
}
