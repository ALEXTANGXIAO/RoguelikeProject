using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caff : MonoBehaviour
{
    public float ScreenWidth;
    public float ScreenHeight;

    private void Start()
    {
        ScreenWidth = Camera.main.pixelWidth;
        ScreenHeight = Camera.main.pixelHeight;
    }

    private void Update()
    {
        Ray ray_;
        ray_ = Camera.main.ScreenPointToRay(new Vector3(ScreenWidth/2, ScreenHeight/2, 0));

        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //ray_ = ray;
            //Debug.DrawRay(ray.origin, ray.direction, Color.red);
            //if (Physics.Raycast(ray, out hit))
            //{
            //    Debug.Log("hit:" + hit.collider.gameObject.name);
            //}
            //else
            //{
            //    Debug.Log("hit:null" + hit);
            //}


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            hit.collider.transform.name = "33";

            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
            foreach (var i in hits)
            {
                //Debug.Log(hits);
                Debug.Log(i.transform.name);
            }
        }
        Debug.DrawRay(ray_.origin, ray_.direction, Color.green);
    }
}
