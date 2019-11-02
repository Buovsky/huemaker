using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorDimensions
{
    class Program
    {
        static void Main(string[] args)
        {
            HSV h = new HSV(40, 0.4f, 0.4f);
            RGB r = new RGB(160, 40, 255);

            Console.WriteLine(h.ToRGB().ToString());
            Console.WriteLine(r.ToHSV().ToString());

            System.Drawing.Color col = System.Drawing.Color.FromArgb(255, 160, 40, 255);
            Console.WriteLine($"{col.GetHue()}, {col.GetSaturation()}, {col.GetBrightness()}");

            Console.ReadLine();
        }
    }
}
