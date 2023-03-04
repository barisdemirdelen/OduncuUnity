using UnityEngine;

namespace Oduncu.ScaleUtil
{
    public class FitToScreen : MonoBehaviour
    {
        private SpriteRenderer m_Sr;

        private void Start()
        {
            m_Sr = GetComponent<SpriteRenderer>();
        
            if (Camera.main == null)
            {
                return;
            }

            // world height is always camera's orthographicSize * 2
            var worldScreenHeight = Camera.main.orthographicSize * 2;

            // world width is calculated by diving world height with screen height
            // then multiplying it with screen width
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            // to scale the game object we divide the world screen width with the
            // size x of the sprite, and we divide the world screen height with the
            // size y of the sprite
            var sprite = m_Sr.sprite;
            transform.localScale = new Vector3(
                worldScreenWidth / sprite.bounds.size.x,
                worldScreenHeight / sprite.bounds.size.y, 1);
        }

    }
}