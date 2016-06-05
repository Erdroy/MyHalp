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
        /// Returns the squared length of this vector.
        /// </summary>
        /// <returns></returns>
        public MyScalar LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
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

        /// <summary>
        /// Creates a string representation of the MyVector3.
        /// </summary>
        /// <returns>A string representation of the MyVector3.</returns>
        public override string ToString()
        {
            return "{ X:" + X + " Y: " + Y + " Z: " + Z + " }";
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="a">First vector in the product.</param>
        /// <param name="b">Second vector in the product.</param>
        /// <returns>Resulting dot product.</returns>
        public static float Dot(MyVector3 a, MyVector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        /// <summary>
        /// Computes the distance between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        public static float Distance(MyVector3 a, MyVector3 b)
        {
            var vec = b - a;
            return vec.Length();
        }

        /// <summary>
        /// Computes the squared distance between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        public static float DistanceSquared(MyVector3 a, MyVector3 b)
        {
            var vec = b - a;
            return vec.LengthSquared();
        }

        /// <summary>
        /// Computes an interpolated state between two vectors.
        /// </summary>
        /// <param name="start">Starting location of the interpolation.</param>
        /// <param name="end">Ending location of the interpolation.</param>
        /// <param name="interpolationAmount">Amount of the end location to use.</param>
        /// <returns>Interpolated intermediate state.</returns>
        public static MyVector3 Lerp(MyVector3 start, MyVector3 end, MyScalar interpolationAmount)
        {
            MyScalar startAmount = 1 - interpolationAmount;

            return new MyVector3
            {
                X = start.X*startAmount + end.X*interpolationAmount,
                Y = start.Y*startAmount + end.Y*interpolationAmount,
                Z = start.Z*startAmount + end.Z*interpolationAmount
            };
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