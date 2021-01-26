using System.Collections;
using UnityEngine;

namespace TGame
{
    public class TResources : MonoBehaviour
    {
        public static GameObject AllocGameObject(string name,string parentname = null)
        {
            GameObject obj = new GameObject();
            obj.name = name;

            if (parentname != null)
            {

            }

            return obj;
        }
    }
}