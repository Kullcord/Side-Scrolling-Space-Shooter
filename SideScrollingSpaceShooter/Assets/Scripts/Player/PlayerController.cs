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

    //[SerializeField] private GameObject projectilePrefab;

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
        gameObject.SetActive(false);
    }

    #region Animation
    /*[Header("Animations")]
    [SerializeField] private float animRotation;
    [SerializeField] private float interpolationValue;
    [SerializeField] private AnimationCurve animCurve;
    bool animating = false;*/

    /*if (moveDirection.y != 0)
        AnimationHandler();
    else
    {
        transform.rotation = Quaternion.identity;
        Debug.Log("4");
    }*/
    /*private void AnimationHandler()
    {
        if (!animating)
            if (moveDirection.y > 0)
            {
                StopAllCoroutines();
                StartCoroutine(AnimateMovement(animRotation));
                Debug.Log("1");
            }
            else if (moveDirection.y < 0)
            {
                StopAllCoroutines();
                StartCoroutine(AnimateMovement(-animRotation));
                Debug.Log("2");
            }
            else if (moveDirection.y == 0)
            {
                transform.rotation = Quaternion.identity;
                Debug.Log("4");
            }
    }

    IEnumerator AnimateMovement(float rotation)
    {
        animating = true;
        var progression = 0.0f;
        Quaternion rotToGo = Quaternion.Euler(0, 0, rotation);

        while (progression < 0.99f)
        {
            progression += interpolationValue * Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, rotToGo, animCurve.Evaluate(progression));

            yield return new WaitForEndOfFrame();
        }

        transform.rotation = rotToGo;
        animating = false;

    }*/
    #endregion
}
