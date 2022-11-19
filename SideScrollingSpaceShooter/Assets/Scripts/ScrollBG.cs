using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;
    private float xScroll;

    [SerializeField] private MeshRenderer mRend;

    private void Awake()
    {
        mRend = GetComponent<MeshRenderer>();    
    }

    private void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        xScroll = Time.time * scrollSpeed;
        Vector2 offset = new Vector2(xScroll, 0f);

        mRend.material.mainTextureOffset = offset;
    }
}
