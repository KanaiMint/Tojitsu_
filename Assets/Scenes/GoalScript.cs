using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    public int nextStage = 0;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject == player)
        {

            string nextSceneName = "stage0";
            Vector2 nextStartPos = Vector2.zero;

            switch (nextStage)
            {
                case 0:
                    nextSceneName = "SampleScene";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 1:
                    nextSceneName = "stage1";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 2:
                    nextSceneName = "stage2";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -4.2f;
                    break;
                case 3:
                    nextSceneName = "stage3";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 4:
                    nextSceneName = "stage4";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 5:
                    nextSceneName = "stage5";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 6:
                    nextSceneName = "stage6";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 7:
                    nextSceneName = "stage7";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 8:
                    nextSceneName = "stage8";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 9:
                    nextSceneName = "stage9";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 10:
                    nextSceneName = "stage10";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 11:
                    nextSceneName = "stage11";
                    nextStartPos.x = -12f;
                    nextStartPos.y = -5.2f;
                    break;
                case 99:
                    nextSceneName = "Boss";
                    nextStartPos.x = -9f;
                    nextStartPos.y = -3.2f;
                    break;
            }


            DontDestroyOnLoad(player);
            //Destroy(player);
            SceneManager.LoadScene(nextSceneName);


            player.GetComponent<PlayerScript>().Init();
            player.transform.position = nextStartPos;

        }

    }
}
