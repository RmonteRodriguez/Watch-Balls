using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAI : MonoBehaviour
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

    public GameObject duplicate;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;
        hunger = 50;

        ChooseDirection();
    }

    void Update()
    {
        if (hunger > 55)
        {
            Reproduce();
        }
        else if (hunger <= 0)
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
                waitTime = Random.Range(0, 5);
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
        walkTime = Random.Range(0, 25);
        walkDirection = Random.Range(0, 9);
        isWalking = true;
        walkCounter = walkTime;
    }

    public void Reproduce()
    {
        Vector3 position = new Vector3(0, 0, 0);
        Instantiate(duplicate, position, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Purple" || collision.gameObject.tag == "Pink")
        {
            Destroy(gameObject);
        }
    }
}
