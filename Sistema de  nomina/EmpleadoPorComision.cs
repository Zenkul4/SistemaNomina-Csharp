namespace SistemaNomina
{
    public class EmpleadoPorComision : Empleado
    {
        public decimal VentasBrutas { get; set; }
        public decimal TarifaComision { get; set; }

        public EmpleadoPorComision(string nombre, string apellido, string nss, decimal ventas, decimal tarifa)
            : base(nombre, apellido, nss)
        {
            VentasBrutas = ventas;
            TarifaComision = tarifa;
        }

        public override decimal CalcularPago() => VentasBrutas * TarifaComision;
        public override string ObtenerTipo() => "Por Comisión";

        public override string ToString()
        {
            return base.ToString() +
                   $"\nDETALLES: Ventas Totales: {VentasBrutas:C2}\n" +
                   $"          Tarifa Comis: {TarifaComision:P1}";
        }
    }
}