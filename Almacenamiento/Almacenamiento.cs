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

        public static void guardarUnaPartida(Partida miPartida)
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
    }
}