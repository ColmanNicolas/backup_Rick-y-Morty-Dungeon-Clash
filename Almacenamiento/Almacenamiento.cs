using System.Text.Json;
using PersonajeClass;
using PartidaClass;
using System;
using System.IO;
using UIUXclass; // Necesario para Directory y File


namespace AlmacenamientoClass
{
    public static class Almacenamiento
    {
        private const string NombreCarpetaPartidas = "partidasGuardadas";

        public static void GuardarUnaPartida(Partida miPartida)
        {
            string jsonString = JsonSerializer.Serialize(miPartida, new JsonSerializerOptions { WriteIndented = true });
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompletaDirectorioPartidas = Path.Combine(rutaBase, NombreCarpetaPartidas);

            if (!Directory.Exists(rutaCompletaDirectorioPartidas))
            {
                Console.WriteLine($"El directorio '{NombreCarpetaPartidas}' no existe. Creándolo...");
                Directory.CreateDirectory(rutaCompletaDirectorioPartidas);
                Console.WriteLine("Directorio creado exitosamente.");
            }

            string nombreArchivo = $"{miPartida.NombreJugador}.json";
            string rutaCompletaArchivoPartida = Path.Combine(rutaCompletaDirectorioPartidas, nombreArchivo);

            try
            {
                File.WriteAllText(rutaCompletaArchivoPartida, jsonString);
                Console.WriteLine("\n                  ..::[ INFORME DE OPERACION ]::..");
                Console.WriteLine(" =====================================================================");

                Console.WriteLine("/---------------------------------------------------------------------\\");
                Console.WriteLine("|                                                                     |");
                Console.WriteLine($"| Jugador: {miPartida.NombreJugador,-31}                            |");
                Console.WriteLine("|                                                                     |");
                Console.WriteLine("| Resultado: Partida de guardada con éxito                            |");
                Console.WriteLine("|                                                                     |");
                Console.WriteLine("\\---------------------------------------------------------------------/");
                Console.WriteLine("                     [ TRANSMISION FINALIZADA ]\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la partida: {ex.Message}");
            }
        }

        private static void MostrarPartidasGuardadas()
        {
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaAlmacenamiento = Path.Combine(rutaBase, "partidasGuardadas");

            if (!File.Exists(rutaAlmacenamiento))
            {
                string[] rutasArchivos = Directory.GetFiles(rutaAlmacenamiento);
                Console.Clear();


                foreach (var unaRuta in rutasArchivos)
                {
                    string jsonString = File.ReadAllText(unaRuta);
                    Partida unaPartida = JsonSerializer.Deserialize<Partida>(jsonString);

                    if (unaPartida != null)
                    {
                        double avancePartida;
                        Console.WriteLine(new string('=', 200));
                        Console.WriteLine($"Partida de Jugador: <{unaPartida.NombreJugador}>");
                        Console.Write("Personaje: ");
                        unaPartida.PersonajeJugador.MostrarUnPersonajeSencillo();
                        Console.WriteLine("Avance:  **  50%  **");
                        Console.WriteLine(new string('=', 180));

                    }

                    else Console.WriteLine("El archivo de la partida guardada esta corrompido. ");

                }
            }

            else Console.WriteLine("No existe la carpeta partidasGuardadas. Cree una nueva partida para poder empezar a jugar");

        }
        private static Partida BuscarUnaPartida(string nombreBuscado)
        {
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaAlmacenamiento = Path.Combine(rutaBase, "partidasGuardadas");

            try
            {
                if (Directory.Exists(rutaAlmacenamiento))
                {
                    string[] rutasArchivos = Directory.GetFiles(rutaAlmacenamiento);

                    if (rutasArchivos.Length == 0)
                    {
                        Console.WriteLine($"No hay partidas guardadas en '{rutaAlmacenamiento}'.");
                        return null;
                    }

                    foreach (var unaRuta in rutasArchivos)
                    {
                        Partida unaPartida = null;

                        try
                        {
                            string jsonString = File.ReadAllText(unaRuta);
                            unaPartida = JsonSerializer.Deserialize<Partida>(jsonString);

                            if (unaPartida != null)
                            {
                                if (string.Equals(unaPartida.NombreJugador, nombreBuscado))
                                {
                                    Console.WriteLine($"¡Partida '{nombreBuscado}' encontrada!");
                                    return unaPartida;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Advertencia: El archivo '{unaRuta}' no contiene una partida válida.");
                            }
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"Error de formato JSON al leer '{unaRuta}': {ex.Message}");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Error de E/S al leer '{unaRuta}': {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ocurrió un error inesperado al procesar '{unaRuta}': {ex.Message}");
                        }
                    }

                    Console.WriteLine($"No se encontró la partida con el nombre '{nombreBuscado}'. Intente nuevamente...");
                    return null;
                }
                else
                {

                    Console.WriteLine($"No existe la carpeta '{rutaAlmacenamiento}'. Cree una nueva partida para poder empezar a jugar.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ocurrió un error general al buscar partidas: {ex.Message}");
                return null;
            }
        }
        public static Partida CargarUnaPartidaGuardada(Partida partidaActual)
        {
            string nombrePartida;

            MostrarPartidasGuardadas();

            nombrePartida = UIUX.ElegirNombreJugador("\nIngrese el nombre de la partida: ");  // reutilizo esta funcion porque comparten las mismas restricciones

            partidaActual = BuscarUnaPartida(nombrePartida.Trim());

            return partidaActual;
        }
    }
}