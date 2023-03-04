using GAF.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oduncu
{
    public class IntroManager : MonoBehaviour
    {
        public MonoBehaviour introClip;

        private bool m_Playing;

        private bool m_Started;

        private void Start()
        {
            m_Playing = false;
            m_Started = false;
        }

        private void Update()
        {
            var clip = (GAFMovieClip)introClip;
            m_Playing = clip.isPlaying();
            if (m_Playing)
            {
                m_Started = true;
            }

            if (!m_Started || m_Playing) return;

            ChangeScene();
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}