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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.down);
            if (hit.collider)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
        Debug.DrawRay(ray_.origin, ray_.direction, Color.green);
    }
}
