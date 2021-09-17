using System;

namespace RPGConsole
{
    public class Vector2
    {
        //general vector class
        public float x, y;
        public Vector2(float x, float y) { this.x = x; this.y = y; }
        //Vector2 Zero = new Vector2(0, 0);
        public string ToString()
        {
            return "X: " + x + " Y: " + y;
        }

        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
            return (float)MathF.Sqrt((v1 * v1) + (v2 * v2));
        }
    }
}
