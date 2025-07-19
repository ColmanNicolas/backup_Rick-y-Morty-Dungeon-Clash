using UtilsClass;

namespace PersonajeClass
{
    public class Personaje

    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }  // se podrai aprovechar de alguna manera
        public string species { get; set; }
        public string gender { get; set; }
        public int hp { get; set; }
        public int ataquebase { get; set; }
        public int velocidad { get; set; }  //determina quien ataca primero y se usa para calcular golpe critico y evasion
        public int inteligencia { get; set; }  //para calcular golpe critico
        public int defensa { get; set; }  //para calcular evasion
        public int nivel { get; set; }  //inicia en 1, se incrementa en 1 por ronda

        private Random _rng = new Random();

        public Personaje() // contructor de clase
        {
            id = 0;
            name = "";
            status = "";
            species = "";
            gender = "";
            hp = 0;
            ataquebase = 0;
            velocidad = 0;
            inteligencia = 0;
            defensa = 0;
            nivel = 1;
        }
        public void inicializarEstadisticas()
        {
            this.hp = _rng.Next(800, 1201);
            this.ataquebase = _rng.Next(80, 141);
            this.velocidad = _rng.Next(70, 101);
            this.inteligencia = _rng.Next(10, 41);
            this.defensa = _rng.Next(5, 41);
        }
        public void MostrarMasivamentePersonaje()
        {
            Console.Write($"{"ID:" + id,-6} - Nombre: {name,-41} - Especie: {species,-21} - Genero: {gender,-10}  ▸  hp: {hp,5} - ataque base: {ataquebase,4} - velocidad: {velocidad,4} - inteligencia: {inteligencia,3} - defensa: {defensa,3}\n");
            Utils.GenerarPausaDeSegundos(0.0025);
        }
        public void MostrarUnPersonajeSencillo()
        {                                                     // sacar luego
            Console.Write($"{"ID:" + id,-6} - Nombre: {name} - Nivel: {nivel} - Especie: {species} - Genero: {gender}  ▸ hp: {hp} - ataque base: {ataquebase} - velocidad: {velocidad} - inteligencia: {inteligencia} - defensa: {defensa}\n");
        }
        public void MostrarEstadisticas()
        {
            Console.Write($" ▸ nivel: {nivel} - hp: {hp} - ataque base: {ataquebase} - velocidad: {velocidad} - inteligencia: {inteligencia} - defensa: {defensa}\n");
        }
        public void MostrarUnPersonajeDetallado()
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ <DETALLE PERSONAJE>            NIVEL: {nivel,-12}          ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ ID: {id,-3} | Nombre: {name,-41} ║");
            Console.WriteLine($"║ Especie: {species,-26} | Género: {gender,-13} ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ [ESTADISTICAS]                                              ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ HP: {hp,-4} | Velocidad: {velocidad,-4} | Ataque: {ataquebase,-23}║");
            Console.WriteLine($"║ Defensa: {defensa,-4} | Inteligencia: {inteligencia,-30}║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════╝");
        }
        public static void MostrarResultadoEnfrentamiento(Personaje p1, Personaje p2)
        {

            Console.WriteLine("                    ..::<GANADOR>::..                                                   ..::<PERDEDOR>::..                          ");
            Console.WriteLine("╔═════════════════════════════════════════════════════════════╗     ╔═════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ <DETALLE PERSONAJE>            NIVEL: {p1.nivel,-15}       ║     ║ <DETALLE PERSONAJE>            NIVEL: {p2.nivel,-14}        ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣     ╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ ID: {p1.id,-3} | Nombre: {p1.name,-41} ║     ║ ID: {p2.id,-3} | Nombre: {p2.name,-41} ║");
            Console.WriteLine($"║ Especie: {p1.species,-26} | Género: {p1.gender,-13} ║     ║ Especie: {p2.species,-26} | Género: {p2.gender,-13} ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣     ╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ [ESTADISTICAS]                                              ║     ║ [ESTADISTICAS]                                              ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣     ╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ HP: {p1.hp,-4} | Velocidad: {p1.velocidad,-4} | Ataque: {p1.ataquebase,-23}║     ║ HP: {p2.hp,-4} | Velocidad: {p2.velocidad,-4} | Ataque: {p2.ataquebase,-4}                   ║");
            Console.WriteLine($"║ Defensa: {p1.defensa,-4} | Inteligencia: {p1.inteligencia,-30}║     ║ Defensa: {p2.defensa,-4} | Inteligencia: {p2.inteligencia,-30}║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════╝     ╚═════════════════════════════════════════════════════════════╝");

        }
        public int CalcularAtaque()
        {
            double porcentajeVariacion = 0.10;

            int minDanio = (int)(this.ataquebase * (1 - porcentajeVariacion));
            int maxDanio = (int)(this.ataquebase * (1 + porcentajeVariacion));

            return _rng.Next(minDanio, maxDanio + 1);
        }
        public static int RecibirDaño(int hpRestante, int danio)
        {
            //falta posibilidad de evitar ataque
            hpRestante -= danio;
            if (hpRestante < 0) hpRestante = 0;
            return hpRestante;
        }
        public bool RealizaGolpeCritico(bool TieneVentajaAtacante)
        {
            double coef = (inteligencia * 0.33 + velocidad * 0.12) / 100.0;
            coef = Math.Min(coef, 0.35);

            if (TieneVentajaAtacante)
            {
                if (_rng.NextDouble() < 0.50)   // 50% de chances de aumentar chances de hacer golpe critico
                {
                    coef += 0.15 + (this.nivel * 2) / 10; // ( 15 + nivel * 2 ) % mas de chances de hacer critico
                }
            }

            return _rng.NextDouble() < coef;
        }
        public bool RealizaEvasion_Bloqueo(bool TieneVentajaAtacante)
        {
            double coef = (defensa * 0.30 + velocidad * 0.15) / 100.0;
            coef = Math.Min(coef, 0.35);


            if (TieneVentajaAtacante)
            {
                if (_rng.NextDouble() < 0.50)   // 50% de chances de disminuir chances de esquivar/bloquear golpe
                {
                    coef -= 0.07 + (this.nivel * 2) / 10; // (7 + nivel * 2) % menos de chances de esquivar/bloquear golpe
                }
            }


            coef = Math.Max(0, coef); // aseguro que no sea negativo

            return _rng.NextDouble() < coef;
        }

        public static bool TieneVentaja(string atacante, string defensor)
        {
            return ventajas.ContainsKey(atacante) && ventajas[atacante].Contains(defensor);
        }

        public void AumentarNivelPersonajeAleatoreamente()
        {

            int[] indicesOpciones = [3, 4, 5];

            indicesOpciones.Barajar();
            double porcenajeIncrementeo;

            for (int i = 0; i < 2; i++) // aumento 2 de 3 estadisticas al azar (descartando hp y ataquebase)
            {
                switch (indicesOpciones[i])
                {
                    case 3:

                        if (_rng.NextDouble() < 0.50) // 50% de probabilidad para esta variante
                        {
                            porcenajeIncrementeo = _rng.Next(20, 36) / 100.0;
                            inteligencia = (int)Math.Round(inteligencia + (inteligencia * porcenajeIncrementeo));
                        }
                        else
                        {
                            inteligencia += _rng.Next(20, 31);
                        }

                        break;
                    case 4:   // mejora defensa entre 0.2 y 0.35
                        porcenajeIncrementeo = _rng.Next(20, 36) / 100.0;
                        defensa = (int)Math.Round(defensa + (defensa * porcenajeIncrementeo));

                        if (_rng.NextDouble() < 0.50)
                        {
                            porcenajeIncrementeo = _rng.Next(20, 36) / 100.0;
                            defensa = (int)Math.Round(defensa + (defensa * porcenajeIncrementeo));
                        }
                        else
                        {
                            defensa += _rng.Next(20, 31);
                        }


                        break;
                    case 5:   // mejora velocidad 0.2  **afecta varias habilidades puede genear desbalanceo
                        porcenajeIncrementeo = 0.2;
                        velocidad = (int)Math.Round(velocidad + (velocidad * porcenajeIncrementeo));
                        break;
                    default:
                        break;
                }
            }

            if (_rng.NextDouble() < 0.50)  //Dos posibles variantes de mejora al azar
            {
                hp += _rng.Next(450, 551);

                porcenajeIncrementeo = _rng.Next(25, 36) / 100.0;
                ataquebase = (int)Math.Round(ataquebase + (ataquebase * porcenajeIncrementeo));
            }
            else
            {
                ataquebase += _rng.Next(40, 60);

                porcenajeIncrementeo = _rng.Next(35, 46) / 100.0;
                hp = (int)Math.Round(hp + (hp * porcenajeIncrementeo));

            }
            nivel++;
        }

        private class Multiplicadores
        {
            public double Hp { get; set; } = 1.0;
            public double Ataque { get; set; } = 1.0;
            public double Inteligencia { get; set; } = 1.0;
            public double Velocidad { get; set; } = 1.0;
            public double Defensa { get; set; } = 1.0;
        }
        public void BalancearEstadisticasPorEspecie()
        {
            var multiplicadoresPorEspecie = new Dictionary<string, Multiplicadores>()
            {
                ["Human"] = new Multiplicadores { Inteligencia = 1.5, Velocidad = 1.1, Hp = 1.0, Ataque = 1.0, Defensa = 1.1 },
                ["Alien"] = new Multiplicadores { Inteligencia = 1.5, Ataque = 1.3, Velocidad = 1.0, Hp = 0.9, Defensa = 1.2 },
                ["Humanoid"] = new Multiplicadores { Velocidad = 1.4, Inteligencia = 1.1, Hp = 1.0, Ataque = 0.9, Defensa = 1.0 },
                ["el juego se llama rick y morty: dungeon clash. es un juego de rol de estrategia y combates basado en la serie de rick y morty. el objetivo es ser el ultimo en pie en una partida personalizada de muchos personajes de la serie. los enfrentamientos son de a dos personajes. el ganador avanza a la sigueinte ronda y el perdedor queda fuera de la partida. Los modos de juego son partida personalizada de 2,4,8,16,32,64,128,258 o 512 jugadores aleatoreos a eleccion del jugador, y hay 3 opciones para elegir el personaje del jugador. puede elegirlo de la lista completa de los 813 personajes disponibles en el juego, dichos personajes son incorporados al juego mediante la api publica https://rickandmortyapi.com/, la otra opcion es elegir el personaje de una lista aleatorea del tamaño de la partida que dictó el jugador y la tercera opcion es obtener un personaje de forma completamente aleatorea. Los personajes en toda instancia nueva del juego se crean con estadisticas de hp,ataque base, velocidad,inteligencia y defensa aleatoreas entre rangos preestablecidos dentro de las mecanicas del juego, luego se balancean las estadisticas con parametros positivos y negativos segun la clase del personaje. Las clases jugables son human,humanoid,robot,alien,disease,mithological creature, Poopybutthole, cronenberg y animal. Para iniciar la partida se barajea la lista de personajes de la partida para daterminar los enfrentamientos futuros. Una vez dentro de la partida, estaremos en el salon del trono para gestionar la partida desde el menu que tiene las siguientes opciones. 1 ver personaje: para ver el personaje con detalle, 2 ver tabla de ventajas: para ver la tabla de ventajas de poder que tienen las clases de personajes sobre otras, 3 iniciar Proximo enfrentamiento: para iniciar los enfrentamientos de a dos personajes. el primer enfrentamiento siempre tiene interfaz visual y dinamica 4 ver tabla de enfrentamientos: para ver de a pares los enfrentamientos de todos los personajes de la corriente ronda, 5 ver detalle de la partida: para ver todo el registro y resumen de la partida, 6 ver imagen de un personaje: para ver imagen por consola de un personaje existente en la api, opcion blanco y negro y a color, opcion 7: guardar la partida. (Aca necesito un espacio en blanco para luego desarrollar con mas detalle cada opcion de menu, eso lo haremos en otro prompt, no extiendas demasiado la descripcion de las opciones que ya te mencione). como se juega: una vez creada la partida, el jugador puede ver el detalle de su personaje con la opcion 1. se puede ver al siguiente contrincante y su especie con la opcion 4 del menu, si se quiere ver mas detalle de estadisticas se puede presionar la opcion 5 de menu, se puede analizar luego si el jugador tiene ventaja por su especie con la opcion 2. para iniciar la ronda se presion la opcion 3 del menu. el combate principal siempre es el primero que tiene interfaz grafica y dinamica de cada turno del enfretamiento, si el jugador esta vivo, siempre vera su enfrentamiento, si pierde, se vera el primer enfretamientos de las IAs. durante el combate podemos ver por cada turno la barra de hp de cada personaje, el detalle de quien ataca a quien y quien se defiende y 3 resultados posibles del ataque,ATAQUE EFECTIVO: que hace el daño del ataque base del personaje con una variacion +- del 10%. ATAQUE CRITICO: al daño base obtenido se le aumenta el 50%. FALLÓ: El rival se defendio/esquivo el ataque y no sufre disminucion de su hp. los turnos corren hasta que uno de los personajes queda con hp en 0. se muestra por pantalla vencedor y perdedor con detalle y el ganador sube de nivel. en el caso de personaje jugador, el aumento de nivel se puede elegir manualmente que mejoras estadisticas desea para su personaje, pudiendo elegir entre 3 diferentes variantes aleatoreas mostradas por pantalla. luego de una eleccion valida se muestra por pantalla el detalla y se retorna a menu omitiendo la interfaz grafica de los combates restantes entre IAs, sus mejora de nivel son aleatoreas. terminada la ronda se puede ver la siguiente tabla de enfrentamientos y/o el resumen de la partida actual que cuenta con todas las estadisticas de personajes. los siguientes turnos tendran la misma dinamica de iniciar el enfrentamiento y el ganador seguira subiendo de nivel, si el jugador pierde la partida, ya no podra volver a reincorporarse. pero podra hacer el seguimiento del avance del resto de personajes siendo testigo siempre del primer enfrentamiento de la tabla.Tambien se puede hacer el guardado de la partida en cualquier momento desde el menu para poder reingresar luego desde el menu incial al estado actual de la partida actual en caso de cerrar el programa. El juego termina cuando el ultimo duelo se da por finalizado y queda un solo personaje vivo en pie y sera condecorado con el derecho al trono de hierro. El juego se desarrolló completamente con c# y .net e incluye todos los temas vistos en las clases teoricas de la materia Taller de lenguajes 1 - FACET - UNT , desde la base de la estructura de un programa en C# hasta gestion de archivos y directorios, uso de Streams para manipulacion binaria de archivos, serializacion y deserializacion de clases a formato JSON y peticiones asincronicas a APIs publicas en la web."] = new Multiplicadores { Ataque = 1.2, Velocidad = 1.1, Hp = 0.95, Inteligencia = 1.0, Defensa = 0.9 },
                ["Mythological Creature"] = new Multiplicadores { Velocidad = 1.4, Hp = 1.2, Ataque = 1.1, Inteligencia = 1.0, Defensa = 1.0 },
                ["Robot"] = new Multiplicadores { Hp = 1.4, Ataque = 1.0, Inteligencia = 0.9, Velocidad = 1.05, Defensa = 1.3 },
                ["Cronenberg"] = new Multiplicadores { Hp = 1.4, Ataque = 1.2, Velocidad = 0.8, Inteligencia = 0.85, Defensa = 1.0 },
                ["Disease"] = new Multiplicadores { Hp = 0.8, Ataque = 1.7, Inteligencia = 0.7, Velocidad = 1.3, Defensa = 0.8 },
                ["Animal"] = new Multiplicadores { Ataque = 1.2, Velocidad = 1.2, Hp = 1.0, Inteligencia = 0.9, Defensa = 1.1 }
            };

            if (multiplicadoresPorEspecie.TryGetValue(this.species, out Multiplicadores mult))
            {
                this.hp = (int)Math.Floor(this.hp * mult.Hp);
                this.ataquebase = (int)Math.Floor(this.ataquebase * mult.Ataque);
                this.inteligencia = (int)Math.Floor(this.inteligencia * mult.Inteligencia);
                this.velocidad = (int)Math.Floor(this.velocidad * mult.Velocidad);
                this.defensa = (int)Math.Floor(this.defensa * mult.Defensa);
            }
        }
        private static readonly Dictionary<string, List<string>> ventajas = new()
        {
            { "Human", new List<string> { "Robot", "Disease" } },
            { "Robot", new List<string> { "Animal", "Humanoid" } },
            { "Alien", new List<string> { "robot", "Mythological Creature" } },
            { "Poopybutthole", new List<string> { "Cronenberg", "Mythological Creature" } },
            { "Mythological Creature", new List<string> { "Animal", "Cronenberg" } },
            { "Animal", new List<string> { "Disease", "Poopybutthole" } },
            { "Disease", new List<string> { "Alien", "Humanoid" } },
            { "Humanoid", new List<string> { "Poopybutthole", "Human" } },
            { "Cronenberg", new List<string> { "human", "Alien" } }
        };
        public static void MostrarTablaDeVentajas()
        {
            Console.WriteLine("                 ..::[ TABLA DE VENTAJAS ESTRATEGICAS ]::..");
            Console.WriteLine(" =========================================================================");

            if (ventajas.Count == 0)
            {
                Console.WriteLine("|             EL SISTEMA NO                |");
                Console.WriteLine("|        DETECTA DATOS DE VENTAJA          |");
                Console.WriteLine(" =========================================================================");
                return;
            }

            Console.WriteLine("/-------------------------------------------------------------------------\\");
            foreach (var unaVentaja in ventajas)
            {
                string clave = unaVentaja.Key.ToUpper();
                List<string> valores = unaVentaja.Value;
                string lineaVentaja = $"| {clave.PadRight(21)} > VENTAJA SOBRE: {string.Join(", ", valores)}";

                Console.WriteLine(lineaVentaja.PadRight(74) + "|");
            }
            Console.WriteLine("\\-------------------------------------------------------------------------/");
            Console.WriteLine("                       [ TRANSMISION FINALIZADA ]");
        }
        public Personaje CopiarSuperficialmente()
        {
            return (Personaje)this.MemberwiseClone();
        }
    }

}
