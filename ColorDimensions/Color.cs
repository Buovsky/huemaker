using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorDimensions
{

    class HSV
    {
        public float hue;
        public float saturation;
        public float value;

        public HSV(float h, float s, float v)
        {
            if (h >= 0 && h <= 359.99f)
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
            // https://www.rapidtables.com/convert/color/hsv-to-rgb.html
            // c = chroma
            float c = value * saturation;

            // h = chroma prime
            float x = c * (1 - Math.Abs((hue / 60) % 2 - 1));

            float r, g, b;

            if (hue >= 0 && hue <= 60)
            {
                r = c;
                g = x;
                b = 0;
            } 
            else if (hue > 60 && hue <= 120)
            {
                r = x;
                g = c;
                b = 0;
            }
            else if (hue > 120 && hue <= 180)
            {
                r = 0;
                g = c;
                b = x;
            }
            else if (hue > 180 && hue <= 240)
            {
                r = 0;
                g = x;
                b = c;
            }
            else if (hue > 240 && hue <= 300)
            {
                r = x;
                g = 0;
                b = c;
            }
            else if (hue > 300 && hue <= 360)
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

            return new RGB(Convert.ToInt32((r + m) * 255), Convert.ToInt32((g + m) * 255), Convert.ToInt32((b + m) * 255));
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
            float hue = col.GetHue();
            float saturation = col.GetSaturation();
            float lightness = col.GetBrightness();

            // convert HSL to HSV
            // saturation helper (since hsl is still 3d, has different model - doubled for light and shades)
            float s = saturation * (lightness < 0.5f ? lightness : 1 - lightness);
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
