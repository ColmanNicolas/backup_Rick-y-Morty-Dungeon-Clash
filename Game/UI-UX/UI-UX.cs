using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using PersonajeClass;

namespace UIclass
{
    public static class UIUX
    {
        public static void MenuInicialUI()
        {
            Console.Clear();
            Console.WriteLine("BIEVENIDO A RICK Y MORTY DUNGEON CLASH\n");
            Console.WriteLine("[1] Para iniciar una partida nueva");
            Console.WriteLine("[2] Para cargar una partida");
            Console.WriteLine("[3] Para borrar una partida (No desarrollada)");
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
        public static string ElegirNombreJugador(string mensaje)   //DEBO FILTRAR QUE NO SE REPITA CON LOS NOMBRES DE PARTIDAS YA CREADAS
        {
            string nombre;
            bool nombreValido = false;

            do
            {
                Console.Write(mensaje);
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

        public static void MenuPrincipalUI(string nombreJugador, int rivalesRestantes)
        {
            Console.Clear();

            // -- Encabezado y Status --
            Console.ForegroundColor = ConsoleColor.White; // Color para el marco y título
            Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                *** SALON DEL TRONO ***                  ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.White; // Cambiamos a blanco para la info
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.Cyan; // Color para el nombre del jugador
            Console.Write($" Jugador: {nombreJugador.PadRight(20)}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Red; // Color para los rivales
            Console.Write($"Rivales: {rivalesRestantes.ToString().PadRight(15)}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════╣");

            // -- Opciones Principales --
            Console.ForegroundColor = ConsoleColor.Gray; // Un color suave para las opciones
            Console.WriteLine("║                                                         ║");
            Console.WriteLine("║ [1] Ver mi personaje                                    ║");
            Console.WriteLine("║ [2] Consultar tabla de ventajas por especie             ║");
            Console.WriteLine("║ [3] Comenzar con la ronda de enfrentamientos            ║");
            Console.WriteLine("║ [4] Ver emparejamientos de esta ronda                   ║");
            Console.WriteLine("║ [5] Ver detalle de partida actual                       ║");
            Console.WriteLine("║ [6] Ver imagen de un personaje                          ║");
            Console.WriteLine("║                                                         ║");

            // -- Opciones Secundarias y Salida --
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╟─────────────────────────────────────────────────────────╢");
            Console.ForegroundColor = ConsoleColor.Green; // Color para la opción de guardar
            Console.WriteLine("║ [7] Guardar Partida actual                              ║");
            Console.ForegroundColor = ConsoleColor.Red; // Color para la opción de salir
            Console.WriteLine("║ [0] Salir de la partida actual                          ║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

            // -- Prompt para el usuario --
            Console.ForegroundColor = ConsoleColor.White;
            // Resetea el color para que el resto de la aplicación no se vea afectada
            Console.ResetColor();
        }
        public static void BarraDeVidaUI(int vidaTotal, int vidaRestante)
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