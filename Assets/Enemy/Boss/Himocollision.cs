using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Himocollision : MonoBehaviour
{
    public bool Hit;
    public bool Cut;
    public int HP=10;
    public GameObject Boss;
    public float HimoHukkatsuTime;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0&&Cut==false)
        {
            Cut = true;
            HimoHukkatsuTime = 0;
        }
        if(Cut==true)
        {
            HimoHukkatsuTime += Time.deltaTime;
            if (HimoHukkatsuTime > 5)
            {
                HP = 10;
                Cut = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boomerang")
        {
            HP -= 1;
            Destroy(collision.gameObject);
        }
    }
}
