using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
using UIclass;

using System;
using System.Drawing; // Necesitás instalar System.Drawing.Common si usás .NET Core
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using ImageClass;
using System.ComponentModel.DataAnnotations;
using UtilsClass;
using System.ComponentModel;
using PersonajeClass;

namespace RickAndMortyApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            List<Personaje> personajes = new List<Personaje>();
            List<Personaje> personajesVivosEnPartida = new List<Personaje>();
            List<Personaje> rivalesDerrotados = new List<Personaje>();

            /*
                        UI.BarraDeVidaUI(100, 100);  // 100%
                        UI.BarraDeVidaUI(100, 45);   // 45%
                        UI.BarraDeVidaUI(100, 1);    // 0% 
                        */
            int opcionPrimaria, opcionSecundaria, opcionTerciaria, cantidadPersonajes;
            string? nombreJugador;
            Personaje? personajeJugador = new Personaje();
            Personaje? personajeGanador = new Personaje();

            do
            {
                UI.MenuInicialUI();
                opcionPrimaria = Utils.validarOpcionMenu(0, 3, "\nSu opcion: ");

                switch (opcionPrimaria)
                {

                    case 1:   // logica partida nueva
                        personajesVivosEnPartida.Clear();
                        personajes = await ObtenerPersonajesAPI(812, false);  //comunicacion con la api

                        personajes.ForEach(p => p.inicializarEstadisticas());

                        nombreJugador = UI.ElegirNombreJugador();

                        UI.ElegirNuevoPersonajeUI();
                        opcionSecundaria = Utils.validarOpcionMenu(1, 3,"\nSu opcion: " );

                        cantidadPersonajes = Utils.validarTamanioPartida();

                        personajeJugador = UsuarioEligeSuPersonaje(opcionSecundaria, ref personajes, cantidadPersonajes);

                        FiltrarPersonajesParaNuevaPartida(personajes, ref personajesVivosEnPartida, personajeJugador, cantidadPersonajes);

                        //empieza la partida. logica de menu
                        do
                        { 
                            UI.MenuPrincipalUI(nombreJugador, personajesVivosEnPartida.Count);
                            opcionTerciaria = Utils.validarOpcionMenu(0, 7,"\nSu opcion: ");
                            switch (opcionTerciaria)
                            {
                                case 1:
                                    personajeJugador.MostrarUnPersonaje();
                                    break;
                                case 2:
                                    Personaje.MostrarTablaDeVentajas();
                                    break;
                                case 3:
                                    // iniciar combate
                                    break;
                                case 4:
                                    //mostrar combates del corriente turno
                                    Console.WriteLine($"Cantidad de personajes vivos en la partida: {personajesVivosEnPartida.Count} - Duelos restantes: {personajesVivosEnPartida.Count / 2} ");
                                    int auxContador = 0;
                                    personajesVivosEnPartida.ForEach(personaje =>
                                    {
                                        Console.Write($"{"[" + (auxContador + 1) + "]",-7}");
                                        personaje.MostrarMasivamentePersonajes();
                                        Console.WriteLine();
                                        auxContador++;
                                    });
                                    break;
                                case 5:
                                    // ejecutar duelos restantes o hacer partida rapida (?)
                                    break;
                                case 6:
                                    int id = Utils.validarOpcionMenu(1, 826, "\nIngrese el identificador(ID) de un personaje(1 al 826): ");
                                    await ImageToASCII.MostrarPersonajePorId(id);
                                    break;
                                case 7:
                                    // guardar partida en archivo
                                    break;
                                default:
                                    break;
                            }
                            if (opcionTerciaria != 0) Utils.PresioneKparaContinuar();

                        } while (opcionTerciaria != 0);

                        break;
                    case 2: // Logica cargar partida
                        break;
                    case 3:
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
                Utils.PresioneKparaContinuar();
            } while (opcionPrimaria != 0);

            Console.Clear();


        }
        public static Personaje UsuarioEligeSuPersonaje(int opcionElegirPersonaje, ref List<Personaje> personajes, int cantidadPersonajes)
        {
            Personaje personajeJugador;
            int auxContador = 0, identificadorPersonaje;



            if (opcionElegirPersonaje == 3)
            {
                personajes.Barajar();


                personajeJugador = personajes[0];
                Utils.GenerarPausaDeSegundos(1);
                Console.Write("\nSu personaje fue asignado: ");
            }
            else
            {
                if (opcionElegirPersonaje == 2) //mezclar y limitar lista de personajes solo para opcion 2
                {
                    personajes.Barajar();
                    personajes = personajes.Take(cantidadPersonajes).ToList();
                }

                do
                {
                    Console.WriteLine("\nPERSONAJES Y CARACTERISICAS (las caracteristicas varian en cada nueva partida)\n");
                    Utils.GenerarPausaDeSegundos(2);
                    personajes.ForEach(personaje =>
                    {
                        Console.Write($"{"[" + (auxContador + 1) + "]",-7}");
                        personaje.MostrarMasivamentePersonajes();
                        Console.WriteLine("");
                        auxContador++;
                    });
                    auxContador = 0;
                    identificadorPersonaje = Utils.validarOpcionMenu(1, 825,"\nIngrese el identificador(ID) del personaje que quiera usar: ");
                    personajeJugador = personajes.Find(p => p.id == identificadorPersonaje);

                    if (personajeJugador == null)
                    {
                        Console.WriteLine($"\nError: No se encontró el personaje con el ID: ({identificadorPersonaje}), intente nuevamente...");
                        Utils.GenerarPausaDeSegundos(2);
                    }

                } while (personajeJugador == null);

                Console.Write("\nPersonaje elegido: ");
            }

            if (opcionElegirPersonaje == 1) //mezclar y limitar lista de personajes al final solo para opcion 3
            {
                personajes.Barajar();
                personajes = personajes.Take(cantidadPersonajes).ToList();
            }

            personajeJugador.MostrarUnPersonaje();
            Console.WriteLine("\nProcesando.... ");

            Utils.GenerarPausaDeSegundos(4);
            Console.Clear();
            return personajeJugador;
        }

        public static void FiltrarPersonajesParaNuevaPartida(List<Personaje> personajesDisponibles, ref List<Personaje> personajesSeleccionados, Personaje personajeJugador, int cantidadPersonajes)
        {
            personajesDisponibles.Remove(personajeJugador);
            for (int i = 0; i < cantidadPersonajes - 1; i++)
            {
                personajesSeleccionados.Add(personajesDisponibles[i]);
            }
            personajesSeleccionados.Add(personajeJugador);
        }

        public static async Task<List<Personaje>> ObtenerPersonajesAPI(int cantidad, bool aleatorio)  //El segundo parametro mezcla la lista
        {
            List<Personaje>? personajes = new List<Personaje>();

            if (cantidad < 1) return [];

            if (cantidad > 812) cantidad = 812;

            using (HttpClient client = new HttpClient())
            {
                string urlBase = "https://rickandmortyapi.com/api/character/";
                int[] indicesDeTodosLosPersonajes = Enumerable.Range(1, 826).ToArray();  // hay una explicacion de por que traigo todos los personajes

                for (int unIndice = 0; unIndice < 826; unIndice++) urlBase = urlBase + indicesDeTodosLosPersonajes[unIndice].ToString() + ",";

                //Console.WriteLine("MI URL:" + urlBase);

                HttpResponseMessage response = await client.GetAsync(urlBase);
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();

                personajes = JsonSerializer.Deserialize<List<Personaje>>(jsonString);
                personajes = personajes.FindAll(p => p.species != "unknown");

                if (aleatorio) personajes.Barajar(); // retorno personajes aleatorios en caso de pedirlo

                personajes = personajes.Take(cantidad).ToList();
            }
            return personajes;
        }

    }
}
