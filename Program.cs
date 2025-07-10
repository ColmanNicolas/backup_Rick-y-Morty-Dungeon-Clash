using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
using UIclass;

using System;
using System.Drawing; // Necesitás instalar System.Drawing.Common si usás .NET Core
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using ImageClass;
using System.ComponentModel.DataAnnotations;
using UtilsClass;
using System.ComponentModel;
using PersonajeClass;

namespace RickAndMortyApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            List<Personaje> personajes = new List<Personaje>();
            List<Personaje> personajesVivosEnPartida = new List<Personaje>();
            List<Personaje> personajesDerrotados = new List<Personaje>();
            List<Personaje> personajesDerrotadosPorElJugador = new List<Personaje>();

            /*
                        UI.BarraDeVidaUI(100, 100);  // 100%
                        UI.BarraDeVidaUI(100, 45);   // 45%
                        UI.BarraDeVidaUI(100, 1);    // 0% 
                        */
            int opcionPrimaria, opcionSecundaria, opcionTerciaria, cantidadPersonajes;
            string? nombreJugador;
            Personaje? personajeJugador = new Personaje();
            Personaje? personajeGanador = new Personaje();

            do
            {
                UI.MenuInicialUI();
                opcionPrimaria = Utils.validarOpcionMenu(0, 3, "\nSu opcion: ");

                switch (opcionPrimaria)
                {

                    case 1:   // logica partida nueva
                        personajesVivosEnPartida.Clear();
                        personajes = await ObtenerPersonajesAPI(812, false);  //comunicacion con la api

                        personajes.ForEach(p => p.inicializarEstadisticas());

                        nombreJugador = UI.ElegirNombreJugador();

                        UI.ElegirNuevoPersonajeUI();
                        opcionSecundaria = Utils.validarOpcionMenu(1, 3, "\nSu opcion: ");

                        cantidadPersonajes = Utils.validarTamanioPartida();

                        personajeJugador = UsuarioEligeSuPersonaje(opcionSecundaria, ref personajes, cantidadPersonajes);

                        FiltrarPersonajesParaNuevaPartida(personajes, ref personajesVivosEnPartida, personajeJugador, cantidadPersonajes);

                        //empieza la partida. logica de menu
                        do
                        {
                            UI.MenuPrincipalUI(nombreJugador, personajesVivosEnPartida.Count);
                            opcionTerciaria = Utils.validarOpcionMenu(0, 7, "\nSu opcion: ");
                            switch (opcionTerciaria)
                            {
                                case 1:
                                    personajeJugador.MostrarUnPersonaje();
                                    break;
                                case 2:
                                    Personaje.MostrarTablaDeVentajas();
                                    break;
                                case 3:
                                    Personaje personajeVencido = EnfrentarDosPersonajes(personajesVivosEnPartida[0], personajesVivosEnPartida[1]);
                                    personajesDerrotados.Add(personajeVencido);
                                    personajesVivosEnPartida.Remove(personajeVencido);
                                    break;
                                case 4:
                                    //mostrar combates del corriente turno
                                    int cantJugadoresRestantes = personajesVivosEnPartida.Count;
                                    if (cantJugadoresRestantes > 1)
                                    {

                                        Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                                        Console.WriteLine("|             SYSTEM MESSAGE: ROUND START!        |");
                                        Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                                        Console.WriteLine($"\n  [JUGADORES VIVOS]: {cantJugadoresRestantes}   [DUELOS EN ESTA RONDA]: {cantJugadoresRestantes / 2}  ");
                                        Utils.GenerarPausaDeSegundos(1);

                                        Console.WriteLine("\n+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                                        Console.WriteLine("|               PROXIMOS ENFRENTAMIENTOS          |");
                                        Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");

                                        for (int i = 0; i < cantJugadoresRestantes - 1; i += 2)
                                        {
                                            string jugador1Nombre = personajesVivosEnPartida[i].name;
                                            string jugador2Nombre = personajesVivosEnPartida[i + 1].name;

                                            // Ajusta el texto "PLAYER UNIT" para que solo aparezca si es el primer personaje
                                            string display1 = (i == 0) ? $"{jugador1Nombre.ToUpper()} <JUGADOR>" : $"{jugador1Nombre.ToUpper()} <IA>";

                                            Console.Write($"\n  [DUELO #{(i / 2) + 1:D2}]: ");
                                            Console.Write($"PESONAJE: {display1,-30} "); // Usamos alineación a la izquierda con padding
                                            Console.WriteLine($"OPONENTE: {jugador2Nombre.ToUpper()} <IA>"); // Usamos alineación a la izquierda con padding
                                            Utils.GenerarPausaDeSegundos(0.005); // Pausa rápida
                                        }

                                        Console.WriteLine("\n+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                                    }
                                    else if (cantJugadoresRestantes == 1)
                                    {
                                        Console.WriteLine("PERSONAJE GANADOR DE LA PARTIDA");
                                        personajesVivosEnPartida[0].MostrarUnPersonaje();
                                    }

                                    Utils.GenerarPausaDeSegundos(1.5);
                                    break;
                                case 5:
                                    // ejecutar duelos restantes o hacer partida rapida (?)
                                    break;
                                case 6:
                                    int id = Utils.validarOpcionMenu(1, 826, "\nIngrese el identificador(ID) de un personaje(1 al 826): ");
                                    int anchoMaximo = Utils.validarOpcionMenu(150, 350, "\nIngrese el ancho maximo que tendra la imagen (150 al 350): ");

                                    await ImageToASCII.MostrarPersonajePorId(id, anchoMaximo);

                                    break;
                                case 7:
                                    // guardar partida en archivo
                                    break;
                                default:
                                    break;
                            }

                            if (opcionTerciaria != 0) Utils.PresioneKparaContinuar();

                        } while (opcionTerciaria != 0);

                        break;
                    case 2: // Logica cargar partida
                        break;
                    case 3:
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
                if (opcionPrimaria != 0) Utils.PresioneKparaContinuar();

            } while (opcionPrimaria != 0);

            Console.Clear();
        }
        public static Personaje UsuarioEligeSuPersonaje(int opcionElegirPersonaje, ref List<Personaje> personajes, int cantidadPersonajes)
        {
            Personaje personajeJugador;
            int auxContador = 0, identificadorPersonaje;



            if (opcionElegirPersonaje == 3)
            {
                personajes.Barajar();


                personajeJugador = personajes[0];
                Utils.GenerarPausaDeSegundos(1);
                Console.Write("\nSu personaje fue asignado: ");
            }
            else
            {
                if (opcionElegirPersonaje == 2) //mezclar y limitar lista de personajes solo para opcion 2
                {
                    personajes.Barajar();
                    personajes = personajes.Take(cantidadPersonajes).ToList();
                }

                do
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
                    identificadorPersonaje = Utils.validarOpcionMenu(1, 825, "\nIngrese el identificador(ID) del personaje que quiera usar: ");
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

        public static void FiltrarPersonajesParaNuevaPartida(List<Personaje> personajesDisponibles, ref List<Personaje> personajesSeleccionados, Personaje personajeJugador, int cantidadPersonajes)
        {
            personajesDisponibles.Remove(personajeJugador);
            personajesDisponibles.Barajar();
            personajesSeleccionados.Add(personajeJugador);

            for (int i = 0; i < cantidadPersonajes - 1; i++)
            {
                personajesSeleccionados.Add(personajesDisponibles[i]);
            }
        }

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
                personajes = personajes.FindAll(p => p.species != "unknown");

                if (aleatorio) personajes.Barajar(); // retorno personajes aleatorios en caso de pedirlo

                personajes = personajes.Take(cantidad).ToList();
            }
            return personajes;
        }

    }
}
