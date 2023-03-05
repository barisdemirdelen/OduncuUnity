using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private Camera m_MainCamera;

    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var mousePosition = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);

        m_SpriteRenderer.flipX = mousePosition.x < transform.position.x;
    }
}
