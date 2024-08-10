using System;
using System.Collections.Generic;
using CapaDatos;
using CapaLogicaNegocio;
using CapaAccesoDatos;


namespace CapaPresentacion
{
    class Program
    {
        static ServicioViajes servicio;

        static void Main(string[] args)
        {
            var repositorio = new RepositorioViajes();
            servicio = new ServicioViajes(repositorio);

            ConfigurarColores();

            while (true)
            {
                MostrarMenu();
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarViaje();
                        break;

                    case "2":
                        MostrarViajes(servicio.ObtenerViajes());
                        break;

                    case "3":
                        BuscarViajePorId();
                        break;

                    case "4":
                        EliminarViaje();
                        break;

                    case "5":
                        ActualizarViaje();
                        break;

                    case "6":
                        return;

                    default:
                        MostrarError("Opción inválida. Por favor, intente de nuevo.");
                        break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("       Registro de Viajes");
            Console.WriteLine("=====================================");
            Console.WriteLine("1. Agregar Viaje");
            Console.WriteLine("2. Ver Viajes");
            Console.WriteLine("3. Buscar Viaje por ID");
            Console.WriteLine("4. Eliminar Viaje");
            Console.WriteLine("5. Actualizar Información de un Viaje");
            Console.WriteLine("6. Salir");
            Console.WriteLine("=====================================");
            Console.Write("Seleccione una opción: ");
        }

        static void MostrarViajes(List<Viaje> viajes)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("       Lista de Viajes");
            Console.WriteLine("=====================================");
            Console.WriteLine("{0,-5} {1,-20} {2,-12} {3,10}", "ID", "Destino", "Fecha", "Costo");
            Console.WriteLine("=====================================");
            foreach (var viaje in viajes)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-12} {3,10:C}", viaje.Id, viaje.Destino, viaje.Fecha.ToShortDateString(), viaje.Costo);
            }
            Console.WriteLine("=====================================");
            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void AgregarViaje()
        {
            Console.Clear();
            MostrarSeparador();
            Console.WriteLine("Agregar un nuevo viaje");
            Console.WriteLine("Por favor, introduzca la siguiente información:");

            string destino;
            DateTime fecha;
            double costo;

            // Validar destino
            do
            {
                Console.Write("Destino: ");
                destino = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(destino));

            // Validar fecha
            do
            {
                Console.Write("Fecha (yyyy-MM-dd): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out fecha));

            // Validar costo
            do
            {
                Console.Write("Costo: ");
            } while (!double.TryParse(Console.ReadLine(), out costo));

            var viaje = new Viaje { Destino = destino, Fecha = fecha, Costo = costo };
            servicio.AgregarViaje(viaje);

            MostrarMensaje("Viaje agregado exitosamente.");
            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void BuscarViajePorId()
        {
            Console.Clear();
            MostrarSeparador();
            Console.WriteLine("Buscar Viaje por ID");

            int id;
            do
            {
                Console.WriteLine("Introduce el ID del viaje:");
            } while (!int.TryParse(Console.ReadLine(), out id));

            var viaje = servicio.ObtenerViajes().Find(v => v.Id == id);

            if (viaje != null)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("       Información del Viaje");
                Console.WriteLine("=====================================");
                Console.WriteLine($"ID: {viaje.Id}");
                Console.WriteLine($"Destino: {viaje.Destino}");
                Console.WriteLine($"Fecha: {viaje.Fecha.ToShortDateString()}");
                Console.WriteLine($"Costo: {viaje.Costo:C}");
                Console.WriteLine("=====================================");
            }
            else
            {
                MostrarMensaje("No se encontró ningún viaje con ese ID.");
            }

            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void EliminarViaje()
        {
            Console.Clear();
            MostrarSeparador();
            Console.WriteLine("Eliminar Viaje");

            int id;

            do
            {
                Console.WriteLine("Introduce el ID del viaje a eliminar:");
            } while (!int.TryParse(Console.ReadLine(), out id) || !servicio.ObtenerViajes().Exists(v => v.Id == id));

            var viaje = servicio.ObtenerViajes().Find(v => v.Id == id);
            servicio.ObtenerViajes().Remove(viaje);

            MostrarMensaje("Viaje eliminado exitosamente.");
            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void ActualizarViaje()
        {
            Console.Clear();
            MostrarSeparador();
            Console.WriteLine("Actualizar Información de un Viaje");

            int id;

            do
            {
                Console.WriteLine("Introduce el ID del viaje a actualizar:");
            } while (!int.TryParse(Console.ReadLine(), out id) || !servicio.ObtenerViajes().Exists(v => v.Id == id));

            var viaje = servicio.ObtenerViajes().Find(v => v.Id == id);

            string destino;
            DateTime nuevaFecha;
            double nuevoCosto;

            Console.Write("Nuevo Destino (deje en blanco para no cambiar): ");
            destino = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(destino))
            {
                viaje.Destino = destino;
            }

            Console.Write("Nueva Fecha (yyyy-MM-dd) (deje en blanco para no cambiar): ");
            if (DateTime.TryParse(Console.ReadLine(), out nuevaFecha))
            {
                viaje.Fecha = nuevaFecha;
            }

            Console.Write("Nuevo Costo (deje en blanco para no cambiar): ");
            if (double.TryParse(Console.ReadLine(), out nuevoCosto))
            {
                viaje.Costo = nuevoCosto;
            }

            MostrarMensaje("Información del viaje actualizada exitosamente.");
            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void MostrarSeparador()
        {
            Console.WriteLine(new string('=', 40));
        }

        static void ConfigurarColores()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        static void MostrarMensaje(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
    }

}


