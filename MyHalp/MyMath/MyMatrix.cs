// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System;

namespace MyHalp.MyMath
{
    /// <summary>
    /// A Matrix with 4 rows and 4 columns.
    /// </summary>
    public class MyMatrix
    {
        public MyScalar M11;
        public MyScalar M12;
        public MyScalar M13;
        public MyScalar M14;
        public MyScalar M21;
        public MyScalar M22;
        public MyScalar M23;
        public MyScalar M24;
        public MyScalar M31;
        public MyScalar M32;
        public MyScalar M33;
        public MyScalar M34;
        public MyScalar M41;
        public MyScalar M42;
        public MyScalar M43;
        public MyScalar M44;

        /// <summary>
        /// Constructs a new 4 row, 4 column MyMatrix.
        /// </summary>
        public MyMatrix() { }

        /// <summary>
        /// Constructs a new 4 row, 4 column MyMatrix.
        /// </summary>
        public MyMatrix(MyScalar m11, MyScalar m12, MyScalar m13, MyScalar m14,
                      MyScalar m21, MyScalar m22, MyScalar m23, MyScalar m24,
                      MyScalar m31, MyScalar m32, MyScalar m33, MyScalar m34,
                      MyScalar m41, MyScalar m42, MyScalar m43, MyScalar m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;

            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;

            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;

            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        /// <summary>
        /// Gets or sets the translation component of the transform.
        /// </summary>
        public MyVector3 Translation
        {
            get
            {
                return new MyVector3()
                {
                    X = M41,
                    Y = M42,
                    Z = M43
                };
            }
            set
            {
                M41 = value.X;
                M42 = value.Y;
                M43 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the backward vector of the MyMatrix.
        /// </summary>
        public MyVector3 Backward
        {
            get
            {
                return new MyVector3(M31, M32, M33);
            }
            set
            {
                M31 = value.X;
                M32 = value.Y;
                M33 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the down vector of the MyMatrix.
        /// </summary>
        public MyVector3 Down
        {
            get
            {
                return new MyVector3(-M21, -M22, -M23);
            }
            set
            {
                M21 = -value.X;
                M22 = -value.Y;
                M23 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the forward vector of the MyMatrix.
        /// </summary>
        public MyVector3 Forward
        {
            get
            {
                return new MyVector3(-M31, -M32, -M33);
            }
            set
            {
                M31 = -value.X;
                M32 = -value.Y;
                M33 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the left vector of the MyMatrix.
        /// </summary>
        public MyVector3 Left
        {
            get
            {
                return new MyVector3(-M11, -M12, -M13);
            }
            set
            {
                M11 = -value.X;
                M12 = -value.Y;
                M13 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the right vector of the MyMatrix.
        /// </summary>
        public MyVector3 Right
        {
            get
            {
                return new MyVector3(M11, M12, M13);
            }
            set
            {
                M11 = value.X;
                M12 = value.Y;
                M13 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the up vector of the MyMatrix.
        /// </summary>
        public MyVector3 Up
        {
            get
            {
                return new MyVector3(M21, M22, M23);
            }
            set
            {
                M21 = value.X;
                M22 = value.Y;
                M23 = value.Z;
            }
        }

        /// <summary>
        /// Transposes the MyMatrix in-place.
        /// </summary>
        public void Transpose()
        {
            MyScalar intermediate = M12;
            M12 = M21;
            M21 = intermediate;

            intermediate = M13;
            M13 = M31;
            M31 = intermediate;

            intermediate = M14;
            M14 = M41;
            M41 = intermediate;

            intermediate = M23;
            M23 = M32;
            M32 = intermediate;

            intermediate = M24;
            M24 = M42;
            M42 = intermediate;

            intermediate = M34;
            M34 = M43;
            M43 = intermediate;
        }

        /// <summary>
        /// Creates a string representation of the MyMatrix.
        /// </summary>
        /// <returns>A string representation of the MyMatrix.</returns>
        public override string ToString()
        {
            return "{" + M11 + ", " + M12 + ", " + M13 + ", " + M14 + "} " +
                   "{" + M21 + ", " + M22 + ", " + M23 + ", " + M24 + "} " +
                   "{" + M31 + ", " + M32 + ", " + M33 + ", " + M34 + "} " +
                   "{" + M41 + ", " + M42 + ", " + M43 + ", " + M44 + "}";
        }

        // ------------ STATIC METHODS ------------
        
        // TODO: Static methods. Transform, Transpose, CreateFromQuaterion etc.

        /// <summary>
        /// Gets the 4x4 identity MyMatrix.
        /// </summary>
        public static MyMatrix Identity
        {
            get
            {
                var toReturn = new MyMatrix(
                    1, 0, 0, 0, 
                    0, 1, 0, 0, 
                    0, 0, 1, 0,
                    0, 0, 0, 1);

                return toReturn;
            }
        }
    }
}