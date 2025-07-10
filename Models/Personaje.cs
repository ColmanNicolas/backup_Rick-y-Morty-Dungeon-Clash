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
            Console.Write($"{"ID:" + id,-6} - Nombre: {name,-41} - Especie: {species,-21} - Genero: {gender,-10}  ▸  hp: {hp,5} - velocidad: {velocidad,4} - ataque base: {ataquebase,4} - inteligencia: {inteligencia,3} - defensa: {defensa,3}");
        }
        public void MostrarUnPersonaje()
        {
            Console.Write($"ID: + {id}  Nombre: {name} - Especie: {species} - Genero: {gender} ▸  hp: {hp} - velocidad: {velocidad} - ataque base: {ataquebase} - inteligencia: {inteligencia} - defensa: {defensa}");
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
    }
}
