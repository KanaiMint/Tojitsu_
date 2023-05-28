using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public bool invincible;

    private int HP;
    public int MaxHP;

    private float kDamagedLookTimeMax = 0.1f;
    private float DamagedLookTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {

        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }

        if (DamagedLookTime > 0.0f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            DamagedLookTime -= Time.deltaTime;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Boomerang")
        {
            Destroy(collision.gameObject);
            if (invincible == false)
            {
                HP--;
                DamagedLookTime = kDamagedLookTimeMax;
            }
        }

    }
}
