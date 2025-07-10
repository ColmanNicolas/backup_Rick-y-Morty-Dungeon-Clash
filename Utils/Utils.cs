using System.Linq;
using System;

namespace UtilsClass
{
    public static class Utils
    {
        public static int validarOpcionMenu(int minValor, int maxValor, string mensajeConsola)
        {
            int opcionEntero;
            string? opcion;
            Console.Write(mensajeConsola);
            while (true)
            {
                opcion = Console.ReadLine();

                if (int.TryParse(opcion, out opcionEntero) && opcionEntero >= minValor && opcionEntero <= maxValor) return opcionEntero;
                else
                {
                    Console.WriteLine($"Error. La opción ingresada ({opcion}) no es válida. Por favor, ingrese un número entre {minValor} y {maxValor}.");
                    Console.Write("Su opcion: ");
                }
            }
        }
        public static int validarTamanioPartida()
        {
            int opcionEntero;
            string? opcion;
            int[] valoresAceptados = [2, 4, 8, 16, 32, 64, 128, 256, 512];
            
            Console.Write("\nSeleccione cuantos personajes tendrá la partida (2, 4, 8, 16, 32, 64, 128, 256, 512): ");
            while (true)
            {
                opcion = Console.ReadLine();

                if (int.TryParse(opcion, out opcionEntero) && valoresAceptados.Contains(opcionEntero)) return opcionEntero;
                else
                {
                    Console.WriteLine($"Error. La opción ingresada ({opcion}) no es válida. Por favor, ingrese un número del siguiente listado (2, 4, 8, 16, 32, 64, 128, 256, 512).");
                    Console.Write("Su opcion: ");
                }
            }
        }

        public static void GenerarPausaDeSegundos(double segundos)
        {
            Thread.Sleep((int)(segundos * 1000)); //
        }
        public static void PresioneKparaContinuar()
        {
            string? inputUsuario;
            do
            {
                Console.Write("\nPresione K para continuar: ");
                inputUsuario = Console.ReadLine();

                if (inputUsuario != null && string.Equals(inputUsuario.ToLower(), "k")) return;

            } while (true);
        }
    }
    public static class ListExtensions
    {

        private static Random rng = new Random();
        /*## El Algoritmo de Fisher-Yates 🔀
        La idea es simple:
        Recorres la colección desde el final hacia el principio.
        En cada paso, eliges un elemento al azar de la parte de la lista que aún no has recorrido.
        Intercambias el elemento actual con el elemento elegido al azar.*/
        public static void Barajar<T>(this IList<T> list) //Metodo de extension para List y Array
        {
            int n = list.Count;
            while (n > 1)         // algoritmo de Fisher-Yates
            {
                n--;
                // Elige un índice aleatorio 'k' entre 0 y n (inclusive).
                int k = rng.Next(n + 1);

                // Intercambia el valor en la posición 'k' con el de la posición 'n'.
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}