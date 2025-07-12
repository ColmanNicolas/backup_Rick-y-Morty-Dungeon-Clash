using System.Drawing;

namespace ASCIIcolorClass
{
    /// <summary>
    /// Representa un color en el espacio de color CIE L*a*b*.
    /// </summary>
    public struct LabColor
    {
        /// <summary>
        /// Componente de luminosidad (Lightness).
        /// </summary>
        public double L;

        /// <summary>
        /// Componente de verde a rojo (Green-Red).
        /// </summary>
        public double A;

        /// <summary>
        /// Componente de azul a amarillo (Blue-Yellow).
        /// </summary>
        public double B;
    }

    /// <summary>
    /// Proporciona métodos para encontrar el color de consola más cercano a un color RGB
    /// utilizando el espacio de color CIELAB para una mejor percepción de la similitud de colores.
    /// </summary>
    public static class AdvancedConsoleColorMapper
    {
        // 1. CAMPOS PRIVADOS
        // Mapeo de ConsoleColor a sus valores RGB.
        private static readonly Dictionary<ConsoleColor, Color> ConsoleColorRgbMap = new Dictionary<ConsoleColor, Color>
        {
            { ConsoleColor.Black, Color.FromArgb(0, 0, 0) },
            { ConsoleColor.DarkBlue, Color.FromArgb(0, 0, 128) },
            { ConsoleColor.DarkGreen, Color.FromArgb(0, 128, 0) },
            { ConsoleColor.DarkCyan, Color.FromArgb(0, 128, 128) },
            { ConsoleColor.DarkRed, Color.FromArgb(128, 0, 0) },
            { ConsoleColor.DarkMagenta, Color.FromArgb(128, 0, 128) },
            { ConsoleColor.DarkYellow, Color.FromArgb(128, 128, 0) },
            { ConsoleColor.Gray, Color.FromArgb(192, 192, 192) },
            { ConsoleColor.DarkGray, Color.FromArgb(128, 128, 128) },
            { ConsoleColor.Blue, Color.FromArgb(0, 0, 255) },
            { ConsoleColor.Green, Color.FromArgb(0, 255, 0) },
            { ConsoleColor.Cyan, Color.FromArgb(0, 255, 255) },
            { ConsoleColor.Red, Color.FromArgb(255, 0, 0) },
            { ConsoleColor.Magenta, Color.FromArgb(255, 0, 255) },
            { ConsoleColor.Yellow, Color.FromArgb(255, 255, 0) },
            { ConsoleColor.White, Color.FromArgb(255, 255, 255) }
        };

        // Almacena la versión CIELAB de los colores de consola para evitar recalcular.
        private static readonly Dictionary<ConsoleColor, LabColor> ConsoleColorLabMap;

        // 2. CONSTRUCTOR ESTÁTICO
        // Se ejecuta una sola vez para inicializar los campos estáticos.
        static AdvancedConsoleColorMapper()
        {
            ConsoleColorLabMap = new Dictionary<ConsoleColor, LabColor>();
            foreach (var entry in ConsoleColorRgbMap)
            {
                ConsoleColorLabMap.Add(entry.Key, RgbToLab(entry.Value));
            }
        }

        // 3. MÉTODOS PÚBLICOS
        /// <summary>
        /// Encuentra el ConsoleColor más cercano a un color System.Drawing.Color dado
        /// usando la distancia CIE76 en el espacio CIELAB.
        /// </summary>
        /// <param name="targetColor">El color RGB a comparar.</param>
        /// <returns>El ConsoleColor más cercano.</returns>
        public static ConsoleColor GetClosestConsoleColor(Color targetColor)
        {
            LabColor targetLab = RgbToLab(targetColor);
            ConsoleColor closestConsoleColor = ConsoleColor.Black;
            double minDistance = double.MaxValue;

            foreach (var entry in ConsoleColorLabMap)
            {
                double currentDistance = CalculateCie76Distance(targetLab, entry.Value);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestConsoleColor = entry.Key;
                }
            }

            return closestConsoleColor;
        }

        // 4. MÉTODOS PRIVADOS DE AYUDA
        /// <summary>
        /// Convierte un color RGB a CIELAB.
        /// </summary>
        /// <remarks>
        /// Esta es una implementación simplificada asumiendo el espacio sRGB y el iluminante D65.
        /// </remarks>
        private static LabColor RgbToLab(Color rgb)
        {
            // Normalizar RGB a un rango de 0-1
            double r = rgb.R / 255.0;
            double g = rgb.G / 255.0;
            double b = rgb.B / 255.0;

            // Corrección gamma para convertir sRGB a RGB lineal
            r = (r > 0.04045) ? Math.Pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
            g = (g > 0.04045) ? Math.Pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
            b = (b > 0.04045) ? Math.Pow((b + 0.055) / 1.055, 2.4) : b / 12.92;

            // Convertir a XYZ usando la matriz de transformación para sRGB
            double x = r * 0.4124564 + g * 0.3575761 + b * 0.1804375;
            double y = r * 0.2126729 + g * 0.7151522 + b * 0.0721750;
            double z = r * 0.0193339 + g * 0.1191920 + b * 0.9503041;

            // Normalizar XYZ con el punto blanco de referencia D65
            x /= 0.95047;
            y /= 1.00000;
            z /= 1.08883;

            // Convertir XYZ a CIELAB
            x = (x > 0.008856) ? Math.Pow(x, 1.0 / 3.0) : (7.787 * x) + (16.0 / 116.0);
            y = (y > 0.008856) ? Math.Pow(y, 1.0 / 3.0) : (7.787 * y) + (16.0 / 116.0);
            z = (z > 0.008856) ? Math.Pow(z, 1.0 / 3.0) : (7.787 * z) + (16.0 / 116.0);

            LabColor lab;
            lab.L = (116.0 * y) - 16.0;
            lab.A = 500.0 * (x - y);
            lab.B = 200.0 * (y - z);

            return lab;
        }

        /// <summary>
        /// Calcula la distancia de color CIE76 (Delta E*ab) entre dos colores CIELAB.
        /// </summary>
        private static double CalculateCie76Distance(LabColor lab1, LabColor lab2)
        {
            double deltaL = lab1.L - lab2.L;
            double deltaA = lab1.A - lab2.A;
            double deltaB = lab1.B - lab2.B;
            return Math.Sqrt(deltaL * deltaL + deltaA * deltaA + deltaB * deltaB);
        }
    }
}


