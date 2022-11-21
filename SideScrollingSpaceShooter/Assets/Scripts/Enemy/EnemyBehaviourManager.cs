using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourManager : MonoBehaviour
{
    public GameObject player;
    public GameObject projectileSpawn;

    public AIBehaviour behaviour;

    [SerializeField] private float health = 100f;

    private void OnEnable()
    {
        health = 100f;
    }

    void Update()
    {
        if (health > 0f)
            behaviour.ExecuteBehaviour(this);
        else
            DeactivateObject();
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.tag == "LeftBorder")
            DeactivateObject();

        if (collision.tag == "PlayerBullet")
            TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
    }
}
