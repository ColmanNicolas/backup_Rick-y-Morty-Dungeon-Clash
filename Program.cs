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
            int opcionPrimaria;

            do
            {
                UIUX.MenuInicialUI();
                opcionPrimaria = Utils.ValidarOpcionMenu(0, 3, "\n Seleccione una opción y presione ENTER ▸ ");

                switch (opcionPrimaria)
                {
                    case 1:   // logica partida nueva

                        partidaActual = await Gameplay.GenerarUnaNuevaPartida(partidaActual);

                        await Gameplay.RunGameLoop(partidaActual);

                        break;
                    case 2:

                        partidaActual = Gameplay.CargarUnaPartidaGuardada(partidaActual);
                        
                        if (partidaActual != null) await Gameplay.RunGameLoop(partidaActual);

                        break;
                    case 3:
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
