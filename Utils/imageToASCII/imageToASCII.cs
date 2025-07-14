using System;
using System.Drawing; // Necesitás instalar System.Drawing.Common si usás .NET Core
using ASCIIcolorClass;
using UtilsClass;

namespace ImageClass
{
    public class ImageToASCII
    {

        public static async Task MostrarPersonajePorId()
        {
            int id = Utils.ValidarOpcionMenu(1, 826, "\nIngrese el identificador(ID) de un personaje(1 al 826): ");
            int anchoMaximo = Utils.ValidarOpcionMenu(150, 350, "\nIngrese el ancho maximo que tendra la imagen (150 al 350): ");
            int imagenAcolor = Utils.ValidarOpcionMenu(0, 1, "\n[0] Mostrar imagen en blanco y negro\n[1] Mostrar imagen con color\n\nSu opcion: ");
            while (true)
            {
                int diseño = Utils.ValidarOpcionMenu(0, 4, "\n[1] Diseño 1\n[2] Diseño 2\n[3] Diseño 3\n[4] Diseño 4\n[0] Salir 0\n\nSu opcion: ");

                if (diseño == 0) return;

                await MostrarImagenComoASCII($"https://rickandmortyapi.com/api/character/avatar/{id}.jpeg", anchoMaximo, imagenAcolor, diseño);
                Utils.PresioneKparaContinuar();
                Console.Clear();
            }

        }
        private static async Task MostrarImagenComoASCII(string url, int anchoMaximo, int imagenAcolor, int diseño)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] datos = await client.GetByteArrayAsync(url);

                    using (MemoryStream ms = new MemoryStream(datos))
                    {
                        using (Bitmap bmp = new Bitmap(ms))
                        {
                            int nuevoAncho = anchoMaximo;

                            int nuevoAlto = (int)(bmp.Height / (double)bmp.Width * nuevoAncho * 0.42);

                            using (Bitmap reducida = new Bitmap(bmp, new Size(nuevoAncho, nuevoAlto)))
                            {
                                for (int y = 0; y < reducida.Height; y++)
                                {
                                    for (int x = 0; x < reducida.Width; x++)
                                    {
                                        Color pixel = reducida.GetPixel(x, y);

                                        if (imagenAcolor == 1) // colorear 
                                        {
                                            // Obtener el ConsoleColor más parecido
                                            ConsoleColor closestConsoleColor = AdvancedConsoleColorMapper.GetClosestConsoleColor(pixel);

                                            // Establecer el color de primer plano de la consola
                                            Console.ForegroundColor = closestConsoleColor;
                                        }

                                        int gris = (pixel.R + pixel.G + pixel.B) / 3;

                                        Console.Write(GrisAASCII(gris, diseño));
                                    }
                                    Console.WriteLine();
                                }
                                Console.ResetColor();
                            }
                        }
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    // Maneja errores relacionados con la solicitud HTTP (ej. 404 Not Found, conexión fallida)
                    Console.WriteLine($"Error al descargar la imagen de la URL: {url}");
                    Console.WriteLine($"Detalles del error HTTP: {httpEx.Message}");

                }
                catch (UriFormatException uriEx)
                {
                    // Maneja errores si la URL proporcionada no es válida
                    Console.WriteLine($"La URL proporcionada no es válida: {url}");
                    Console.WriteLine($"Detalles del error: {uriEx.Message}");
                }
                catch (ArgumentException argEx)
                {
                    // Esto podría ocurrir si MemoryStream o Bitmap reciben datos inválidos
                    Console.WriteLine($"Error de argumento al procesar la imagen: {argEx.Message}");
                    Console.WriteLine("Es posible que los datos descargados no sean una imagen válida.");
                }
                catch (Exception ex)
                {
                    // Atrapa cualquier otra excepción inesperada
                    Console.WriteLine($"Ocurrió un error inesperado al procesar la imagen: {ex.Message}");
                }
            }
        }
        private static char GrisAASCII(int gris, int diseño)
        {
            string escala = "";  //rampa de caracteres
            switch (diseño)
            {
                case 1:
                    escala = "@%#*+=-:. ";
                    break;
                case 2:
                    escala = "█▓▒░ ";
                    break;
                case 3:
                    escala = "10_ ";
                    break;
                case 4:
                    escala = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";
                    break;
                default:
                    escala = "@%#*+=-:. ";
                    break;
            }
            int index = gris * (escala.Length - 1) / 255;
            return escala[index];
        }
    }

}