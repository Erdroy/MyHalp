// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using UnityEngine;

namespace MyHalp
{
    public static partial class MyUnityExtensions
    {
        /// <summary>
        /// Returns the altitude of a character controller. 
        /// </summary>
        /// <param name="controller">The controller</param>
        /// <returns>The altitude</returns>
        public static float GetAltitude(this CharacterController controller)
        {
            RaycastHit altitudeHit;
            var ray = new Ray(controller.transform.position + Vector3.up, Physics.gravity);
            return Physics.SphereCast(ray, controller.radius, out altitudeHit) ? altitudeHit.point.y : float.NegativeInfinity;
        }

        /// <summary>
        ///     Set height of character controller
        /// </summary>
        /// <param name="c">character controller</param>
        /// <param name="height">The height to be set</param>
        public static void SetHeight(this CharacterController c, float height)
        {
            c.height = height;
            c.center = new Vector3(0, height * 0.5f, 0);
        }
        
        /// <summary>
        /// Find game object with given name in childrens.
        /// </summary>
        /// <param name="gameObject">The gamemode</param>
        /// <param name="name">The name</param>
        /// <returns>The found gameobject - if found, otherwise it will return null.</returns>
        public static GameObject FindChildGameObject(this GameObject gameObject, string name)
        {
            var ts = gameObject.transform.GetComponentsInChildren<Transform>(true);
            foreach (var t in ts)
            {
                if (t.gameObject.name == name)
                    return t.gameObject;
            }
            return null;
        }
    }
}