using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HimoCollisionScript : MonoBehaviour
{

    public bool cut = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boomerang")
        {
            cut = true; 

        }
        
    }
}
