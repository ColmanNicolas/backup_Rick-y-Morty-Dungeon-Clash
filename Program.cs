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

namespace RickAndMortyDC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Partida partidaActual = new Partida("", new Personaje(), [], [], []);
            int opcionPrimaria, opcionSecundaria, opcionTerciaria, taminioPartida;

            do
            {
                UIUX.MenuInicialUI();
                opcionPrimaria = Utils.ValidarOpcionMenu(0, 3, "\nSu opcion: ");

                switch (opcionPrimaria)
                {

                    case 1:   // logica partida nueva

                        partidaActual = await Gameplay.GenerarUnaNuevaPartida(partidaActual);

                        do //empieza la partida. logica iterativa de menu
                        {
                            UIUX.MenuPrincipalUI(partidaActual.NombreJugador, partidaActual.PersonajesVivos.Count);
                            opcionTerciaria = Utils.ValidarOpcionMenu(0, 7, "\nSu opcion: ");

                            switch (opcionTerciaria)
                            {
                                case 1:
                                    partidaActual.PersonajeJugador.MostrarUnPersonajeDetallado();

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
                                    partidaActual.MostrarResumen();
                                    break;
                                case 6:
                                    await ImageToASCII.MostrarPersonajePorId();

                                    break;
                                case 7:
                                    Almacenamiento.GuardarUnaPartida(partidaActual);

                                    break;
                                case 0:

                                    break;
                                default:
                                    break;
                            }

                            if (opcionTerciaria != 0) Utils.PresioneKparaContinuar();

                        } while (opcionTerciaria != 0);

                        break;
                    case 2:

                        partidaActual = Gameplay.CargarUnaPartidaGuardada(partidaActual);

                        if (partidaActual == null)
                        {
                            Utils.GenerarPausaDeSegundos(4);
                            break;
                        }
                        do //empieza la partida. logica iterativa de menu
                        {
                            UIUX.MenuPrincipalUI(partidaActual.NombreJugador, partidaActual.PersonajesVivos.Count);
                            opcionTerciaria = Utils.ValidarOpcionMenu(0, 7, "\nSu opcion: ");

                            switch (opcionTerciaria)
                            {
                                case 1:
                                    partidaActual.PersonajeJugador.MostrarUnPersonajeDetallado();

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
                                    partidaActual.MostrarResumen();
                                    break;
                                case 6:
                                    await ImageToASCII.MostrarPersonajePorId();

                                    break;
                                case 7:
                                    Almacenamiento.GuardarUnaPartida(partidaActual);

                                    break;
                                case 0:

                                    break;
                                default:
                                    break;
                            }

                            if (opcionTerciaria != 0) Utils.PresioneKparaContinuar();

                        } while (opcionTerciaria != 0);

                        break;
                    case 3:

                        if (opcionPrimaria != 0) Utils.PresioneKparaContinuar();
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }


            } while (opcionPrimaria != 0);

            Console.Clear();
        }

    }
}
