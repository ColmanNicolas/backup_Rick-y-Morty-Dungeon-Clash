# Documentación de `ASCIIcolorClass`

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

## Uso Básico 🚀

Para usar la clase, simplemente llama al método estático `GetClosestConsoleColor` y pásale el color que deseas aproximar.

```csharp
// Ejemplo: Encontrar el color de consola más cercano a un naranja quemado.
Color miColor = Color.FromArgb(210, 105, 30); // Color "Chocolate"
ConsoleColor colorMasCercano = AdvancedConsoleColorMapper.GetClosestConsoleColor(miColor);

// El resultado probablemente será ConsoleColor.DarkYellow
Console.WriteLine($"El color más cercano es: {colorMasCercano}");
