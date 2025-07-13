
using AlmacenamientoClass;
using APIClass;
using ImageClass;
using PartidaClass;
using PersonajeClass;
using UIUXclass;
using UtilsClass;

namespace GameplayClass
{
    public static class Gameplay
    {
        public static async Task RunGameLoop(Partida partidaActual)
        {
            int opcionTerciaria;
            do //empieza la partida. logica iterativa de menu
            {
                UIUX.MenuPrincipalUI(partidaActual.NombreJugador, partidaActual.PersonajesVivos.Count);
                opcionTerciaria = Utils.ValidarOpcionMenu(0, 7, "\n Seleccione una opción y presione ENTER ▸ ");
                Console.Clear();

                switch (opcionTerciaria)
                {
                    case 1:
                        partidaActual.PersonajeJugador.MostrarUnPersonajeDetallado();

                        break;
                    case 2:
                        Personaje.MostrarTablaDeVentajas();

                        break;
                    case 3:
                        EjecutarCombatesDeLaRonda(partidaActual.PersonajesVivos.Count, ref partidaActual);

                        break;
                    case 4:
                        MostrarCombatesDeLaRonda(partidaActual.PersonajesVivos.Count, partidaActual);

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
        }
        public static async Task<Partida> GenerarUnaNuevaPartida(Partida partidaActual)
        {
            List<Personaje> personajes = await API.ObtenerPersonajesAPI(813, false);   //comunicacion con la API
            int opcionSecundaria, taminioPartida;

            personajes.ForEach(p =>
            {
                p.inicializarEstadisticas();
                p.BalancearEstadisticasPorEspecie();
            });

            partidaActual.NombreJugador = UIUX.ElegirNombreJugador("\nIngrese su nombre dentro del juego (4 a 30 caracteres, solo letras y números): ");

            UIUX.ElegirNuevoPersonajeUI();

            opcionSecundaria = Utils.ValidarOpcionMenu(1, 3, "\n Seleccione una opción y presione ENTER ▸ ");

            taminioPartida = Utils.ValidarTamanioPartida();

            partidaActual.PersonajeJugador = UsuarioEligeSuPersonaje(opcionSecundaria, ref personajes, taminioPartida);

            FiltrarPersonajesParaNuevaPartida(personajes, ref partidaActual, taminioPartida);

            return partidaActual;
        }

        public static Partida CargarUnaPartidaGuardada(Partida partidaActual)
        {
            string nombrePartida;

            Almacenamiento.MostrarPartidasGuardadas();

            nombrePartida = UIUX.ElegirNombreJugador("\nIngrese el nombre de la partida: ");  // reutilizo esta funcion porque comparten las mismas restricciones

            partidaActual = Almacenamiento.BuscarUnaPartida(nombrePartida.Trim());

            return partidaActual;
        }

        private static void MostrarCombatesDeLaRonda(int cantJugadoresRestante, Partida partidaActual)
        {
            if (cantJugadoresRestante > 1)
            {
                Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                Console.WriteLine($"\n  [JUGADORES VIVOS]: {cantJugadoresRestante}   [DUELOS EN ESTA RONDA]: {cantJugadoresRestante / 2}  ");
                Utils.GenerarPausaDeSegundos(1);

                Console.WriteLine("\n+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                Console.WriteLine("|               PROXIMOS ENFRENTAMIENTOS          |");
                Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");

                for (int i = 0; i < cantJugadoresRestante - 1; i += 2)
                {
                    string jugador1Nombre = partidaActual.PersonajesVivos[i].name;
                    string jugador1Especie = partidaActual.PersonajesVivos[i].species;
                    string jugador2Nombre = partidaActual.PersonajesVivos[i + 1].name;
                    string jugador2Especie = partidaActual.PersonajesVivos[i + 1].species;

                    if (i == 0)
                    {
                        if (partidaActual.PersonajesVivos[i] == partidaActual.PersonajeJugador) jugador1Nombre = $"<JUGADOR> {jugador1Nombre.ToUpper()}  ▸ {jugador1Especie} ";
                        else jugador1Nombre = $"<IA> {jugador1Nombre.ToUpper()}  ▸ {jugador1Especie}";
                    }
                    else jugador1Nombre = $"<IA> {jugador1Nombre.ToUpper()}  ▸ {jugador1Especie}";

                    Console.Write($"\n  [DUELO #{(i / 2) + 1:D2}]: ");
                    Console.Write($"{jugador1Nombre,-60}  vs                        ");
                    Console.WriteLine($"<IA> {jugador2Nombre.ToUpper()} ▸ {jugador2Especie} ");
                    Utils.GenerarPausaDeSegundos(0.02); // Pausa rápida
                }

                Console.WriteLine("\n+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            }

            else if (cantJugadoresRestante == 1)
            {
                Console.WriteLine("PERSONAJE GANADOR DE LA PARTIDA");
                partidaActual.PersonajesVivos[0].MostrarUnPersonajeDetallado();
            }
        }

        private static void EjecutarCombatesDeLaRonda(int cantJugadoresRestantes, ref Partida miPartida)
        {
            if (cantJugadoresRestantes > 1)
            {
                List<Personaje> personajesVencidosTemporal = [];
                for (int i = 0; i < cantJugadoresRestantes - 1; i += 2)
                {
                    if (i + 1 < miPartida.PersonajesVivos.Count)
                    {
                        Personaje p1 = miPartida.PersonajesVivos[i];  // ok
                        Personaje p2 = miPartida.PersonajesVivos[i + 1];
                        Personaje personajeVencido = EnfrentarDosPersonajes(ref p1, ref p2);

                        miPartida.PersonajesQuePerdieron.Add(personajeVencido);
                        personajesVencidosTemporal.Add(personajeVencido);
                    }
                    else break;
                }

                for (int i = miPartida.PersonajesVivos.Count - 1; i >= 0; i--)
                {
                    if (personajesVencidosTemporal.Contains(miPartida.PersonajesVivos[i]))
                    {
                        miPartida.PersonajesVivos.RemoveAt(i);
                    }
                }

            }
            else if (cantJugadoresRestantes == 1)
            {
                Console.WriteLine("PERSONAJE GANADOR DE LA PARTIDA");
                miPartida.PersonajesVivos[0].MostrarUnPersonajeDetallado();
            }
        }
        private static Personaje UsuarioEligeSuPersonaje(int opcionElegirPersonaje, ref List<Personaje> personajes, int tamanioPArtida)
        {
            Personaje? personajeJugador;
            int auxContador = 0, identificadorPersonaje;

            if (opcionElegirPersonaje == 3) //para opcion 3 retorno un personaje al azar
            {
                personajes.Barajar();
                personajeJugador = personajes[0];
                Utils.GenerarPausaDeSegundos(1);
                Console.Write("\nSu personaje fue asignado:\n");
            }
            else
            {
                if (opcionElegirPersonaje == 2) //mezclar y limitar lista de personajes solo para opcion 2
                {
                    personajes.Barajar();
                    personajes = personajes.Take(tamanioPArtida).ToList();
                }

                do  // logica para elegir el personaje
                {
                    Console.WriteLine("\nPERSONAJES Y CARACTERISICAS (las caracteristicas varian en cada nueva partida)\n");
                    Utils.GenerarPausaDeSegundos(1.5);

                    personajes.ForEach(personaje =>
                    {
                        Console.Write($"{"[" + (auxContador + 1) + "]",-7}");
                        personaje.MostrarMasivamentePersonaje();
                        auxContador++;
                    });
                    auxContador = 0; // no borrar
                    identificadorPersonaje = Utils.ValidarOpcionMenu(1, 825, "\nIngrese el identificador(ID) del personaje que quiera usar: ");
                    personajeJugador = personajes.Find(p => p.id == identificadorPersonaje);

                    if (personajeJugador == null)
                    {
                        Console.WriteLine($"\nError: No se encontró el personaje con el ID: ({identificadorPersonaje}), intente nuevamente...");
                        Utils.GenerarPausaDeSegundos(2);
                    }

                } while (personajeJugador == null);

                Console.Write("\nPersonaje elegido: \n\n");
            }

            if (opcionElegirPersonaje == 1) //mezclar y limitar lista de personajes al final solo para opcion 1
            {
                personajes.Barajar();
                personajes = personajes.Take(tamanioPArtida).ToList();
            }

            personajeJugador.MostrarUnPersonajeDetallado();
            Console.WriteLine("\n\nProcesando.... ");

            Utils.GenerarPausaDeSegundos(4);
            Console.Clear();
            return personajeJugador;
        }

        private static Personaje EnfrentarDosPersonajes(ref Personaje personaje1, ref Personaje rival)
        {
            int hpRestantePersonaje1 = personaje1.hp;
            int hpRestanteRival = rival.hp;
            //Personaje personajeVencido,personajeGanador;
            Console.WriteLine("FIGHT!!");
            MostrarEstadoCombate(personaje1, hpRestantePersonaje1, rival, hpRestanteRival);
            do
            {
                if (personaje1.velocidad > rival.velocidad)     // ataca primero personaje 1
                {
                    hpRestanteRival = Personaje.RecibirDaño(hpRestanteRival, personaje1.CalcularAtaque());
                    MostrarEstadoCombate(personaje1, hpRestantePersonaje1, rival, hpRestanteRival);
                    if (hpRestanteRival > 0)
                    {
                        MostrarEstadoCombate(personaje1, hpRestantePersonaje1, rival, hpRestanteRival);
                        hpRestantePersonaje1 = Personaje.RecibirDaño(hpRestantePersonaje1, rival.CalcularAtaque());
                    }
                }
                else            // ataca primero el rival
                {
                    hpRestantePersonaje1 = Personaje.RecibirDaño(hpRestantePersonaje1, rival.CalcularAtaque());
                    MostrarEstadoCombate(personaje1, hpRestantePersonaje1, rival, hpRestanteRival);
                    if (hpRestanteRival > 0)
                    {
                        hpRestanteRival = Personaje.RecibirDaño(hpRestanteRival, personaje1.CalcularAtaque());
                        MostrarEstadoCombate(personaje1, hpRestantePersonaje1, rival, hpRestanteRival);
                    }
                }
            } while (hpRestanteRival > 0 && hpRestantePersonaje1 > 0);

            if (hpRestanteRival <= 0)
            {
                Personaje.MostrarResultadoEnfrentamiento(personaje1, rival);
                Console.Write("\nSe subió de nivel y mejorarán algunas estadisticas:\nViejas Estadisticas ");
                personaje1.MostrarEstadisticas();
                personaje1.AumentarNivelPersonaje();
                Console.Write("Nuevas estadisticas ");
                personaje1.MostrarEstadisticas();
                Utils.PresioneKparaContinuar();
                return rival;
            }
            else
            {
                Personaje.MostrarResultadoEnfrentamiento(rival, personaje1);
                Console.Write("\nSe subio de nivel y mejoraran algunas estadisticas:\nViejas Estadisticas: ");
                rival.MostrarEstadisticas();
                rival.AumentarNivelPersonaje();
                Console.Write("Nuevas estadisticas ");
                rival.MostrarEstadisticas();
                Utils.PresioneKparaContinuar();
                return personaje1;
            }
        }

        private static void FiltrarPersonajesParaNuevaPartida(List<Personaje> personajesDisponibles, ref Partida miPartida, int cantidadPersonajes)
        {
            personajesDisponibles.Remove(miPartida.PersonajeJugador);
            personajesDisponibles.Barajar();
            miPartida.PersonajesVivos.Add(miPartida.PersonajeJugador);

            for (int i = 0; i < cantidadPersonajes - 1; i++)
            {
                miPartida.PersonajesVivos.Add(personajesDisponibles[i]);
            }
        }
        private static void MostrarEstadoCombate(Personaje personaje1, int hpRestantePersonaje1, Personaje rival, int hpRestanteRival)
        {
            Console.Clear();
            Console.WriteLine($"\n\nPersonaje: {personaje1.name.ToUpper()}");
            UIUX.BarraDeVidaUI(personaje1.hp, hpRestantePersonaje1);

            UIUX.BarraDeVidaUI(rival.hp, hpRestanteRival);
            Console.WriteLine($"Personaje: {rival.name.ToUpper()}");
            Utils.GenerarPausaDeSegundos(1.7);
        }
    }
}