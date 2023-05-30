using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    public GameObject Door;

    private bool ON = false;
    public Sprite On;
    public Sprite Off;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (ON == false)
        {
            Door.SetActive(true);
            this.GetComponent<SpriteRenderer>().sprite = Off;
        }
        else
        {
            Door.SetActive(false);
            this.GetComponent<SpriteRenderer>().sprite = On;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Boomerang")
        {

            if (ON == false)
            {
                ON = true;
            }
            else
            {
                //ON = false;
            }

        }
    }
}
