using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageManager : MonoBehaviour
{

    public StageIniter stageIniter;

    public GameObject Boss;
    private Vector3 BossPos;

    // Start is called before the first frame update
    void Start()
    {
        BossPos = Boss.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
