// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using UnityEngine;

namespace MyHalp
{
    public static partial class MyUnityExtensions
    {
        // LocalRotation
        public static void SetLocalRotation(this Transform transform, Quaternion rotation)
        {
            transform.localRotation = rotation;
        }

        // LocalRotation
        public static void SetLocalRotation(this Transform transform, Vector3 eulerAngles)
        {
            transform.localEulerAngles = eulerAngles;
        }

        // Rotation
        public static void SetRotation(this Transform transform, Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        // Rotation
        public static void SetRotation(this Transform transform, Vector3 eulerAngles)
        {
            transform.eulerAngles = eulerAngles;
        }

        // LocalRotation
        public static void SetLocalRotation(this GameObject gameObject, Quaternion rotation)
        {
            gameObject.transform.localRotation = rotation;
        }

        // LocalRotation
        public static void SetLocalRotation(this GameObject gameObject, Vector3 eulerAngles)
        {
            gameObject.transform.localEulerAngles = eulerAngles;
        }

        // Rotation
        public static void SetRotation(this GameObject gameObject, Quaternion rotation)
        {
            gameObject.transform.rotation = rotation;
        }

        // Rotation
        public static void SetRotation(this GameObject gameObject, Vector3 eulerAngles)
        {
            gameObject.transform.eulerAngles = eulerAngles;
        }

        // Position
        public static void SetPosition(this GameObject gameObject, Vector3 position)
        {
            gameObject.transform.position = position;
        }

        // LocalPosition
        public static void SetLocalPosition(this GameObject gameObject, Vector3 localPosition)
        {
            gameObject.transform.localPosition = localPosition;
        }

        // Position
        public static void SetPosition(this Transform trasform, Vector3 position)
        {
            trasform.position = position;
        }

        // LocalPosition
        public static void SetLocalPosition(this Transform trasform, Vector3 localPosition)
        {
            trasform.localPosition = localPosition;
        }

        // Scale
        public static void SetScale(this GameObject gameObject, Vector3 scale)
        {
            gameObject.transform.localScale = scale;
        }
        
        // Scale
        public static void SetScale(this Transform trasform, Vector3 scale)
        {
            trasform.localScale = scale;
        }

        // Vector/MyQuaternion extensions
        public static Vector2 Mul(this Vector2 vecA, Vector2 vecB)
        {
            return new Vector2(vecA.x * vecB.x, vecA.y * vecB.y);
        }

        public static Vector2 Add(this Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.x + vec2.x, vec1.y + vec2.y);
        }

        public static Vector3 Mul(this Vector3 vecA, Vector3 vecB)
        {
            return new Vector3(vecA.x * vecB.x, vecA.y * vecB.y, vecA.z * vecB.z);
        }

        public static Vector3 Div(this Vector3 vecA, Vector3 vecB)
        {
            return new Vector3(vecA.x / vecB.x, vecA.y / vecB.y, vecA.z / vecB.z);
        }

        public static Vector3 Add(this Vector3 vecA, Vector3 vecB)
        {
            return new Vector3(vecA.x + vecB.x, vecA.y + vecB.y, vecA.z + vecB.z);
        }

        public static Vector3 Sub(this Vector3 vecA, Vector3 vecB)
        {
            return new Vector3(vecA.x - vecB.x, vecA.y - vecB.y, vecA.z - vecB.z);
        }

        public static Quaternion Mul(this Quaternion vecA, Quaternion vecB)
        {
            return new Quaternion(vecA.x * vecB.x, vecA.y * vecB.y, vecA.z * vecB.z, vecA.w);
        }

        public static Quaternion Mul(this Quaternion vecA, float x)
        {
            return new Quaternion(vecA.x * x, vecA.y * x, x, vecA.w);
        }

        public static Quaternion Div(this Quaternion vecA, Quaternion vecB)
        {
            return new Quaternion(vecA.x / vecB.x, vecA.y / vecB.y, vecA.z / vecB.z, vecA.w);
        }

        public static Quaternion Add(this Quaternion vecA, Quaternion vecB)
        {
            return new Quaternion(vecA.x + vecB.x, vecA.y + vecB.y, vecA.z + vecB.z, vecA.w);
        }

        public static Quaternion Sub(this Quaternion vecA, Quaternion vecB)
        {
            return new Quaternion(vecA.x - vecB.x, vecA.y - vecB.y, vecA.z - vecB.z, vecA.w);
        }

        /// <summary>
        ///     Returns horizontal vector (y = 0)
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <returns>Horizontal vector</returns>
        public static Vector3 Horizontal(this Vector3 vector)
        {
            vector.y = 0;
            return vector;
        }

        /// <summary>
        ///     Returns vertical vector (x,z = 0)
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <returns>Horizontal vector</returns>
        public static Vector3 Vertical(this Vector3 vector)
        {
            vector.x = vector.z = 0;
            return vector;
        }
    }
}