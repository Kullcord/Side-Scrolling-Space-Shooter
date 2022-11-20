﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float deactivationTimer = 2f;

    private void OnEnable()
    {
        Invoke(nameof(DestroyProjectile), deactivationTimer);
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

    private void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Asteroid")// || == "Enemy"
        {
            DestroyProjectile();
        }
    }
}