using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator anim;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();

        var randXY = Random.Range(0.75f, 1.0f);
        var randRotSpeed = Random.Range(rotationSpeed / 2, rotationSpeed * 2);

        transform.localScale = new Vector3(randXY, randXY, transform.localScale.z);
        rotationSpeed = randRotSpeed;

        Invoke(nameof(DeactivateObject), anim.GetCurrentAnimatorStateInfo(0).length);
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
