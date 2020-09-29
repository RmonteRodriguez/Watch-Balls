using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleAI : MonoBehaviour
{
    public float moveSpeed;
    public float hunger;
    public float walkTime;
    public float waitTime;
    private float walkCounter;
    private float waitCounter;

    public Rigidbody2D rb;

    public bool isWalking;

    private int walkDirection;
    private int reproducedAmount;

    public GameObject duplicate;

    public Transform currentLocation;
    public new Vector3 spawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;
        hunger = 50;
        reproducedAmount = 0;

        ChooseDirection();
    }

    void Update()
    {
        spawnPoint = transform.position;

        if (hunger >= 150)
        {
            if (reproducedAmount <= 1)
            {
                Reproduce();
            }
        }

        if (hunger <= 0)
        {
            Destroy(gameObject);
        }

        if (isWalking)
        {
            walkCounter -= Time.deltaTime;
            hunger -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    rb.velocity = new Vector2(0, moveSpeed);
                    break;
                case 1:
                    rb.velocity = new Vector2(moveSpeed, 0);
                    break;
                case 2:
                    rb.velocity = new Vector2(0, -moveSpeed);
                    break;
                case 3:
                    rb.velocity = new Vector2(-moveSpeed, 0);
                    break;
                case 4:
                    rb.velocity = new Vector2(-moveSpeed, moveSpeed);
                    break;
                case 5:
                    rb.velocity = new Vector2(moveSpeed, -moveSpeed);
                    break;
                case 6:
                    rb.velocity = new Vector2(moveSpeed, moveSpeed);
                    break;
                case 7:
                    rb.velocity = new Vector2(-moveSpeed, -moveSpeed);
                    break;
            }

            if (walkCounter < 0)
            {
                waitTime = Random.Range(0, 25);
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            hunger += Time.deltaTime;

            rb.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        walkTime = Random.Range(0, 10);
        walkDirection = Random.Range (0, 9);
        isWalking = true;
        walkCounter = walkTime;
    }

    public void Reproduce()
    {
        Instantiate(duplicate, spawnPoint, Quaternion.identity);

        reproducedAmount++;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Red")
        {
            hunger = hunger + 5;
        }

        if (collision.gameObject.tag == "Pink")
        {
            hunger = hunger + 10;
        }
    }
}
