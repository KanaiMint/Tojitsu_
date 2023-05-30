using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject TargetObject;
    public Vector2 CameraPos = Vector2.zero;
    private Vector3 TargetPos;

    public bool follow = false;

    // Start is called before the first frame update
    void Start()
    {
        TargetObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            TargetPos = TargetObject.transform.position;
        }
        else
        {
            TargetPos = Vector3.zero;
        }
            TargetPos.x += CameraPos.x;
            TargetPos.y += CameraPos.y;
            TargetPos.z = -10;
        

        this.transform.position = TargetPos;
    }
}
