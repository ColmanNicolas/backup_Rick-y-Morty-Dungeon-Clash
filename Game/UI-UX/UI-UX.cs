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
        }
        public static void ElegirNuevoPersonajeUI()
        {
            Console.WriteLine("\nPor favor, seleccione una de las siguientes opciones para la selección de personajes:\n");
            Console.WriteLine("[1] Selección manual de un personaje de la lista completa (un total de 812 personajes disponibles).");
            Console.WriteLine("[2] Selección manual de un personaje dentro de una partida personalizada (con un máximo de 512 personajes).");
            Console.WriteLine("[3] Generación aleatoria de un personaje dentro de una partida personalizada (con un máximo de 512 personajes).");

        }
        //public static Personaje
        public static string? ElegirNombreJugador()
        {
            string? nombre;
            bool nombreValido = false;

            do
            {
                Console.Write("\nIngrese su nombre dentro del juego (4 a 30 caracteres, solo letras y números): ");
                nombre = Console.ReadLine();

                if (!string.IsNullOrEmpty(nombre) &&
                    nombre.Length >= 4 && nombre.Length <= 30 &&
                    Regex.IsMatch(nombre, @"^[a-zA-Z0-9]+$"))
                {
                    nombreValido = true;
                }
                else
                {
                    Console.WriteLine("Nombre inválido. Debe tener entre 4 y 30 caracteres y solo contener letras y números. ");
                }

            } while (!nombreValido);

            return nombre;
        }

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

        }
        public static void BarraDeVidaU(int vidaTotal, int vidaRestante)
        {
            double hpRestante = vidaRestante / (double)vidaTotal;
            int largoBordes = 200;
            int barritas = (int)Math.Ceiling(hpRestante * largoBordes);

            string barra = new string('█', barritas).PadRight(largoBordes, '░');
            string lineaBarraInermedia = $"*{barra}*  {Math.Round(hpRestante * 100)}%";

            Console.WriteLine(new string('*', largoBordes + 2));
            Console.WriteLine(lineaBarraInermedia);
            Console.WriteLine(new string('*', largoBordes + 2));
        }
    }
}