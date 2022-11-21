using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType
    {
        BasicShooter,
        Bomber
    }

    public EnemyType state;

    Rigidbody2D rb;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject projectileSpawn;

    [SerializeField] private float speed;

    [SerializeField] private float attackTime;
    private float maxAttackTime;

    private bool canShoot;

    private void Start()
    {
        maxAttackTime = attackTime;
    }

    private void Update()
    {
        StateHandler();
    }

    private void StateHandler()
    {
        if(state == EnemyType.BasicShooter)
        {
            Vector3 temp = transform.position;
            temp.x += -speed * Time.deltaTime;
            transform.position = temp;

            Shoot();
        }
        else if (state == EnemyType.Bomber)
        {
            if(Vector2.Distance(transform.position, player.transform.position) > 2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            RotateTowardsTarget();
        }
    }

    private void Shoot()
    {
        GameObject obj = ObjectPooling.instance.GetPooledObjects("EnemyBullet");

        if (obj == null)
            return;

        if (attackTime < maxAttackTime)
            attackTime += Time.deltaTime;
        else if (attackTime >= maxAttackTime)
            canShoot = true;

        if (canShoot)
        {
            canShoot = false;
            attackTime = 0.0f;

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);
        }
    }

    private void RotateTowardsTarget()
    {
        var offset = 90f;
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
