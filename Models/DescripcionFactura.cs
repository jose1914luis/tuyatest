namespace TestTuya
{
    public class DescripcionFactura{

        public int DescripcionFacturaId {get;set;}
        public int  ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int  FacturaId { get; set; }
        public Factura Factura { get; set; }
        public int Cantidad { get; set; }
        public int Subtotal { get; set; }
    }
}