using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomeranParScript : MonoBehaviour
{
    private Vector3 Velocity;
    // Start is called before the first frame update
    void Start()
    {
        Velocity = new(Random.Range(-1.0f, 1.0f), -0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity * Time.deltaTime*5.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
