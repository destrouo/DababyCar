using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform SpawnPos;
    public List<GameObject> Enemys;
    public float time;

    private int Count;
    private System.Random rand = new System.Random();


    // private void Awake()
    // {
    //     Application.targetFrameRate = 60;
    // }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        Count = Enemys.Count;
    }

    // Update is called once per frame
    void Repeat()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        // if Score >30 time =2
        // Score >60 time = 1.5;
        // score > 100 time = 1;
        yield return new WaitForSeconds(time);
        int RandCount = rand.Next(0, Count);
        Instantiate(Enemys[RandCount], SpawnPos.position, Quaternion.identity);
        Repeat();
    }

}
