using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class Figure
    {
         public List<Point> plist;

        public virtual void Draw()
        {
            Console.ForegroundColor = ConsoleColor.White;

            foreach (Point p in plist)
            {
                p.Draw();
            }
        }

        internal bool IsHit(Figure figure)
        {
            foreach (var p in plist)
            {
                if (figure.IsHit(p))
                {
                    return true;
                }
            }
            return false;
            
        }

        private bool IsHit(Point point)
        {
            foreach (var p in plist)
            {
                if (p.IsHit(point)) { return true; }
            }
            return false;
            
        }

    }
}
