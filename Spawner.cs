using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject balls;

    private int amount;
    private int finalAmount;

    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
        finalAmount = Random.Range(1, 10000);
    }

    // Update is called once per frame
    void Update()
    {
        if (amount <= finalAmount)
        {
            SpawnBalls();
        }
    }

    void SpawnBalls()
    {
        Vector3 position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-50.0F, 50.0F), 0);
        Instantiate (balls, position, Quaternion.identity);

        amount++;
    }
}
