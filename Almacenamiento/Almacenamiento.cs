using System.Text.Json;
using PersonajeClass;
using PartidaClass;
using System;
using System.IO; // Necesario para Directory y File


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
                Console.WriteLine($"Partida guardada exitosamente en: {rutaCompletaArchivoPartida}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la partida: {ex.Message}");
            }
        }

        /*public static Partida CargarUnaPartida(string nombrePartida)
        {
            //logica para traer la partida
            Partida partidaCargada = new Partida();
            return partidaCargada;
        }*/
        public static void MostrarPartidasGuardadas()
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
    }
}