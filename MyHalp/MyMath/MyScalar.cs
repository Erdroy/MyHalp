// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.
// Select which scalar you want to use.
#define SCALAR_FLOAT
//#define SCALAR_DOUBLE

using System.Globalization;

namespace MyHalp.MyMath
{
    /// <summary>
    /// MyScalar
    /// This class helps to change the base-scalars in whole 'MyHalp.MyMath' namespace.
    /// </summary>
    public struct MyScalar
    {
#if SCALAR_FLOAT
        public float Value;

        public static implicit operator MyScalar(float value)
        {
            return new MyScalar {Value = value};
        }

        public static implicit operator float(MyScalar scalar)
        {
            return scalar.Value;
        }
#else
        public double Value;

        public static implicit operator MyScalar(double value)
        {
            return new MyScalar {Value = value};
        }

        public static implicit operator double(MyScalar scalar)
        {
            return scalar.Value;
        }
#endif

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}