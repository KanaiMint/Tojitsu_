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
        lineRenderer.positionCount = 2; // ���̒��_����2�ɐݒ�
        lineRenderer.SetPosition(0, new Vector3(transform.position.x,transform.position.y+1000)); // ���̎n�_�����WA�ɐݒ�
        lineRenderer.SetPosition(1, transform.position); // ���̏I�_�����WB�ɐݒ�
    }
}
