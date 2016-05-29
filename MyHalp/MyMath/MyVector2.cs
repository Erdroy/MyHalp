// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System;
using UnityEngine;

namespace MyHalp.MyMath
{
    /// <summary>
    /// Vector with two components(X, Y).
    /// </summary>
    public struct MyVector2
    {
        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public MyScalar X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public MyScalar Y;
        
        public MyVector2(MyScalar x, MyScalar y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        /// <returns></returns>
        public MyScalar Length()
        {
            return (MyScalar) Math.Sqrt(X*X + Y*Y);
        }

        /// <summary>
        /// Normalize the vector.
        /// </summary>
        public void Normalize()
        {
            var length = Length();
            X *= length;
            Y *= length;
        }

        /// <summary>
        /// Returns normalized vectors.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public MyVector2 Normalized()
        {
            return new MyVector2(X, Y) * Length();
        }

#region OPERATORS

        // ------ VEC X VEC ------ 
        public static MyVector2 operator +(MyVector2 vec1, MyVector2 vec2)
        {
            return new MyVector2(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }

        public static MyVector2 operator -(MyVector2 vec1, MyVector2 vec2)
        {
            return new MyVector2(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        public static MyVector2 operator *(MyVector2 vec1, MyVector2 vec2)
        {
            return new MyVector2(vec1.X * vec2.X, vec1.Y * vec2.Y);
        }

        public static MyVector2 operator /(MyVector2 vec1, MyVector2 vec2)
        {
            return new MyVector2(vec1.X / vec2.X, vec1.Y / vec2.Y);
        }

        // ------ VEC X SCALAR ------ 
        public static MyVector2 operator +(MyVector2 vec1, MyScalar val)
        {
            return new MyVector2(vec1.X + val, vec1.Y + val);
        }

        public static MyVector2 operator -(MyVector2 vec1, MyScalar val)
        {
            return new MyVector2(vec1.X - val, vec1.Y - val);
        }

        public static MyVector2 operator *(MyVector2 vec1, MyScalar val)
        {
            return new MyVector2(vec1.X * val, vec1.Y * val);
        }

        public static MyVector2 operator /(MyVector2 vec1, MyScalar val)
        {
            return new MyVector2(vec1.X / val, vec1.Y / val);
        }

        // ------ VEC = VEC ------ 
        public static implicit operator MyVector2(Vector3 vec)
        {
            return new MyVector2(vec.x, vec.y);
        }

        public static implicit operator Vector3(MyVector2 vec)
        {
            return new Vector3(vec.X, vec.Y);
        }
#endregion
    }
}