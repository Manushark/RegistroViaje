using CapaAccesoDatos;
using CapaDatos;
using System.Collections.Generic;

namespace CapaLogicaNegocio
{
    public class ServicioViajes
    {
        private readonly RepositorioViajes _repositorio;

        public ServicioViajes(RepositorioViajes repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Viaje> ObtenerViajes()
        {
            return _repositorio.ObtenerViajes();
        }

        public void AgregarViaje(Viaje viaje)
        {
            _repositorio.AgregarViaje(viaje);
        }

        internal object ObtenerViajePorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
