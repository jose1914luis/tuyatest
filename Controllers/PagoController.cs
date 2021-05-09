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

namespace TestTuya.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagoController : ControllerBase
    {
            

        private int facturaId;
        private int pedidoId;
        [HttpPost]
        public string Post(Factura factura)
        {

            GenerarFactura(factura);
            
            return "respuesta";
        }

        public async void GenerarFactura( Factura factura){

            string url = "http://localhost:5000/Factura";
            HttpServices httpServices = new HttpServices();
            await httpServices.CallServices(factura, url);
            factura.FacturaId = int.Parse(httpServices.responseBody);
            
            if(factura.FacturaId >0){
                GenerarPedido(factura);
            }
        }
        
        public async void GenerarPedido( Factura factura){

            string url = "http://localhost:5000/Pedido";
            HttpServices httpServices = new HttpServices();
            await httpServices.CallServices(factura, url);
            this.pedidoId = int.Parse(httpServices.responseBody);
        }
        
    }
}
