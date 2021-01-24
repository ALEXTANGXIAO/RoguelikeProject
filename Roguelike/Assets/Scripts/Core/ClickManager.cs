using System.Collections;
using UnityEngine;

namespace TGame
{
    public class ClickManager : MonoBehaviour
    {
        public float ScreenWidth;
        public float ScreenHeight;

        private void Start()
        {
            ScreenWidth = Camera.main.pixelWidth;
            ScreenHeight = Camera.main.pixelHeight;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //将点击坐标转为世界坐标
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    hit.collider.attachedRigidbody.AddForce(Vector2.up);
                }
            }
        }
    }
}