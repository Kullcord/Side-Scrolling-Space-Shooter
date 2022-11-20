using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float degreesPerSecond;

    Rigidbody2D rb;

    private void Start()//Need to change to OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        AsteroidMovement();
    }

    private void AsteroidMovement()
    {
        transform.Rotate(0, 0, degreesPerSecond * rotationSpeed);

        //Vector3 temp = transform.position;
        //temp.x += -movementSpeed * Time.deltaTime;

        rb.velocity = Vector2.left * movementSpeed;

        //transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            gameObject.SetActive(false);
        }
    }
}
