


using PartidaClass;
using PersonajeClass;
using UtilsClass;

namespace GameplayClass
{
    public class Gameplay
    {
        public static void MostrarCombatesDeLaRonda(int cantJugadoresRestante, Partida partidaActual)
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
                    // bug cuando muere el jugador en mostrar el duelo
                    string display1 = (i == 0) ? $"<JUGADOR> {jugador1Nombre.ToUpper()}  ▸ {jugador1Especie} " : $"<IA> {jugador1Nombre.ToUpper()}  ▸ {jugador1Especie}";

                    Console.Write($"\n  [DUELO #{(i / 2) + 1:D2}]: ");
                    Console.Write($"{display1,-60}  vs                        ");
                    Console.WriteLine($"<IA> {jugador2Nombre.ToUpper()} ▸ {jugador2Especie} ");
                    Utils.GenerarPausaDeSegundos(0.01); // Pausa rápida
                }

                Console.WriteLine("\n+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            }
            else if (cantJugadoresRestante == 1)
            {
                Console.WriteLine("PERSONAJE GANADOR DE LA PARTIDA");
                partidaActual.PersonajesVivos[0].MostrarUnPersonaje();
            }
        }
        public static void EjecutarCombatesDeLaRonda(int cantJugadoresRestantes, ref Partida miPartida)
        {
            if (cantJugadoresRestantes > 1)
            {
                List<Personaje> personajesVencidosTemporal = [];
                for (int i = 0; i < cantJugadoresRestantes - 1; i += 2)
                {
                    if (i + 1 < miPartida.PersonajesVivos.Count)
                    {
                        Personaje personajeVencido = EnfrentarDosPersonajes(miPartida.PersonajesVivos[i], miPartida.PersonajesVivos[i + 1]);
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
                miPartida.PersonajesVivos[0].MostrarUnPersonaje();
            }
        }
        public static Personaje UsuarioEligeSuPersonaje(int opcionElegirPersonaje, ref List<Personaje> personajes, int cantidadPersonajes)
        {
            Personaje personajeJugador;
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
                    personajes = personajes.Take(cantidadPersonajes).ToList();
                }

                do  // logica para elegir el personaje
                {
                    Console.WriteLine("\nPERSONAJES Y CARACTERISICAS (las caracteristicas varian en cada nueva partida)\n");
                    Utils.GenerarPausaDeSegundos(1.5);
                    personajes.ForEach(personaje =>
                    {
                        Console.Write($"{"[" + (auxContador + 1) + "]",-7}");
                        personaje.MostrarMasivamentePersonajes();
                        auxContador++;
                    });
                    auxContador = 0;
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

            if (opcionElegirPersonaje == 1) //mezclar y limitar lista de personajes al final solo para opcion 3
            {
                personajes.Barajar();
                personajes = personajes.Take(cantidadPersonajes).ToList();
            }

            personajeJugador.MostrarUnPersonaje();
            Console.WriteLine("\n\nProcesando.... ");

            Utils.GenerarPausaDeSegundos(4);
            Console.Clear();
            return personajeJugador;
        }
        
        public static Personaje EnfrentarDosPersonajes(Personaje personaje1, Personaje rival)
        {
            int hpPersonaje1 = personaje1.hp;
            int hpRival = rival.hp;
            Personaje personajeVencido;
            int auxContador = 0;
            do
            {
                if (personaje1.velocidad > rival.velocidad)     // ataca primero personaje 1
                {
                    hpRival = Personaje.RecibirDaño(hpRival, personaje1.CalcularAtaque());

                    if (hpRival >= 0)
                    {
                        hpPersonaje1 = Personaje.RecibirDaño(hpPersonaje1, rival.CalcularAtaque());
                    }
                }
                else            // ataca primero el rival
                {
                    hpPersonaje1 = Personaje.RecibirDaño(hpPersonaje1, rival.CalcularAtaque());

                    if (hpRival >= 0)
                    {
                        hpRival = Personaje.RecibirDaño(hpRival, personaje1.CalcularAtaque());
                    }
                }
                Console.WriteLine("FIN DEl TURNO: " + ++auxContador);
            } while (hpRival > 0 && hpPersonaje1 > 0);

            if (hpRival <= 0) personajeVencido = rival;
            else personajeVencido = personaje1;

            Console.WriteLine("\nEl siguiente personaje Perdio la batalla: \n");
            personajeVencido.MostrarMasivamentePersonajes();
            Utils.PresioneKparaContinuar();

            return personajeVencido;  // retorno derrotado para removerlo de lista de jugadores
        }

        public static void FiltrarPersonajesParaNuevaPartida(List<Personaje> personajesDisponibles, ref Partida miPartida, int cantidadPersonajes)
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