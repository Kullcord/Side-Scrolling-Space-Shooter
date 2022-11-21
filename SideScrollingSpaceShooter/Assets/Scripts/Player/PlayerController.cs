using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [Header("Movement")]
    Vector3 moveDirection;
    [SerializeField] private float moveSpeed = 3.0f;

    bool speedUp;
    [SerializeField] float speedUpMultiplier;

    [Header("Attacking")]
    [SerializeField] private GameObject projectileSpawn;

    [SerializeField] private float attackTime;
    private float maxAttackTime;
    private bool canShoot;

    [Header("Min/Max")]
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [SerializeField] float minX;
    [SerializeField] float maxX;

    public float health = 100f;

    #endregion

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        maxAttackTime = attackTime;
    }

    private void Update()
    {
        MovePlayer();
        Shoot();
    }

    private void MovePlayer()
    {
        Vector3 temp = transform.position;

        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        speedUp = Input.GetButton("Speed Up");

        temp += Time.deltaTime * moveSpeed * (speedUp ? speedUpMultiplier : 1) * moveDirection.normalized;

        if (temp.y <= minY)
            temp.y = maxY;
        else if (temp.y >= maxY)
            temp.y = minY;

        if (temp.x >= maxX)
            temp.x = maxX;
        else if (temp.x <= minX)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        transform.position = temp;
    }

    private void Shoot()
    {
        GameObject obj = ObjectPooling.instance.GetPooledObjects("PlayerBullet");

        if (obj == null)
            return;

        if(attackTime < maxAttackTime)
            attackTime += Time.deltaTime;
        else if (attackTime >= maxAttackTime)
            canShoot = true;

        if (Input.GetButton("Fire1") && canShoot)
        {
            canShoot = false;
            attackTime = 0.0f;

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ent");

        if(collision.tag == "EnemyBullet")
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            Debug.Log(">>>");
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void DeactivateObject()
    {
        GameObject obj = ObjectPooling.instance.GetPooledObjects("PlayerExplosion");

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

        gameObject.SetActive(false);
    }
}
