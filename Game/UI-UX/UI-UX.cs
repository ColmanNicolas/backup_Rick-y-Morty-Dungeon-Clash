using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using PersonajeClass;

namespace UIclass
{
    public static class UI
    {
        public static void MenuInicialUI()
        {
            Console.Clear();
            Console.WriteLine("BIEVENIDO A RICK Y MORTY DUNGEON CLASH\n");
            Console.WriteLine("[1] Para iniciar una partida nueva");
            Console.WriteLine("[2] Para cargar una partida");
            Console.WriteLine("[3] Para borrar una partida");
            Console.WriteLine("[0] Para salir");
            Console.Write("\nSu opcion: ");
        }
        public static void ElegirNuevoPersonajeUI()
        {
            Console.WriteLine("\nPor favor, seleccione una de las siguientes opciones para la selección de personajes:");
            Console.WriteLine("[1] Selección manual de un personaje de la lista completa (un total de 812 personajes disponibles).");
            Console.WriteLine("[2] Selección manual de un personaje dentro de una partida personalizada (con un máximo de 512 personajes).");
            Console.WriteLine("[3] Generación aleatoria de un personaje dentro de una partida personalizada (con un máximo de 512 personajes).");
            Console.Write("\nIngrese su opción: ");
        }
        //public static Personaje

        public static void MenuPrincipalUI(string? nombreJugador, int rivalesRestantes)
        {
            Console.Clear();
            Console.WriteLine($"Jugador: {nombreJugador}        ▸ Rivales restantes: {rivalesRestantes}");
            Console.WriteLine("──────────────────────────────────────────────");
            Console.WriteLine("[1] Ver mi personaje");
            Console.WriteLine("[2] Consultar tabla de ventajas por especie");
            Console.WriteLine("[3] Iniciar próximo combate");
            Console.WriteLine("[4] Ver historial de batallas de este turno");
            Console.WriteLine("[5] Simular todos los duelos restantes");
            Console.WriteLine("[6] Ver imagen de un personaje");
            Console.WriteLine("[7] Guardar Partida actual");
            Console.WriteLine("[0] Salir del juego");
            Console.Write("\nSu opcion: ");

        }

    }
}