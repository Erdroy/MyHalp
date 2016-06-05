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
        /// Returns the squared length of this vector.
        /// </summary>
        /// <returns></returns>
        public MyScalar LengthSquared()
        {
            return X * X + Y * Y + Z * Z + W * W;
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

        /// <summary>
        /// Creates a string representation of the MyVector4.
        /// </summary>
        /// <returns>A string representation of the MyVector4.</returns>
        public override string ToString()
        {
            return "{ X:" + X + " Y: " + Y + " Z: " + Z + " W: " + W + " }";
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="a">First vector in the product.</param>
        /// <param name="b">Second vector in the product.</param>
        /// <returns>Resulting dot product.</returns>
        public static float Dot(MyVector4 a, MyVector4 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        /// <summary>
        /// Computes the distance between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        public static float Distance(MyVector4 a, MyVector4 b)
        {
            var vec = b - a;
            return vec.Length();
        }

        /// <summary>
        /// Computes the squared distance between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        public static float DistanceSquared(MyVector4 a, MyVector4 b)
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
        public static MyVector4 Lerp(MyVector4 start, MyVector4 end, MyScalar interpolationAmount)
        {
            MyScalar startAmount = 1 - interpolationAmount;

            return new MyVector4
            {
                X = start.X * startAmount + end.X * interpolationAmount,
                Y = start.Y * startAmount + end.Y * interpolationAmount,
                Z = start.Z * startAmount + end.Z * interpolationAmount,
                W = start.W * startAmount + end.W * interpolationAmount
            };
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