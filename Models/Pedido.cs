using System;
using System.ComponentModel.DataAnnotations;

namespace TestTuya
{
    public class Pedido{

        public int PedidoId { get; set; }
        
        public int FacturaId { get; set; }
        public Factura Factura {get; set;}
                
        public DateTime FechaEntrega { get; set; }
    }
}