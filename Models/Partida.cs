using PersonajeClass;

namespace PartidaClass
{
    public class Partida
    {
        public string NombreJugador { get; set; }
        public Personaje PersonajeJugador { get; set; }
        public List<Personaje> PersonajesVivos { get; set; }
        public List<Personaje> PersonajesDerrotadosPorElJugador { get; set; }
        public List<Personaje> PersonajesQuePerdieron { get; set; }

        public Partida()
        {
            NombreJugador = "";
            PersonajeJugador = new Personaje();
            PersonajesVivos = new List<Personaje>();
            PersonajesDerrotadosPorElJugador = new List<Personaje>();
            PersonajesQuePerdieron = new List<Personaje>();
            
        }
        public Partida(string nombre, Personaje jugador, List<Personaje> Pvivos, List<Personaje> PderrotadosPorJugaodr, List<Personaje> Pvencidos)
        {
            NombreJugador = nombre;
            PersonajeJugador = jugador;
            PersonajesVivos = Pvivos;
            PersonajesDerrotadosPorElJugador = PderrotadosPorJugaodr;
            PersonajesQuePerdieron = Pvencidos;
        }
        public void MostrarResumen()
        {
            Console.WriteLine("--- Detalles de la Partida ---");
            Console.WriteLine($"Nombre del Jugador: **{NombreJugador}**");

            Console.WriteLine("\n--- Personaje del Jugador ---");

            if (PersonajeJugador != null) PersonajeJugador.MostrarUnPersonajeDetallado();
            else Console.WriteLine("El personaje del jugador no está definido.");

            Console.WriteLine("\n--- Personajes Vivos ---");
            if (PersonajesVivos != null && PersonajesVivos.Count > 0)
            {
                foreach (var p in PersonajesVivos)
                {
                    p.MostrarMasivamentePersonaje(); 
                }
            }
            else{ Console.WriteLine("No hay personajes vivos en esta lista.");}

            Console.WriteLine("\n--- Personajes Derrotados por el Jugador ---");
            if (PersonajesDerrotadosPorElJugador != null && PersonajesDerrotadosPorElJugador.Count > 0)
            {
                foreach (var p in PersonajesDerrotadosPorElJugador)
                {
                    p.MostrarMasivamentePersonaje(); 
                }
            }
            
            else { Console.WriteLine("El jugador no ha derrotado a ningún personaje en esta lista."); }

            Console.WriteLine("\n--- Personajes que Perdieron (Vencidos) ---");
            if (PersonajesQuePerdieron != null && PersonajesQuePerdieron.Count > 0)
            {
                foreach (var p in PersonajesQuePerdieron)
                {
                    p.MostrarMasivamentePersonaje(); 
                }
            }
            
            else { Console.WriteLine("No hay personajes vencidos en esta lista."); }

            Console.WriteLine("-----------------------------");
        }
    }
}