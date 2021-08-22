using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPlayer : MonoBehaviour
{

    public Sprite[] Wheels;

    public SpriteRenderer spriteWheel1;
    public SpriteRenderer spriteWheel2;

    // Start is called before the first frame update
    void Start()
    {
        //spriteWheel1 = GetComponent<SpriteRenderer>();
        //spriteWheel2 = GetComponent<SpriteRenderer>();
        int WheelID = PlayerPrefs.GetInt("EquipWheels");
        spriteWheel1.sprite = Wheels[WheelID];
        spriteWheel2.sprite = Wheels[WheelID];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
