using CapaDatos;
using System.Collections.Generic;

namespace CapaAccesoDatos
{
    public class RepositorioViajes
    {
        private List<Viaje> viajes;
        private int siguienteId;

        public RepositorioViajes()
        {
            viajes = new List<Viaje>();
            siguienteId = 1; 
        }

        public void AgregarViaje(Viaje viaje)
        {
            viaje.Id = siguienteId++;
            viajes.Add(viaje);
        }

        public List<Viaje> ObtenerViajes()
        {
            return viajes;
        }

        public Viaje ObtenerViajePorId(int id)
        {
            return viajes.Find(v => v.Id == id);
        }
    }
}
