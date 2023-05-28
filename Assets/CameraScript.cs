using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject TargetObject;
    public Vector2 CameraPos = Vector2.zero;
    private Vector3 TargetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetPos = TargetObject.transform.position;

        TargetPos.x += CameraPos.x;
        TargetPos.y += CameraPos.y;
        TargetPos.z = -10;

        this.transform.position = TargetPos;
    }
}
