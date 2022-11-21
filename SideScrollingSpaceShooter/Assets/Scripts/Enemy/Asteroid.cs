using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float degreesPerSecond;
    [SerializeField] private float damage;

    Rigidbody2D rb;
                                                  
    private void OnEnable()                                       
    {
        rb = GetComponent<Rigidbody2D>();

        var randXY = Random.Range(0.25f, 1.0f);
        var randRotSpeed = Random.Range(rotationSpeed / 2, rotationSpeed * 2);
        var randSpeed = Random.Range(movementSpeed / 2, movementSpeed * 2);

        transform.localScale = new Vector3(randXY, randXY, transform.localScale.z);
        rotationSpeed = randRotSpeed;
        movementSpeed = randSpeed;
        
    }

    private void FixedUpdate()
    {
        AsteroidMovement();
    }

    private void AsteroidMovement()
    {
        transform.Rotate(0, 0, degreesPerSecond * rotationSpeed);

        //rb.velocity = Vector2.left * movementSpeed;

        Vector3 temp = transform.position;
        temp.x += -movementSpeed * Time.deltaTime;
        transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "EnemyBullet" || collision.tag == "Player")
        {
            ExplosionPool("AsteroidExplosion");

            DeactivateObject();
        }

        if (collision.tag == "LeftBorder")
            DeactivateObject();

        if (collision.tag == "Player")
            PlayerController.instance.TakeDamage(damage);
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    private void ExplosionPool(string tag)
    {
        GameObject obj = ObjectPooling.instance.GetPooledObjects(tag);

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }
}
