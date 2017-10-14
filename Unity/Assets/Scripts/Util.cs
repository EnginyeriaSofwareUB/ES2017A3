using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    public class Util
    {
        public static void AssignObjectParent(GameObject gameObject, string childTag)
        {
            var objects = GameObject.FindGameObjectsWithTag(childTag);
            foreach (var obj in objects)
                obj.transform.parent = gameObject.transform;
        }

        public static void AssignObjectParent(string parentTag, GameObject childGameObject)
        {
            var parent = GameObject.FindGameObjectWithTag(parentTag);
            childGameObject.transform.parent = parent.transform;
        }

        public static void AssignObjectParent(string parentTag, string childTag)
        {
            var parent = GameObject.FindGameObjectWithTag(parentTag);
            var objects = GameObject.FindGameObjectsWithTag(childTag);
            foreach (var obj in objects)
                obj.transform.parent = parent.transform;
        }

        public static UnityEngine.Object LoadWeapon(string name)
        {
            return AssetDatabase.LoadAssetAtPath("Assets/Resources/Weapons/" + name + ".prefab", typeof(GameObject));
        }

        public static void StopScene()
        {
            Time.timeScale = 0;
        }

        public static void StartScene()
        {
            Time.timeScale = 1;
        }
    }
}
