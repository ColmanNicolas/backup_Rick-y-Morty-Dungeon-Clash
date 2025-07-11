using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
using UIclass;

using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using ImageClass;
using System.ComponentModel.DataAnnotations;
using UtilsClass;
using System.ComponentModel;
using PersonajeClass;
using PartidaClass;
using AlmacenamientoClass;
using APIClass;
using GameplayClass;

namespace RickAndMortyApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            List<Personaje> personajes = new List<Personaje>();

            Partida partidaActual = new Partida("", new Personaje(), [], [], []);

            List<Personaje> personajesDerrotadosPorElJugador = new List<Personaje>();

            /*
                        UI.BarraDeVidaUI(100, 100);  // 100%
                        UI.BarraDeVidaUI(100, 45);   // 45%
                        UI.BarraDeVidaUI(100, 1);    // 0% 
                        */

            int opcionPrimaria, opcionSecundaria, opcionTerciaria, cantidadPersonajesPartida;


            do
            {
                UIUX.MenuInicialUI();
                opcionPrimaria = Utils.ValidarOpcionMenu(0, 3, "\nSu opcion: ");

                switch (opcionPrimaria)
                {

                    case 1:   // logica partida nueva

                        personajes = await API.ObtenerPersonajesAPI(812, false);  //comunicacion con la api

                        personajes.ForEach(p =>
                        {
                            p.inicializarEstadisticas();
                            p.BalancearEstadisticasPorEspecie();
                        });

                        partidaActual.NombreJugador = UIUX.ElegirNombreJugador();


                        UIUX.ElegirNuevoPersonajeUI();
                        opcionSecundaria = Utils.ValidarOpcionMenu(1, 3, "\nSu opcion: ");

                        cantidadPersonajesPartida = Utils.ValidarTamanioPartida();

                        partidaActual.PersonajeJugador = Gameplay.UsuarioEligeSuPersonaje(opcionSecundaria, ref personajes, cantidadPersonajesPartida);

                        Gameplay.FiltrarPersonajesParaNuevaPartida(personajes, ref partidaActual, cantidadPersonajesPartida);

                        //empieza la partida. logica de menu
                        do
                        {
                            UIUX.MenuPrincipalUI(partidaActual.NombreJugador, partidaActual.PersonajesVivos.Count);
                            opcionTerciaria = Utils.ValidarOpcionMenu(0, 7, "\nSu opcion: ");

                            switch (opcionTerciaria)
                            {
                                case 1:
                                    partidaActual.PersonajeJugador.MostrarUnPersonaje();

                                    break;
                                case 2:
                                    Personaje.MostrarTablaDeVentajas();

                                    break;
                                case 3:
                                    Gameplay.EjecutarCombatesDeLaRonda(partidaActual.PersonajesVivos.Count, ref partidaActual);

                                    break;
                                case 4:
                                    Gameplay.MostrarCombatesDeLaRonda(partidaActual.PersonajesVivos.Count, partidaActual);

                                    break;
                                case 5:
                                    partidaActual.Mostrar();
                                    break;
                                case 6:
                                    await ImageToASCII.MostrarPersonajePorId();

                                    break;
                                case 7:
                                    Almacenamiento.guardarUnaPartida(partidaActual);
                                    
                                    break;
                                case 0:

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
                //if (opcionPrimaria != 0) Utils.PresioneKparaContinuar();

            } while (opcionPrimaria != 0);

            Console.Clear();
        }
        
    }
}
