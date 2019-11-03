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

            RGB r = h.ToRGB();

            Console.WriteLine(h.ToRGB().ToString());
            Console.WriteLine(r.ToHSV().ToString());

            Console.ReadLine();
        }
    }
}
