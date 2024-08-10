using System;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class ConexionBD
    {
        private string connectionString;

        public ConexionBD(string connectionString)
        {
            this.connectionString = connectionString;
        }

    
        public SqlConnection ObtenerConexion()
        {
            try
            {
                var conexion = new SqlConnection(connectionString);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
                throw;
            }
        }

     
        public void EjecutarComando(string query, SqlParameter[] parametros = null)
        {
            using (var conexion = ObtenerConexion())
            {
                using (var comando = new SqlCommand(query, conexion))
                {
                    if (parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }
                    comando.ExecuteNonQuery();
                }
            }
        }

        
        public object EjecutarScalar(string query, SqlParameter[] parametros = null)
        {
            using (var conexion = ObtenerConexion())
            {
                using (var comando = new SqlCommand(query, conexion))
                {
                    if (parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }
                    return comando.ExecuteScalar();
                }
            }
        }
    }
}
