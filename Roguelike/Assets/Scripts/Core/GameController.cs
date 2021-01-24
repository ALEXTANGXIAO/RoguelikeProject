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
            //Map.GetMap();
        }

        void InitGame()
        {
            rogueLikeManager.SetScene();
        }

        private void Update()
        {
            Debug.DrawRay(transform.position,  new Vector3(0, 0, 1), Color.red);

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction, Color.green);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    Debug.Log("hit:" + hit.collider.gameObject.name);
                }
                else
                {
                    //Debug.Log("hit:null" + hit);
                }



                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                //Debug.DrawRay(ray.origin, ray.direction, Color.green);
                //Debug.Log(hit.collider.transform.name);
                //hit.collider.transform.name = "33";

                //    //RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
                //    //foreach (var i in hits)
                //    //{
                //    //    //Debug.Log(hits);
                //    //    Debug.Log(i.transform.name);
                //    //}


                //    //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //将点击坐标转为世界坐标
                //    //Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                //    //Debug.DrawRay(mousePos, mousePos, Color.green);
                //    //RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                //    //if (hit.collider != null)
                //    //{
                //    //    hit.collider.transform.name = "33";
                //    //    Debug.Log(hit.collider.gameObject.name);
                //    //    //hit.collider.attachedRigidbody.AddForce(Vector2.up);
                //    //}
                //}
            }
        }

        public void FindPath()
        {
            MapInfo start = Map.MapTemp["5-2"];
            MapInfo end   = Map.MapTemp["6-8"];
            AStar.Instance.FindPath(start, end);
        }
    }
}

