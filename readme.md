# Rick y Morty: Dungeon Clash

**Rick y Morty: Dungeon Clash** es un juego de rol de estrategia y combates basado en la serie *Rick and Morty*. Desarrollado en C# con .NET, el juego simula un torneo de eliminación donde el objetivo principal es ser el último personaje en pie.

---

## 🎮 Descripción General

- **Nombre del juego**: Rick y Morty: Dungeon Clash
- **Género**: Estrategia por turnos / RPG
- **Plataforma**: Consola (C# / .NET)
- **Inspiración**: Serie *Rick and Morty*
- **Objetivo**: Ser el último personaje en pie tras múltiples rondas de combate.

## 🔧 Tecnologías y Recursos

- Lenguaje: **C#**
- Framework: **.NET**
- Librerías: Manejo de archivos, Streams binarios, Serialización JSON
- API utilizada: [Rick and Morty API](https://rickandmortyapi.com/)

## ⚔️ Mecánicas del Juego

- **Formato del torneo**: Eliminación directa (combates de a 2)
- **Tamaño de la partida**: 2, 4, 8, 16, 32, 64, 128, 258 o 512 personajes
- **Modos de selección del personaje jugador**:
  1. Elegir de la lista completa (813 personajes)
  2. Elegir de una lista aleatoria basada en el tamaño de la partida
  3. Selección completamente aleatoria

## 🧬 Estadísticas de los personajes

Cada personaje cuenta con estadísticas aleatorias:
- HP
- Ataque base
- Velocidad
- Inteligencia
- Defensa

Estas estadísticas se balancean según la **clase del personaje**:
- Human
- Humanoid
- Robot
- Alien
- Disease
- Mythological Creature
- Poopybutthole
- Cronenberg
- Animal

## 🗺️ Desarrollo del juego

1. **Creación de la partida**: se elige la cantidad de jugadores para la partida y luego se le permite al jugador escoger su personaje.
2. **Menú principal del salón del trono**:
   - 1. Ver personaje
   - 2. Ver tabla de ventajas entre clases
   - 3. Iniciar próximo enfrentamiento
   - 4. Ver tabla de enfrentamientos
   - 5. Ver detalle de la partida
   - 6. Ver imagen de personaje en consola (blanco y negro o color)
   - 7. Guardar la partida


## 📜 Detalle del Menú Principal

### 1. Ver personaje
Permite visualizar el personaje que controla el jugador en la partida. Muestra estadísticas actuales, especie, nivel y mejoras obtenidas en el transcurso del torneo.

### 2. Ver tabla de ventajas
Muestra una tabla que indica las especies sobre las que cada clase tiene ventaja. Estas ventajas afectan la probabilidad de ataques críticos (más alta) y reducen la capacidad del rival de esquivar o bloquear. Los ataques críticos no pueden ser esquivados.

**Ventajas entre especies:**

| Especie              | Ventajas sobre                             |
|----------------------|--------------------------------------------|
| Human                | Robot, Disease                             |
| Robot                | Animal, Humanoid                           |
| Alien                | Robot, Mythological Creature               |
| Poopybutthole        | Cronenberg, Mythological Creature          |
| Mythological Creature| Animal, Cronenberg                         |
| Animal               | Disease, Poopybutthole                     |
| Disease              | Alien, Humanoid                            |
| Humanoid             | Poopybutthole, Human                       |
| Cronenberg           | Human, Alien                               |

### 3. Iniciar próximo enfrentamiento
Inicia el combate entre los dos personajes siguientes en la ronda. El primer enfrentamiento de cada ronda siempre se presenta con una interfaz gráfica y dinámica que incluye:
- Barras de HP
- Detalles de ataque y defensa
- Resultado del ataque: efectivo, crítico o falló

El perdedor queda fuera de la partida. El ganador sube de nivel. Si el jugador gana, puede elegir una de tres variantes de mejoras aleatorias que afectan 4 de sus 5 estadísticas.

### 4. Ver tabla de enfrentamientos
Permite ver los enfrentamientos programados para la ronda actual. Se muestra el nombre, especie y si el personaje es IA o jugador.

### 5. Ver detalle de la partida
Muestra un resumen completo del estado de la partida, incluyendo:
- Personaje jugador (estadísticas y logros)
- Lista de personajes vivos
- Lista de personajes eliminados
- Personajes eliminados por el jugador
- Estadísticas completas de todos los personajes

### 6. Ver imagen de un personaje
Permite ver la imagen de cualquier personaje existente en la API de Rick and Morty (ID del 1 al 826) en formato ASCII en consola.

Pasos:
1. Ingresar el ID del personaje
2. Elegir el ancho de la imagen (150–350 caracteres)
3. Elegir si será en color o en blanco y negro
4. Seleccionar uno de los 4 estilos visuales (ramas de caracteres diferentes)

Para apreciar bien imágenes anchas, se recomienda usar **zoom out** en la consola.

### 7. Guardar la partida
Guarda el estado actual de la partida con el nombre del jugador. Si ya existe una partida con ese nombre, se sobrescribe. El guardado es **sensible a mayúsculas y minúsculas**.

## 🕹️ Cómo se juega

- Se consulta el detalle del personaje propio con opción 1.
- Se analiza el contrincante y demas enfrentamientos con opción 4 y 5.
- Se visualiza si existe ventaja de clase con opción 2.
- Se inicia el combate con opción 3.

### Interfaz de combate

- Se muestra cada turno: HP, ataque, defensa, y resultado del ataque.
- Resultados posibles:
  - **Ataque efectivo**: Daño base ±10%
  - **Ataque crítico**: Daño base +50%
  - **Falló**: Sin daño

El ganador sube de nivel. Si es el jugador, elige entre tres mejoras aleatorias. Si no, la mejora es aleatoria.

## 💾 Guardado y reanudación

- Se puede guardar el estado de la partida en cualquier momento (opción 7).
- El juego puede reanudarse desde donde se dejó tras cerrar el programa.

## 🏆 Final del juego

El juego finaliza cuando solo queda un personaje con vida. Este es coronado con el **Trono de Hierro**.

---

## 🏫 Contexto académico

Este juego fue desarrollado como proyecto integrador de la materia **Taller de Lenguajes I** de la **FACET - UNT**. Se abordaron los siguientes contenidos:

- Estructura de programas en C#
- Manipulación de archivos y directorios
- Streams binarios
- Serialización y deserialización JSON
- Peticiones asincrónicas a APIs públicas

---

## Documentación de `ASCIIcolorClass`

## Resumen General 📝

Este namespace contiene la clase estática `AdvancedConsoleColorMapper`, una utilidad diseñada para resolver un problema común en aplicaciones de consola: encontrar el `System.ConsoleColor` más cercano a un `System.Drawing.Color` arbitrario.

La comparación de colores se realiza en el espacio de color **CIELAB (L*a*b*)**, que está diseñado para aproximarse a la percepción visual humana. Esto produce resultados mucho más precisos y naturales que una simple comparación en el espacio RGB.

## Funcionamiento Interno ⚙️

El proceso se puede resumir en los siguientes pasos:

1.  **Inicialización (una sola vez)**:
    * Se tiene un mapa predefinido de los 16 `ConsoleColor` a sus valores RGB.
    * En el constructor estático, cada uno de estos colores RGB se convierte a su equivalente en el espacio CIELAB y se guarda en un diccionario (`ConsoleColorLabMap`) para un acceso rápido y evitar cálculos repetidos.

2.  **Búsqueda del Color más Cercano**:
    * El método público `GetClosestConsoleColor(Color targetColor)` toma un color RGB como entrada.
    * Este color de entrada también se convierte al espacio CIELAB.
    * Se calcula la "distancia" perceptual entre el color de entrada y cada uno de los 16 colores de consola precalculados usando la fórmula de diferencia de color **CIE76 (Delta E)**.
    * Se devuelve el `ConsoleColor` que tenga la distancia (diferencia) mínima.

## 📐 Detalle Matemático: Delta E (CIE76)

Para determinar qué `ConsoleColor` es perceptualmente más cercano a un color dado, se utiliza una fórmula matemática conocida como **Delta E (CIE76)**. Esta fórmula mide la diferencia entre dos colores en el espacio **CIELAB (L\*a\*b\*)**, un modelo diseñado para representar el color de manera más cercana a la percepción humana.

### Fórmula

\[
\Delta E = \sqrt{(L_1 - L_2)^2 + (a_1 - a_2)^2 + (b_1 - b_2)^2}
\]

Donde:

- \( L_1, a_1, b_1 \) son las coordenadas del color de entrada convertido a CIELAB.
- \( L_2, a_2, b_2 \) son las coordenadas de un color `ConsoleColor` también convertido a CIELAB.
- \( \Delta E \) es la "distancia perceptual" entre ambos colores.

### Interpretación

Esta fórmula es una **distancia euclidiana tridimensional** aplicada al espacio perceptual de colores. Cuanto más bajo sea el valor de \( \Delta E \), más similares serán los colores desde el punto de vista del ojo humano.

Este enfoque es mucho más preciso que comparar colores en el espacio RGB, ya que este último **no es uniforme perceptualmente**: diferencias numéricas iguales no implican diferencias visuales iguales.


## Uso Básico 🚀

Para usar la clase, simplemente llama al método estático `GetClosestConsoleColor` y pásale el color que deseas aproximar.

```csharp
// Ejemplo: Encontrar el color de consola más cercano a un naranja quemado.
Color miColor = Color.FromArgb(210, 105, 30); // Color "Chocolate"
ConsoleColor colorMasCercano = AdvancedConsoleColorMapper.GetClosestConsoleColor(miColor);

// El resultado probablemente será ConsoleColor.DarkYellow
Console.WriteLine($"El color más cercano es: {colorMasCercano}");
```

## 📂 Estructura del Proyecto
```
RickAndMortyDungeonClash/
│
├── Program.cs                    # Punto de entrada del programa
├── Game/
│   ├── Gameplay/                 # Lógica general del juego y su ejecución
│   └── UIUX/                     # Interfaces graficas y mensajes por consola
│
├── Almacenamiento/
│   └── Almacenamientos.cs       # Control de opciones del menú principal
│
├── Models/
│   ├── Personaje.cs             # Representación de los personajes (atributos, clase, etc.)
│   └── Partida.cs               # Clase para gestionar la persistencia de la partida creada
│
├── Api/
│   └── Api.cs                   # Conexión a la API de Rick and Morty
│
├── Utils/
│   ├── imageToASCII/
│   │   ├── imageToASCII.cs      # Conversión de imágenes a ASCII para consola
│   │   └── ASCIIcolor.cs        # Calculo del color mas parecido de consola para cada pixel
│   └── Utils.cs                 # Métodos auxiliares para salida por consola, validacion de entradas y pausas de ejecucion del programa
│
├── partidasGuardadas/           # Carpeta donde se almacenan las partidas guardadas, se crea automaticamente
│   
├── .gitignore                   # Archivos a ignorar por Git
│
└── README.md                    # Documentación del proyecto
```
---

© 2025 - Nicolás Colman

---


