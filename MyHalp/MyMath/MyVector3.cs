// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System;
using UnityEngine;

namespace MyHalp.MyMath
{
    /// <summary>
    /// Vector with three components(X, Y, Z).
    /// </summary>
    public struct MyVector3
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
        
        public MyVector3(MyScalar x, MyScalar y, MyScalar z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        /// <returns></returns>
        public MyScalar Length()
        {
            return (MyScalar) Math.Sqrt(X*X + Y*Y + Z*Z);
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
        }

        /// <summary>
        /// Returns normalized vectors.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public MyVector3 Normalized()
        {
            return new MyVector3(X, Y, Z) * Length();
        }

#region OPERATORS

        // ------ VEC X VEC ------ 
        public static MyVector3 operator +(MyVector3 vec1, MyVector3 vec2)
        {
            return new MyVector3(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }

        public static MyVector3 operator -(MyVector3 vec1, MyVector3 vec2)
        {
            return new MyVector3(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }

        public static MyVector3 operator *(MyVector3 vec1, MyVector3 vec2)
        {
            return new MyVector3(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);
        }

        public static MyVector3 operator /(MyVector3 vec1, MyVector3 vec2)
        {
            return new MyVector3(vec1.X / vec2.X, vec1.Y / vec2.Y, vec1.Z / vec2.Z);
        }

        // ------ VEC X SCALAR ------ 
        public static MyVector3 operator +(MyVector3 vec1, MyScalar val)
        {
            return new MyVector3(vec1.X + val, vec1.Y + val, vec1.Z + val);
        }

        public static MyVector3 operator -(MyVector3 vec1, MyScalar val)
        {
            return new MyVector3(vec1.X - val, vec1.Y - val, vec1.Z - val);
        }

        public static MyVector3 operator *(MyVector3 vec1, MyScalar val)
        {
            return new MyVector3(vec1.X * val, vec1.Y * val, vec1.Z * val);
        }

        public static MyVector3 operator /(MyVector3 vec1, MyScalar val)
        {
            return new MyVector3(vec1.X / val, vec1.Y / val, vec1.Z / val);
        }

        // ------ VEC = VEC ------ 
        public static implicit operator MyVector3(Vector3 vec)
        {
            return new MyVector3(vec.x, vec.y, vec.z);
        }

        public static implicit operator Vector3(MyVector3 vec)
        {
            return new Vector3(vec.X, vec.Y, vec.Z);
        }
#endregion
    }
}