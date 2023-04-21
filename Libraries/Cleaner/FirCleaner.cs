using System.Collections.Generic;
using UnityEngine;
using Transform = UnityEngine.Transform;

namespace FirCleaner
{
    public static class GameCleaner
    {
        public static void DeleteAllGameObjects<T1, T2>(Dictionary<T1,T2> values, float t = 0)
        {
            if (values == null && values.Count > 0)
                return;

            foreach (KeyValuePair<T1, T2> value in values)
            {
                if(value.Key is Object Key)
                    MonoBehaviour.Destroy(Key, t);
                if(value.Value is Object Value)
                    MonoBehaviour.Destroy(Value, t);
            }
        }

        public static void DeleteAllGameObjects<T>(T[] values, float t = 0) where T : Object
        {
            if (values == null)
                return;

            foreach (T value in values)
            {
                MonoBehaviour.Destroy(value, t);
            }
        }
        public static void DeleteAllGameObjects<T>(List<T> values, float t = 0) where T : Object
        {
            if (values == null)
                return;

            foreach (T value in values)
            {
                MonoBehaviour.Destroy(value, t);
            }
        }

        public static void DeleteAllChild<T>(T value, float t = 0) where T : MonoBehaviour
        {
            if (value != null)
                DeleteAllChild(value.transform, t);
        }
        public static void DeleteAllChild(Transform transform, float t = 0)
        {
            if (transform == null)
                return;

            int i = transform.childCount;
            while (i > 0)
            {
                i--;
                MonoBehaviour.Destroy(transform.GetChild(i).gameObject, t);
            }
        }
    }
}
