using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerScript Player;

    public Transform Transform;
    public float Movespeed_Second;
    public Vector2 Vel;
    public bool Death;
    public GameObject Bullet;
    public int HP;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(Player.transform.position, this.transform.position) < 10f) //‹ß‚­‚É‚¢‚½‚ç
        {

        }
    }
}
