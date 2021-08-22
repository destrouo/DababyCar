using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveEnemy : MonoBehaviour
{

    private float speed = 0.04f;
    Vector2 dir = new Vector2(-2f, 0f);
   // public Vector2 dir;
    private int Score;

    Jump JumpScript = new Jump();

    //Vector2 diry = new Vector2(0f, 0.005f);

    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Score = DataHolder._Score;
        if (Score == 10)
            speed = 0.045f;
        else if (Score == 20)
            speed = 0.05f;
        else if (Score == 30)
            speed = 0.06f;
        else if (Score == 40)
            speed = 0.07f;
        else if (Score == 50)
            speed = 0.10f;
        else if (Score == 60)
            speed = 0.12f;
        else if (Score == 100)
            speed = 0.14f;


        transform.Translate(dir * speed, Space.World);

        if (gameObject.transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
