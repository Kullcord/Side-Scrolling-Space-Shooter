using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourManager : MonoBehaviour
{
    public GameObject player;
    public GameObject projectileSpawn;

    public AIBehaviour behaviour;

    public float health = 100f;
    public float damage = 100f;

    private void OnEnable()
    {
        health = 100f;
        player = PlayerController.instance.gameObject;
    }

    void Update()
    {
        if (health > 0f)
            behaviour.ExecuteBehaviour(this);
        else
            DeactivateObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LeftBorder")
            DeactivateObject();

        if (collision.tag == "PlayerBullet")
            TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);

        if(collision.tag == "Player")
        {
            DeactivateObject();
            PlayerController.instance.TakeDamage(damage);
        }
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
