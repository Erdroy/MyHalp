// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System;
using UnityEngine;

namespace MyHalp.MyMath
{
    public struct MyQuaternion
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
        
        public MyQuaternion(MyScalar x, MyScalar y, MyScalar z, MyScalar w)
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
        public MyQuaternion Normalized()
        {
            return new MyQuaternion(X, Y, Z, W) * Length();
        }

#region OPERATORS

        // ------ VEC X VEC ------ 
        public static MyQuaternion operator +(MyQuaternion vec1, MyQuaternion vec2)
        {
            return new MyQuaternion(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z, vec1.W + vec2.W);
        }

        public static MyQuaternion operator -(MyQuaternion vec1, MyQuaternion vec2)
        {
            return new MyQuaternion(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z, vec1.W - vec2.W);
        }

        public static MyQuaternion operator *(MyQuaternion vec1, MyQuaternion vec2)
        {
            return new MyQuaternion(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z, vec1.W * vec2.W);
        }

        public static MyQuaternion operator /(MyQuaternion vec1, MyQuaternion vec2)
        {
            return new MyQuaternion(vec1.X / vec2.X, vec1.Y / vec2.Y, vec1.Z / vec2.Z, vec1.W / vec2.W);
        }

        // ------ VEC X SCALAR ------ 
        public static MyQuaternion operator +(MyQuaternion vec1, MyScalar val)
        {
            return new MyQuaternion(vec1.X + val, vec1.Y + val, vec1.Z + val, vec1.W + val);
        }

        public static MyQuaternion operator -(MyQuaternion vec1, MyScalar val)
        {
            return new MyQuaternion(vec1.X - val, vec1.Y - val, vec1.Z - val, vec1.W - val);
        }

        public static MyQuaternion operator *(MyQuaternion vec1, MyScalar val)
        {
            return new MyQuaternion(vec1.X * val, vec1.Y * val, vec1.Z * val, vec1.W * val);
        }

        public static MyQuaternion operator /(MyQuaternion vec1, MyScalar val)
        {
            return new MyQuaternion(vec1.X / val, vec1.Y / val, vec1.Z / val, vec1.W / val);
        }

        // ------ VEC = VEC ------ 
        public static implicit operator MyQuaternion(Quaternion vec)
        {
            return new MyQuaternion(vec.x, vec.y, vec.z, vec.w);
        }

        public static implicit operator Quaternion(MyQuaternion vec)
        {
            return new Quaternion(vec.X, vec.Y, vec.Z, vec.W);
        }
#endregion
    }
}