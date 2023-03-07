using System;
using Oduncu.Events;
using UnityEngine;

public class AdamController : MonoBehaviour
{
    private Camera m_MainCamera;

    private void Start()
    {
        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var mousePosition = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        var currentTransform = transform;

        var sign = mousePosition.x < currentTransform.position.x ? -1 : 1;
        var localScale = currentTransform.localScale;
        localScale.x = sign * localScale.x;
        currentTransform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AdamKilled.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
