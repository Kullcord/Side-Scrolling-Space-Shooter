using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    #region Fields
    Vector3 moveDirection;
    [SerializeField] private float moveSpeed = 3.0f;

    bool speedUp;
    [SerializeField] float speedUpMultiplier;

    [SerializeField] float minY;
    [SerializeField] float maxY;

    [SerializeField] float minX;
    [SerializeField] float maxX;
    #endregion

    private void Update()
    {
        MovePlayer();
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
        {
            Debug.Log("ded");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        transform.position = temp;
    }
}
