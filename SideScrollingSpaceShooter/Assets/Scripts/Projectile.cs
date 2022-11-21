using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float deactivationTimer = 2f;
    [SerializeField] private bool isEnemy;
    public float damage = 20f;

    public GameObject explosion;
    public LayerMask enemyLayer;
    public float explosionRange;

    private void OnEnable()
    {
        Invoke(nameof(DeactivateProjectile), deactivationTimer);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;

        transform.position = temp;
    }

    private void DeactivateProjectile()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid" || collision.tag == "LeftBorder" || collision.tag == "RightBorder"
            || (!isEnemy && collision.tag == "Enemy1" || collision.tag == "Enemy2") || (isEnemy && collision.tag == "Player"))
        {
            ObjectPool();

            DeactivateProjectile();
        }

        if ((isEnemy && collision.tag == "Player"))
            PlayerController.instance.TakeDamage(damage);

        if (!isEnemy && collision.tag == "Enemy1" || collision.tag == "Enemy2")
            Explode();
    }

    bool alreadyDone = false;
    bool dealtDamage = false;

    private void Explode()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, enemyLayer);
        for(int i = 0; i < enemies.Length; i++)
        {
            if (!dealtDamage)
            {
                enemies[i].GetComponent<EnemyBehaviourManager>().TakeDamage(damage);
                dealtDamage = true; 
            }

            Invoke("Delay", 0.05f);
        }
    }

    private void Delay()
    {
        dealtDamage = false;
    }

    private void ObjectPool()
    {
        //add explosion from object pool
        GameObject obj = ObjectPooling.instance.GetPooledObjects("BulletExplosion");

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

        alreadyDone = true;
    }
}
