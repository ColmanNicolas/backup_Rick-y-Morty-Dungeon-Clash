using PersonajeClass;

namespace PartidaClass
{
    public class Partida
    {
        public Personaje? PersonajeJugador { get; set; }
        public List<Personaje>? PersonajesVivos { get; set; }
        public List<Personaje>? PersonajesDerrotadosPorElJugador { get; set; }
        public List<Personaje>? PersonajesQuePerdieron { get; set; }

    }
}