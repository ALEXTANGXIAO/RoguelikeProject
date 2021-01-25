using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TGame
{
    public class RogueLikeManager : MonoSingleton<RogueLikeManager>
    {
        [Serializable]
        public class Count
        {
            public int minimum;
            public int maximum;

            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }

        public int rows = 0;        
        public int columns = 0;      
        public GameObject[] floorTiles;
        public GameObject[] outerWallTiles;
        public GameObject exitTile;

        public Count wallCount = new Count(50, 90);
        public Count foodCount = new Count(1, 5);
        public GameObject[] wallArray;
        public GameObject[] foodArray;
        public GameObject[] enemyArray;
        public GameObject[] bossArray;

        Transform mapHandler;
        Transform objHandler;
        List<Vector3> gridPositions = new List<Vector3>();

        public void SetScene()
        {
            SetMapRandom();
            SetExitTile();
            InitialiseList();
            xxx();
        }

        private void SetMapRandom()
        {
            mapHandler = new GameObject("Map").transform;
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y ++)
                {
                    GameObject toInstance = floorTiles[Random.Range(0,floorTiles.Length)];
                    if (x == 0 || x == columns -1 || y == 0 || y == rows -1)
                        toInstance = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    //GameObject instance = Instantiate(toInstance,new Vector3(x,y,0f),Quaternion.identity) as GameObject;
                    GameObject instance = Instantiate(toInstance, (new Vector3(x, y, 0f)), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(mapHandler);
                    instance.name = toInstance.name;
                }
            }
        }

        private void InitialiseList()
        {
            gridPositions.Clear();
            for(int x = 0; x < columns;x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }

        //从girdPosition数组内获得一个随机坐标
        private Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);
            return (randomPosition);

        }

        //根据传入的数组中随机设置Object
        private void RandomSetObject(GameObject[] objectArray, int minimum,int maxminum,string tag = "Root")
        {
            if (objHandler == null)
            {
                objHandler = new GameObject(tag).transform;
            }

            if (objectArray.Length == 0)
            {
                return;
            }
            int objectCount = Random.Range(minimum, maxminum + 1);
            for (int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPosition();
                GameObject obj = objectArray[Random.Range(0, objectArray.Length)];
                GameObject instance = Instantiate(obj, randomPosition, Quaternion.identity);
                instance.transform.SetParent(objHandler);
                instance.name = obj.name;
            }
        }

        private void xxx()
        {
            RandomSetObject(wallArray,wallCount.minimum,wallCount.maximum);
            RandomSetObject(foodArray, foodCount.minimum, foodCount.maximum);
            int enemyCount = 20;
            RandomSetObject(enemyArray, enemyCount, enemyCount);
        }

        private void SetExitTile()
        {
            if (exitTile != null)
            {
                Instantiate(exitTile, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
            }
        }
    }
}


