namespace SistemaNomina
{
    public class EmpleadoPorHoras : Empleado
    {
        public decimal SueldoPorHora { get; set; }
        public decimal HorasTrabajadas { get; set; }

        public EmpleadoPorHoras(string nombre, string apellido, string nss, decimal sueldo, decimal horas)
            : base(nombre, apellido, nss)
        {
            SueldoPorHora = sueldo;
            HorasTrabajadas = horas;
        }

        public override decimal CalcularPago()
        {
            if (HorasTrabajadas <= 40)
                return SueldoPorHora * HorasTrabajadas;
            else
                
                return (SueldoPorHora * 40) + (SueldoPorHora * 1.5m * (HorasTrabajadas - 40));
        }
        public override string ObtenerTipo() => "Por Horas";

        public override string ToString()
        {
            return base.ToString() +
                   $"\nDETALLES: Precio/Hora: {SueldoPorHora:C2}\n" +
                   $"          Horas Trab.: {HorasTrabajadas}";
        }
    }
}