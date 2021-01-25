using UnityEditor;
using UnityEngine;

namespace TGame
{
    class Singleton<T> where T : new()
    {
        private static T sInstance;
        public static T Instance
        {
            get
            {
                if (null == sInstance)
                {
                    sInstance = new T();
                }
                return sInstance;
            }
        }
    }
}