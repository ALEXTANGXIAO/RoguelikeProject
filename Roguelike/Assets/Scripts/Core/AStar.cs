using UnityEngine;
using System.Collections.Generic;

namespace TGame
{
    public class Map
    {
        public static Dictionary<string, MapInfo> MapTemp = new Dictionary<string, MapInfo>();
        /// <summary>
        /// 初始化地图
        /// </summary>
        public static Dictionary<string, MapInfo> GetMap()
        {
            if (MapTemp.Count > 0)
            {
                return MapTemp;
            }

            var rows = RogueLikeManager.Instance.rows;

            var columns = RogueLikeManager.Instance.columns;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int tag = 0;
                    MapInfo mi = new MapInfo(i, j, tag);
                    MapTemp.Add(i + "-" + j, mi);
                    //Debug.Log(i + "-" + j);
                }
            }
            return MapTemp;
        }

        public static void SetMap(int x,int y,int tag)
        {
            string index = x.ToString() + "-" + y.ToString();

            foreach (string key in MapTemp.Keys)
            {
                if (MapTemp[key].Equals(index))
                {
                    MapTemp[key].tag = tag;
                }
            }
        }
    }

    /// <summary>
    /// 地图节点
    /// </summary>
    public class MapInfo
    {
        public int x;
        public int y;
        public int tag;
        public int gValue;
        public int hValue;
        public MapInfo parent;
        public MapInfo()
        { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tag"></param>
        public MapInfo(int x, int y, int tag)
        {
            this.x = x;
            this.y = y;
            this.tag = tag;
            this.gValue = 0;
            this.hValue = 0;
            this.parent = null;
        }
    }

    public class AStar : MonoSingleton<AStar>
    {
        /// <summary>
        /// 地图
        /// </summary>
        Dictionary<string, MapInfo> map;
        /// <summary>
        /// open列表
        /// </summary>
        Dictionary<string, MapInfo> openList = new Dictionary<string, MapInfo>();
        /// <summary>
        /// close列表
        /// </summary>
        Dictionary<string, MapInfo> closeList = new Dictionary<string, MapInfo>();

        /// <summary>
        /// 当前点
        /// </summary>
        MapInfo currentV;
        /// <summary>
        /// 当前点的相邻节点列表
        /// </summary>
        Dictionary<string, MapInfo> adjancentMap;

        void Start()
        {
            map = Map.MapTemp;
        }

        /// <summary>
        /// 寻路
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        public void FindPath(MapInfo start, MapInfo end)
        {
            if (end.tag == 1) return;

            openList.Clear();
            closeList.Clear();
            map = Map.MapTemp;

            openList.Add(start.x + "-" + start.y, start);

            do
            {
                currentV = GetTheLowestFrom(openList);

                closeList.Add(currentV.x + "-" + currentV.y, currentV);
                openList.Remove(currentV.x + "-" + currentV.y);

                if (closeList.ContainsKey(end.x + "-" + end.y))
                {
                    Debug.Log("FindPath");
                    PrintThePath(end);
                    break;
                }

                adjancentMap = AdjacentList(currentV);

                foreach (string k in adjancentMap.Keys)
                {
                    if (closeList.ContainsKey(k))
                    {
                        continue;
                    }

                    if (!openList.ContainsKey(k))
                    {
                        adjancentMap[k].parent = currentV;
                        adjancentMap[k].gValue = currentV.gValue + 1;
                        adjancentMap[k].hValue = GetManhattanDistance(adjancentMap[k], end);
                        openList.Add(k, adjancentMap[k]);
                    }
                    else
                    {
                        int g = currentV.gValue + 1;
                        if (g < adjancentMap[k].gValue)
                        {
                            adjancentMap[k].gValue = g;
                            adjancentMap[k].parent = currentV;
                        }
                    }
                }


            } while (openList.Count > 0);
        }

        /// <summary>
        /// 获取openlist中F最小的节点
        /// </summary>
        /// <param name="open"></param>
        /// <returns></returns>
        public MapInfo GetTheLowestFrom(Dictionary<string, MapInfo> open)
        {
            MapInfo result = null;
            int min = 10000;
            foreach (MapInfo m in open.Values)
            {
                if (m.gValue + m.hValue < min)
                {
                    result = m;
                    min = m.gValue + m.hValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取当前节点的相邻节点
        /// </summary>
        /// <param name="m">当前节点</param>
        /// <returns></returns>
        public Dictionary<string, MapInfo> AdjacentList(MapInfo m)
        {
            Dictionary<string, MapInfo> resultDic = new Dictionary<string, MapInfo>();

            string left = (m.x - 1) + "-" + m.y;
            string right = (m.x + 1) + "-" + m.y;
            string top = m.x + "-" + (m.y - 1);
            string bot = m.x + "-" + (m.y + 1);

            if (map.ContainsKey(left))
            {
                if (map[left].tag == 0)
                    resultDic.Add(left, map[left]);
            }

            if (map.ContainsKey(right))
            {
                if (map[right].tag == 0)
                    resultDic.Add(right, map[right]);
            }

            if (map.ContainsKey(top))
            {
                if (map[top].tag == 0)
                    resultDic.Add(top, map[top]);
            }

            if (map.ContainsKey(bot))
            {
                if (map[bot].tag == 0)
                    resultDic.Add(bot, map[bot]);
            }
            return resultDic;
        }

        /// <summary>
        /// 获得两个点的曼哈顿距离
        /// 作为估值函数
        /// </summary>
        /// <param name="st"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int GetManhattanDistance(MapInfo st, MapInfo end)
        {
            int result = 0;
            result = Mathf.Abs(st.x - end.x) + Mathf.Abs(st.y - end.y);
            return result;
        }

        /// <summary>
        /// 输出路径
        /// </summary>
        /// <param name="end">终点</param>
        public void PrintThePath(MapInfo end)
        {
            string s = "";
            MapInfo m = end;
            while (m.parent != null)
            {
                s += "(" + m.parent.x + "," + m.parent.y + ")->";
                m = m.parent;
            }
            Debug.Log(s);
        }

        public void MoveThePath(MapInfo end)
        {
            MapInfo m = end;
            Dictionary<string, MapInfo> resultDic = new Dictionary<string, MapInfo>();
            while (m.parent != null)
            {
                m = m.parent;
            }
        }
    }
}

