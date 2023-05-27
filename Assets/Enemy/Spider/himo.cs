using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class himo : MonoBehaviour
{
    public SpiderScript spider;
    public GameObject Spiderprefab;
    public float HimoPos;
    public float HimoSpeed;

  

    // Start is called before the first frame update
    void Start()
    {
        
        spider = Spiderprefab.GetComponent<SpiderScript>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (spider.isHook)
        {
                Debug.Log("fack");
            if (!spider.isReturn)
            {
                HimoPos += Time.deltaTime * 10;
                transform.localScale = new Vector3(1, HimoPos, 1);

               // transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            }
            if (spider.isReturn)
            {


                transform.localScale = new Vector3(1, HimoPos, 1);
                HimoPos -= Time.deltaTime * 10;
               // transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);



                
            }
        }
       

    }
  
    private void shinsyuku()
    {
        //if (transform.localScale.y > 10.0f)
        //{
        //    spider.isReturn = true;
        //}
        //else if (transform.localScale.y < 0.0f)
        //{
        //    spider.isReturn = false;
        //}

       
    }
}
