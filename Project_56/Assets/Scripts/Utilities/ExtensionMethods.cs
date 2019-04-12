using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading;

namespace Project56
{
    public static class ExtensionMethods
    {
        public static void DestroyChildren(this Transform t)
        {
            bool isPlaying = Application.isPlaying;

            while (t.childCount != 0)
            {
                Transform child = t.GetChild(0);

                if (isPlaying)
                {
                    child.SetParent(null);
                    UnityEngine.Object.Destroy(child.gameObject);
                }
                else UnityEngine.Object.DestroyImmediate(child.gameObject);
            }
        }

        public static void SetActive(this Transform obj, bool value)
        {
            obj.gameObject.SetActive(value);
        }

        public static void SetActive(this MonoBehaviour obj, bool value)
        {
            obj.gameObject.SetActive(value);
        }

        public static void SetParent(this Transform transform, Transform parent, Vector3 localPosition, Vector3 localScale, bool worldPositionStays)
        {
            transform.SetParent(parent, worldPositionStays);
            transform.localPosition = localPosition;
            transform.localScale = localScale;
        }

        //
        public static bool IsNullOrEmpty<T>(this T[] array) where T : class
        {
            if (array == null || array.Length == 0)
                return true;
            else
                return array.All(item => item == null);
        }

        public static bool Contains<T>(this T[] array, T element)
        {
            for (int i = array.Length - 1; i != -1; --i)
            {
                if (element.Equals(array[i]))
                    return true;
            }
            return false;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static System.Random Local;

        public static System.Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}