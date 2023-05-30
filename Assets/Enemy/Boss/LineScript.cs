using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject HimoCollision;
    public bool cut = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
       



    }

    private void DrawLine()
    {
        lineRenderer.positionCount = 2; // 線の頂点数を2に設定
        lineRenderer.SetPosition(0, new Vector3(transform.position.x,transform.position.y+1000,0.1f)); // 線の始点を座標Aに設定
        lineRenderer.SetPosition(1,new Vector3 (transform.position.x,transform.position.y,0.1f)); // 線の終点を座標Bに設定
    }
}
