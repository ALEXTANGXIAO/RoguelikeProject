using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace TGame
{
    public class GameController : MonoSingleton<GameController>
    {
        RogueLikeManager rogueLikeManager;

        [HideInInspector] public bool playerTurn = true;

        private void Start()
        {
            rogueLikeManager = GetComponent<RogueLikeManager>();
            InitGame();
            Map.GetMap();
        }

        void InitGame()
        {
            rogueLikeManager.SetScene();
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.down);
                if (hit.collider)
                {
                    Debug.Log(hit.collider.gameObject.name);

                    var endpos = hit.collider.gameObject.transform.position;

                    var startpos = GameObject.Find("Player").transform.position;
                    FindPath(startpos, endpos);
                    Debug.Log(AStar.Instance.pathDic);
                    Player.Instance.MoveByPath(AStar.Instance.pathDic);
                }
            }
        }

        public void FindPath(Vector3 startpos,Vector3 endpos)
        {
            Debug.Log(startpos + "=>" + endpos);
            MapInfo start = Map.MapTemp[startpos.x + "-" + startpos.y];
            MapInfo end   = Map.MapTemp[endpos.x + "-" + endpos.y];
            AStar.Instance.FindPath(start, end);
        }
    }
}

