// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System;
using UnityEngine;

namespace MyHalp.MyMath
{
    /// <summary>
    /// Vector with four components(X, Y, Z, W).
    /// </summary>
    public struct MyVector4
    {
        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public MyScalar X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public MyScalar Y;

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public MyScalar Z;

        /// <summary>
        /// The W component of the vector.
        /// </summary>
        public MyScalar W;
        
        public MyVector4(MyScalar x, MyScalar y, MyScalar z, MyScalar w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        /// <returns></returns>
        public MyScalar Length()
        {
            return (MyScalar) Math.Sqrt(X*X + Y*Y + Z*Z + W*W);
        }

        /// <summary>
        /// Normalize the vector.
        /// </summary>
        public void Normalize()
        {
            var length = Length();
            X *= length;
            Y *= length;
            Z *= length;
            W *= length;
        }

        /// <summary>
        /// Returns normalized vectors.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public MyVector4 Normalized()
        {
            return new MyVector4(X, Y, Z, W) * Length();
        }

#region OPERATORS

        // ------ VEC X VEC ------ 
        public static MyVector4 operator +(MyVector4 vec1, MyVector4 vec2)
        {
            return new MyVector4(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z, vec1.W + vec2.W);
        }

        public static MyVector4 operator -(MyVector4 vec1, MyVector4 vec2)
        {
            return new MyVector4(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z, vec1.W - vec2.W);
        }

        public static MyVector4 operator *(MyVector4 vec1, MyVector4 vec2)
        {
            return new MyVector4(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z, vec1.W * vec2.W);
        }

        public static MyVector4 operator /(MyVector4 vec1, MyVector4 vec2)
        {
            return new MyVector4(vec1.X / vec2.X, vec1.Y / vec2.Y, vec1.Z / vec2.Z, vec1.W / vec2.W);
        }

        // ------ VEC X SCALAR ------ 
        public static MyVector4 operator +(MyVector4 vec1, MyScalar val)
        {
            return new MyVector4(vec1.X + val, vec1.Y + val, vec1.Z + val, vec1.W + val);
        }

        public static MyVector4 operator -(MyVector4 vec1, MyScalar val)
        {
            return new MyVector4(vec1.X - val, vec1.Y - val, vec1.Z - val, vec1.W - val);
        }

        public static MyVector4 operator *(MyVector4 vec1, MyScalar val)
        {
            return new MyVector4(vec1.X * val, vec1.Y * val, vec1.Z * val, vec1.W * val);
        }

        public static MyVector4 operator /(MyVector4 vec1, MyScalar val)
        {
            return new MyVector4(vec1.X / val, vec1.Y / val, vec1.Z / val, vec1.W / val);
        }

        // ------ VEC = VEC ------ 
        public static implicit operator MyVector4(Vector4 vec)
        {
            return new MyVector4(vec.x, vec.y, vec.z, vec.w);
        }

        public static implicit operator Vector4(MyVector4 vec)
        {
            return new Vector4(vec.X, vec.Y, vec.Z, vec.W);
        }
#endregion
    }
}