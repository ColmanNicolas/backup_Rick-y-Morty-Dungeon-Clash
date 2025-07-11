using System.Text.Json;
using PersonajeClass;
using UtilsClass;

namespace APIClass
{
    public static class API
    {
        public static async Task<List<Personaje>> ObtenerPersonajesAPI(int cantidad, bool aleatorio)  //El segundo parametro mezcla la lista
        {
            List<Personaje>? personajes = new List<Personaje>();

            if (cantidad < 1) return [];

            if (cantidad > 812) cantidad = 812;

            using (HttpClient client = new HttpClient())
            {
                string urlBase = "https://rickandmortyapi.com/api/character/";
                int[] indicesDeTodosLosPersonajes = Enumerable.Range(1, 826).ToArray();  // hay una explicacion de por que traigo todos los personajes

                for (int unIndice = 0; unIndice < 826; unIndice++) urlBase = urlBase + indicesDeTodosLosPersonajes[unIndice].ToString() + ",";

                //Console.WriteLine("MI URL:" + urlBase);

                HttpResponseMessage response = await client.GetAsync(urlBase);
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();

                personajes = JsonSerializer.Deserialize<List<Personaje>>(jsonString);
                personajes =  personajes.FindAll(p => p.species != "unknown");

                if (aleatorio) personajes.Barajar(); // retorno personajes aleatorios en caso de pedirlo

                personajes = personajes?.Take(cantidad).ToList();
            }
            return personajes;
        }
    }
}