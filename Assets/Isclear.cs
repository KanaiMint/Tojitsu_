using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Isclear : MonoBehaviour
{
    public GameObject BossPrefab;
    private MoveScript moveScript;
    
    // Start is called before the first frame update
    void Start()
    {
        moveScript = BossPrefab.GetComponent<MoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BossPrefab == null)
        {
             this.gameObject.SetActive(true); 
        }
        else
        {
           this .gameObject.SetActive(false);
        }
    }
}
