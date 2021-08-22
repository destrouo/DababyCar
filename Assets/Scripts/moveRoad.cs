using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRoad : MonoBehaviour
{
    private float speed = 4f;
    private Transform back_Tranform;
    private float back_Size;
    private float back_pos;

    private int score;

    public float y = 2.34f;
    void Start()
    {
        back_Tranform = GetComponent<Transform>();
        back_Size = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score = DataHolder._Score;
        if (score == 10)
            speed = 6f;
        else if (score == 20)
            speed = 8f;
        else if (score == 30)
            speed = 9f;
        else if (score == 40)
            speed = 10f;
        else if (score == 50)
            speed = 11f;
        else if (score == 60)
            speed = 13f;
        else if (score == 100)
            speed = 15f;

        Move();
    }

    public void Move()
    {
        back_pos -= speed * Time.deltaTime;
        back_pos = Mathf.Repeat(back_pos, back_Size);
        back_Tranform.position = new Vector2(back_pos, y);
    }
}