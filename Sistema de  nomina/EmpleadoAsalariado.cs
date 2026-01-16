namespace SistemaNomina
{
    public class EmpleadoAsalariado : Empleado
    {
        public decimal SalarioSemanal { get; set; }

        public EmpleadoAsalariado(string nombre, string apellido, string nss, decimal salario)
            : base(nombre, apellido, nss)
        {
            SalarioSemanal = salario;
        }

        public override decimal CalcularPago() => SalarioSemanal;
        public override string ObtenerTipo() => "Asalariado";

        public override string ToString()
        {
            return base.ToString() + $"\nDETALLES: Salario Fijo Semanal: {SalarioSemanal:C2}";
        }
    }
}