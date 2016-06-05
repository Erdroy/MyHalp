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
        /// Returns the squared length of this vector.
        /// </summary>
        /// <returns></returns>
        public MyScalar LengthSquared()
        {
            return X * X + Y * Y;
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

        /// <summary>
        /// Creates a string representation of the MyVector2.
        /// </summary>
        /// <returns>A string representation of the MyVector2.</returns>
        public override string ToString()
        {
            return "{ X:" + X + " Y: " + Y + " }";
        }

        /// <summary>
        /// Computes the dot product of the two vectors.
        /// </summary>
        /// <param name="a">First vector of the dot product.</param>
        /// <param name="b">Second vector of the dot product.</param>
        /// <returns>Dot product of the two vectors.</returns>
        public static float Dot(MyVector2 a, MyVector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        
        /// <summary>
        /// Computes the distance between two two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        public static float Distance(MyVector2 a, MyVector2 b)
        {
            var vec = b - a;
            return vec.Length();
        }

        /// <summary>
        /// Computes the squared distance between two two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        public static float DistanceSquared(MyVector2 a, MyVector2 b)
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
        public static MyVector2 Lerp(MyVector2 start, MyVector2 end, MyScalar interpolationAmount)
        {
            MyScalar startAmount = 1 - interpolationAmount;

            return new MyVector2
            {
                X = start.X * startAmount + end.X * interpolationAmount,
                Y = start.Y * startAmount + end.Y * interpolationAmount
            };
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