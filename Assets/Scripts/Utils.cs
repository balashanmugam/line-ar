using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LineAR {
    public static class Utils {
        /// <summary>
        /// Bala's bresnham algorithm, returns a set of Vector3 points which can be used to
        /// draw the circle.
        /// </summary>
        /// <param name="centre"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static List<Vector3> Circle(Vector3 centre, float radius = 1) {
            // Step 1
            // Set initial conditions
            Vector3 point = new Vector3(0, radius);

            float d = 3 - (2 * radius);

            // step 2
            // Add points to all 8 quadrants
            // q will contain the points of all the points in all 8 quadrants
            int stepSize = 50;

            float incrementor = radius / stepSize;

            List<List<Vector3>> q = new List<List<Vector3>>();

            q = Bresnham(q, centre, point);

            // Step 3
            while (point.y >= point.x) {
                point.x += incrementor;
                if (d > 0) {
                    point.y -= incrementor;
                    d = d + 4 * (point.x - point.y) + 10;
                }
                else {
                    d = d + 4 * point.x + 10;
                }

                q = Bresnham(q, centre, point);
            }

            // append and return a final vector
            List<Vector3> result = new List<Vector3>();
            foreach (var a in q) {
                //lol
                result.Concat( a);
            }

            return result;
        }

        private static List<List<Vector3>> Bresnham(List<List<Vector3>> a, Vector3 centre, Vector3 point) {
            a[0].Append(new Vector3(centre.x + point.x, centre.y + point.y)); // x,y
            a[1].Append(new Vector3(centre.x + point.y, centre.y + point.x)); // y,x
            a[2].Append(new Vector3(centre.x - point.y, centre.y + point.x)); // -y,x
            a[3].Append(new Vector3(centre.x - point.x, centre.y + point.y)); // -x,y
            a[4].Append(new Vector3(centre.x - point.x, centre.y - point.y)); // -x,-y
            a[5].Append(new Vector3(centre.x - point.y, centre.y - point.x)); // -y,-x
            a[6].Append(new Vector3(centre.x + point.y, centre.y - point.x)); // y,-x
            a[7].Append(new Vector3(centre.x + point.x, centre.y - point.y)); // x,-y 
            return a;
        }
    }
}