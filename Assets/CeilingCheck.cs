using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    private string groundTag = "Ground";

    private bool isCeiling = false;
    private bool isCeilingEnter, isCeilingStay, isCeilingExit;
    public bool IsCeiling()
    {
        if (isCeilingEnter || isCeilingStay)
        {
            isCeiling = true;
        }
        else if (isCeilingExit)
        {
            isCeiling = false;
        }

        isCeilingEnter = false;
        isCeilingStay = false;
        isCeilingExit = false;
        return isCeiling;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isCeilingEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isCeilingStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isCeilingExit = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
