namespace SistemaNomina
{
    public abstract class Empleado
    {
        public string PrimerNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string NumeroSeguroSocial { get; set; }

        public Empleado(string nombre, string apellido, string nss)
        {
            PrimerNombre = nombre;
            ApellidoPaterno = apellido;
            NumeroSeguroSocial = nss;
        }

        public abstract decimal CalcularPago();
        public abstract string ObtenerTipo();

        public override string ToString()
        {
            return $"NOMBRE:   {PrimerNombre} {ApellidoPaterno}\n" +
                   $"NSS:      {NumeroSeguroSocial}\n" +
                   $"TIPO:     {ObtenerTipo()}";
        }
    }
}