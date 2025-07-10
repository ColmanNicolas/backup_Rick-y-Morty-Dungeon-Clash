using UtilsClass;

namespace PersonajeClass
{
    public class Personaje

    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string species { get; set; }
        public string gender { get; set; }
        public string image { get; set; }
        public int hp { get; set; }
        public int ataquebase { get; set; }
        public int velocidad { get; set; }  //determina quien ataca primero y se usa para calcular golpe critico y evasion
        public int inteligencia { get; set; }  //para calcular golpe critico
        public int defensa { get; set; }  //para calcular evasion

        public void inicializarEstadisticas()
        {
            Random rng = new Random();
            this.hp = rng.Next(800, 1501);
            this.velocidad = rng.Next(70, 101);
            this.ataquebase = rng.Next(80, 150);
            this.inteligencia = rng.Next(10, 41);
            this.defensa = rng.Next(5, 36);
        }
        public void MostrarMasivamentePersonajes()
        {
            Console.Write($"{"ID:" + id,-6} - Nombre: {name,-41} - Especie: {species,-21} - Genero: {gender,-10}  ▸  hp: {hp,5} - velocidad: {velocidad,4} - ataque base: {ataquebase,4} - inteligencia: {inteligencia,3} - defensa: {defensa,3}\n");
            Utils.GenerarPausaDeSegundos(0.0025);
        }
        public void MostrarUnPersonaje()
        {
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ <DETALLE PERSONAJE>                                         ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ ID: {id,-3} | Nombre: {name,-41} ║");
            Console.WriteLine($"║ Especie: {species,-26} | Género: {gender,-13} ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ [ESTADISTICAS]                                              ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ HP: {hp,-4} | Velocidad: {velocidad,-4} | Ataque: {ataquebase,-4}                   ║");
            Console.WriteLine($"║ Defensa: {defensa,-4} | Inteligencia: {inteligencia,-4}                          ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
        }
        public int CalcularAtaque()
        {
            //logica de cantidad de daño
            return 10;
        }
        public void RecibirDaño(int cantidad)
        {
            //falta posibilidad de evitar ataque
            hp -= cantidad;
            if (hp < 0) hp = 0;
        }
        public bool CalcularGolpeCritico()  // puedo recibir boolean de tiene ventaja o no
        {
            double coef = (inteligencia * 0.3 + velocidad * 0.1) / 100.0;
            coef = Math.Min(coef, 0.2);

            Random rng = new Random();
            return rng.NextDouble() < coef;
        }
        public bool CalcularEvasion_Bloqueo()
        {
            double coef = (defensa * 0.3 + velocidad * 0.15) / 100.0;
            coef = Math.Min(coef, 0.3);

            Random rng = new Random();
            return rng.NextDouble() < coef;
        }

        /*bool TieneVentaja(string atacante, string defensor)
        {
            return ventajas.ContainsKey(atacante) && ventajas[atacante].Contains(defensor);
        }*/


        public class Multiplicadores
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
                ["Poopybutthole"] = new Multiplicadores { Ataque = 1.2, Velocidad = 1.1, Hp = 0.95, Inteligencia = 1.0, Defensa = 0.9 },
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
            }
        }
        private static readonly Dictionary<string, List<string>> ventajas = new()
        {
            { "Human", new List<string> { "Robot", "Disease" } },
            { "Robot", new List<string> { "Animal", "Humanoid" } },
            { "Alien", new List<string> { "Human", "Mythological Creature" } },
            { "Poopybutthole", new List<string> { "Cronenberg", "Disease" } },
            { "Mythological Creature", new List<string> { "Animal", "Human" } },
            { "Animal", new List<string> { "Disease", "Poopybutthole" } },
            { "Disease", new List<string> { "Alien", "Humanoid" } },
            { "Humanoid", new List<string> { "Poopybutthole", "Human" } },
            { "Cronenberg", new List<string> { "Robot", "Alien" } }
        };
        public static void MostrarTablaDeVentajas()
        {
            Console.WriteLine("\n--- Tabla de Ventajas por especie ---");
            if (ventajas.Count == 0)
            {
                Console.WriteLine("El diccionario de ventajas está vacío.");
                return;
            }

            foreach (var unaVentaja in ventajas)
            {
                string clave = unaVentaja.Key;
                List<string> valores = unaVentaja.Value;

                Console.WriteLine($"  {clave} tiene ventaja sobre: {string.Join(", ", valores)}");
            }
            Console.WriteLine("---------------------------------------------------");
        }
    }

}
