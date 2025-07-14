
using UIUXclass;
using UtilsClass;
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
            Partida partidaActual;
            int opcionPrimaria;

            do
            {
                UIUX.MenuInicialUI();
                opcionPrimaria = Utils.ValidarOpcionMenu(0, 3, "\n Seleccione una opción y presione ENTER ▸ ");

                switch (opcionPrimaria)
                {
                    case 1:   // logica partida nueva
                        partidaActual = new Partida("", new Personaje(), [], [], []);
                        partidaActual = await Gameplay.GenerarUnaNuevaPartida(partidaActual);

                        await Gameplay.RunGameLoop(partidaActual);

                        break;
                    case 2:
                        partidaActual = new Partida("", new Personaje(), [], [], []);
                        partidaActual = Gameplay.CargarUnaPartidaGuardada(partidaActual);

                        if (partidaActual != null) await Gameplay.RunGameLoop(partidaActual);
                        else Utils.GenerarPausaDeSegundos(2.5);

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
