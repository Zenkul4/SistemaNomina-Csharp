namespace SistemaNomina
{
    public class EmpleadoAsalariadoPorComision : EmpleadoPorComision
    {
        public decimal SalarioBase { get; set; }

        public EmpleadoAsalariadoPorComision(string nombre, string apellido, string nss, decimal ventas, decimal tarifa, decimal baseSalario)
            : base(nombre, apellido, nss, ventas, tarifa)
        {
            SalarioBase = baseSalario;
        }

        public override decimal CalcularPago()
        {
            
            return base.CalcularPago() + SalarioBase + (SalarioBase * 0.10m);
        }
        public override string ObtenerTipo() => "Asalariado + Com.";

        public override string ToString()
        {
            
            return $"NOMBRE:   {PrimerNombre} {ApellidoPaterno}\n" +
                   $"NSS:      {NumeroSeguroSocial}\n" +
                   $"TIPO:     {ObtenerTipo()}\n" +
                   $"DETALLES: Salario Base:   {SalarioBase:C2}\n" +
                   $"          Ventas:         {VentasBrutas:C2}\n" +
                   $"          Tarifa:         {TarifaComision:P1}";
        }
    }
}