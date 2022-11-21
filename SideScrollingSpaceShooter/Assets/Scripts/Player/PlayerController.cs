using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [Header("Movement")]
    Vector3 moveDirection;
    [SerializeField] private float moveSpeed = 3.0f;

    [Header("Stats")]
    [SerializeField] private Slider healthBar;
    public float health = 100f;
    public float maxHealthAmount;

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

        maxHealthAmount = health;
        healthBar.maxValue = maxHealthAmount;
        healthBar.value = health;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
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
