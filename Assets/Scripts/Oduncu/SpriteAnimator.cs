using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Oduncu
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        public List<Sprite> sprites;
        public bool play;
        public bool loop;
        public int fps = 60;

        private SpriteRenderer m_SpriteRenderer;
        private int m_CurrentIndex;
        private bool m_Playing;

        private void Start()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_CurrentIndex = 0;

            if (play)
            {
                Play();
            }
        }

        public async void Play()
        {
            m_CurrentIndex = 0;
            m_Playing = true;

            while (m_Playing)
            {
                m_SpriteRenderer.sprite = sprites[m_CurrentIndex];
                m_CurrentIndex += 1;

                if (m_CurrentIndex >= sprites.Count)
                {
                    if (!loop)
                    {
                        m_Playing = false;
                    }

                    m_CurrentIndex = 0;
                }

                await Task.Delay((int)(1000.0 / fps));
            }
        }

        private void OnDestroy()
        {
            m_Playing = false;
        }
    }
}