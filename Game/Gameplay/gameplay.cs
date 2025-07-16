
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
            bool omitirEnfrentamientos = false;

            if (cantJugadoresRestantes > 1)
            {
                List<Personaje> personajesVencidosTemporal = [];
                for (int i = 0; i < cantJugadoresRestantes - 1; i += 2)
                {
                    if (i + 1 < miPartida.PersonajesVivos.Count)
                    {
                        Personaje p1 = miPartida.PersonajesVivos[i];
                        Personaje p2 = miPartida.PersonajesVivos[i + 1];
                        Personaje personajeVencido = EnfrentarDosPersonajes(ref p1, ref p2, ref miPartida, omitirEnfrentamientos);

                        miPartida.PersonajesQuePerdieron.Add(personajeVencido);
                        personajesVencidosTemporal.Add(personajeVencido);

                    }
                    else break;
                    omitirEnfrentamientos = true; // omito enfrentamientos luego de la primera ronda
                }

                EliminarPersonajesVencidos(ref miPartida, personajesVencidosTemporal);

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

        private static Personaje EnfrentarDosPersonajes(ref Personaje personaje1, ref Personaje rival, ref Partida partidaActual, bool omitirEnfrentamientoUI)
        {
            // Determino quién ataca primero y en el bucle invierto roles
            Personaje atacante = (personaje1.velocidad >= rival.velocidad) ? personaje1 : rival;
            Personaje defensor = (atacante == personaje1) ? rival : personaje1;

            int hpAtacante = atacante.hp;
            int hpDefensor = defensor.hp;
            int turno = 1;
            bool critico, evasion;

            if (omitirEnfrentamientoUI) //logica secuencial sin UI
            {
                while (hpAtacante > 0 && hpDefensor > 0)
                {
                    critico = false;
                    evasion = false;

                    int danioRealizado = atacante.CalcularAtaque();
                    critico = atacante.RealizaGolpeCritico();

                    if (critico)  // aumenta daño con critico
                    {
                        danioRealizado = (int)Math.Round(danioRealizado * 1.3);
                        hpDefensor = Personaje.RecibirDaño(hpDefensor, danioRealizado);
                    }
                    else
                    {
                        evasion = defensor.RealizaEvasion_Bloqueo();

                        if (!evasion) hpDefensor = Personaje.RecibirDaño(hpDefensor, danioRealizado); //si no evade/bloquea recibe el daño calculado

                        // si evade no recibe daño
                    }


                    // Si el defensor sigue vivo, intercambio roles
                    if (hpDefensor > 0)
                    {
                        Personaje temp = atacante;
                        atacante = defensor;
                        defensor = temp;

                        int hpTemp = hpAtacante;
                        hpAtacante = hpDefensor;
                        hpDefensor = hpTemp;
                    }
                    turno++;
                }
            }
            else //logica secuencial con UI
            {

                UIUX.IniciarCuentaAtras();
                UIUX.MostrarBarrasDeHp(personaje1, (personaje1 == atacante) ? hpAtacante : hpDefensor, rival, (rival == atacante) ? hpAtacante : hpDefensor, turno);
                Utils.GenerarPausaDeSegundos(1.5);

                Console.WriteLine("╔═════════════════════════════════════════════════════════╗           ╔═════════════════════════════════════════════════════════╗");
                Console.WriteLine($"║ ATACA: {defensor.name.ToUpper(),-48} ║ ========> ║ DEFIENDE: {atacante.name.ToUpper(),-46}║");                             // quedó invertido, funciona
                Console.WriteLine("╚═════════════════════════════════════════════════════════╝           ╚═════════════════════════════════════════════════════════╝");

                for (int i = 0; i < 6; i++) Console.WriteLine(new string(' ', 120));  // "limpio" parcialemente la consola 

                Console.SetCursorPosition(0, Console.CursorTop - 6); // luego retorno puntero 6 lugares
                Utils.GenerarPausaDeSegundos(2);

                // combate
                while (hpAtacante > 0 && hpDefensor > 0)
                {
                    critico = false;
                    evasion = false;

                    int danioRealizado = atacante.CalcularAtaque();
                    critico = atacante.RealizaGolpeCritico();

                    if (critico)  // aumenta daño con critico
                    {
                        danioRealizado = (int)Math.Round(danioRealizado * 1.3);
                        hpDefensor = Personaje.RecibirDaño(hpDefensor, danioRealizado);

                        Console.WriteLine($"\n GOLPE CRITICO!                                                          -{danioRealizado,-4} HP");
                        Utils.GenerarPausaDeSegundos(2);

                    }
                    else
                    {
                        evasion = defensor.RealizaEvasion_Bloqueo();

                        if (!evasion)
                        {
                            hpDefensor = Personaje.RecibirDaño(hpDefensor, danioRealizado); //si no evade/bloquea recibe el daño calculado
                            Console.WriteLine($"\n ATAQUE EFECTIVO                                                          -{danioRealizado,-4} HP");
                            Utils.GenerarPausaDeSegundos(2);
                        }
                        else
                        {
                            Console.WriteLine($"\n FALLÓ!!                                                                  ESQUIVA EL ATAQUE");
                            Utils.GenerarPausaDeSegundos(2);
                        }
                    }

                    UIUX.MostrarBarrasDeHp(personaje1, (personaje1 == atacante) ? hpAtacante : hpDefensor, rival, (rival == atacante) ? hpAtacante : hpDefensor, turno);
                    Utils.GenerarPausaDeSegundos(1.5);

                    if (hpAtacante > 0 && hpDefensor > 0)
                    {
                        Console.WriteLine("╔═════════════════════════════════════════════════════════╗           ╔═════════════════════════════════════════════════════════╗");
                        Console.WriteLine($"║ ATACA: {defensor.name.ToUpper(),-48} ║ ========> ║ DEFIENDE: {atacante.name.ToUpper(),-46}║");                             // quedó invertido, funciona
                        Console.WriteLine("╚═════════════════════════════════════════════════════════╝           ╚═════════════════════════════════════════════════════════╝");

                        for (int i = 0; i < 6; i++) Console.WriteLine(new string(' ', 120));  // "limpio" parcialemente la consola 

                        Console.SetCursorPosition(0, Console.CursorTop - 6); // luego retorno puntero 6 lugares
                        Utils.GenerarPausaDeSegundos(2);
                    }

                    // Si el defensor sigue vivo, intercambio roles
                    if (hpDefensor > 0)
                    {
                        Personaje temp = atacante;
                        atacante = defensor;
                        defensor = temp;

                        int hpTemp = hpAtacante;
                        hpAtacante = hpDefensor;
                        hpDefensor = hpTemp;
                    }
                    turno++;
                }
                //muestro resultado final limpio incluyendo barra de hp
                Console.Clear();
                UIUX.MostrarBarrasDeHp(personaje1, (personaje1 == atacante) ? hpAtacante : hpDefensor, rival, (rival == atacante) ? hpAtacante : hpDefensor, turno);
            }

            Personaje ganador = (hpAtacante > 0) ? atacante : defensor;
            Personaje perdedor = (ganador == personaje1) ? rival : personaje1;

            ProcesarVictoria(ganador, perdedor, ref partidaActual, omitirEnfrentamientoUI);

            return perdedor;
        }

        private static void EliminarPersonajesVencidos(ref Partida miPartida, List<Personaje> personajesVencidosTemporal)
        {
            for (int i = miPartida.PersonajesVivos.Count - 1; i >= 0; i--)
            {
                if (personajesVencidosTemporal.Contains(miPartida.PersonajesVivos[i]))
                {
                    miPartida.PersonajesVivos.RemoveAt(i);
                }
            }
        }

        private static void ProcesarVictoria(Personaje ganador, Personaje perdedor, ref Partida partidaActual, bool omitirEnfrentamientoUI)
        {
            if (omitirEnfrentamientoUI)
            {
                ganador.AumentarNivelPersonaje();

                if (partidaActual.PersonajeJugador.id == ganador.id)  //actualizo partidaActual.PersonajeJugador si el jugador paso a la siguiente ronda
                {
                    partidaActual.PersonajeJugador = ganador;
                }

            }
            else
            {
                Personaje.MostrarResultadoEnfrentamiento(ganador, perdedor);
                Console.Write("\n¡El ganador sube de nivel y mejorarán sus estadísticas!\n\nEstadísticas Anteriores: ");
                ganador.MostrarEstadisticas();

                ganador.AumentarNivelPersonaje();

                if (partidaActual.PersonajeJugador.id == ganador.id)  //actualizo partidaActual.PersonajeJugador si el jugador paso a la siguiente ronda
                {
                    partidaActual.PersonajeJugador = ganador;
                }

                Console.Write("Nuevas Estadísticas: ");
                ganador.MostrarEstadisticas();

                Utils.PresioneKparaContinuar();
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

    }
}