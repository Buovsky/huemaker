using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorDimensions
{

    class HSV
    {
        public int hue;
        public float saturation;
        public float value;

        public HSV(int h, float s, float v)
        {
            if (h >= 0 && h <= 359)
            {
                hue = h;
            }
            else
            {
                throw new ArgumentException();
            }

            if (s >= 0 && s <= 1)
            {
                saturation = s;
            }
            else
            {
                throw new ArgumentException();
            }

            if (v >= 0 && v <= 1)
            {
                value = v;
            } 
            else 
            {
                throw new ArgumentException();
            }
        }

        public RGB ToRGB()
        {
            // algorithm based on
            // https://en.wikipedia.org/wiki/HSL_and_HSV#HSV_to_RGB
            // c = chroma
            float c = value * saturation;

            // h = chroma prime
            float h = hue / 60;
            float x = c * (1 - ((h % 2) - 1));

            float r, g, b;

            if (h >= 0 && h <= 1)
            {
                r = c;
                g = x;
                b = 0;
            } 
            else if (h > 1 && h <= 2)
            {
                r = x;
                g = c;
                b = 0;
            }
            else if (h > 2 && h <= 3)
            {
                r = 0;
                g = c;
                b = x;
            }
            else if (h > 3 && h <= 4)
            {
                r = 0;
                g = x;
                b = c;
            }
            else if (h > 4 && h <= 5)
            {
                r = x;
                g = 0;
                b = c;
            }
            else if (h > 5 && h <= 6)
            {
                r = c;
                g = 0;
                b = x;
            } 
            else
            {
                r = 0;
                g = 0;
                b = 0;
            }

            float m = value - c;

            return new RGB(Convert.ToInt32((r + m) * 100), Convert.ToInt32((g + m) * 100), Convert.ToInt32((b + m) * 100));
        }

        public override string ToString()
        {
            return $"{hue}, {saturation}, {value}";
        }
    }

    class RGB
    {
        public int red;
        public int green;
        public int blue;

        public RGB(int r, int g, int b)
        {
            if (r >= 0 && r <= 255)
            {
                red = r;
            }
            else
            {
                throw new ArgumentException();
            }

            if (g >= 0 && g <= 255)
            {
                green = g;
            }
            else
            {
                throw new ArgumentException();
            }

            if (b >= 0 && b <= 255)
            {
                blue = b;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public HSV ToHSV()
        {
            // get HSL
            System.Drawing.Color col = System.Drawing.Color.FromArgb(red, green, blue);
            int hue = Convert.ToInt32(col.GetHue());
            float saturation = (float)Math.Round(Convert.ToDouble(col.GetSaturation()), 2);

            // convert HSL to HSV
            float lightness = (float)Math.Round(Convert.ToDouble(col.GetBrightness()), 2);

            // saturation helper (since hsl is still 3d, has different model - doubled for light and shades)
            float s = saturation * (lightness < .5 ? lightness : 1 - lightness);
            saturation = 2 * s / (lightness + s);
            float value = lightness + saturation;

            return new HSV(hue % 360, saturation > 1 ? 1 : saturation, value > 1 ? 1 : value);
        }

        public override string ToString()
        {
            return $"{red}, {green}, {blue}";
        }
    }

    class Color
    {
        public HSV hsvDimension;
        public RGB rgbDimension;

        public Color(HSV h, RGB r)
        {
            hsvDimension = h;
            rgbDimension = r;
        }
    }
}
