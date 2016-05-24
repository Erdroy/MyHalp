// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyColor class - helps with colors.
    /// </summary>
    public struct MyColor
    {
        public float R, G, B, A;

        public MyColor(float r, float g, float b, float a)
        {
            R = r; G = g; B = b; A = a;
        }

        public MyColor(Color color)
        {
            R = color.r; B = color.b; G = color.g; A = color.a;
        }

        public Color GetColor()
        {
            return new Color(R, G, B, A);
        }
    }
}
