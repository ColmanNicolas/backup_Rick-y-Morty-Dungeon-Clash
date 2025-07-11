using System;
using System.Drawing; // Necesitás instalar System.Drawing.Common si usás .NET Core
using UtilsClass;

namespace ImageClass
{
    public class ImageToASCII
    {

        public static async Task MostrarPersonajePorId()
        {
            int id = Utils.ValidarOpcionMenu(1, 826, "\nIngrese el identificador(ID) de un personaje(1 al 826): ");
            int anchoMaximo = Utils.ValidarOpcionMenu(150, 350, "\nIngrese el ancho maximo que tendra la imagen (150 al 350): ");
            await MostrarImagenComoASCII($"https://rickandmortyapi.com/api/character/avatar/{id}.jpeg", anchoMaximo);
        }
        private static async Task MostrarImagenComoASCII(string url, int anchoMaximo)
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
                                        int gris = (pixel.R + pixel.G + pixel.B) / 3;
                                        Console.Write(GrisAASCII(gris));
                                    }
                                    Console.WriteLine();
                                }
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

        private static char GrisAASCII(int gris)
        {
            string escala = "@%#*+=-:. "; // del más oscuro al más claro
            int index = gris * (escala.Length - 1) / 255;
            return escala[index];
        }
    }
}