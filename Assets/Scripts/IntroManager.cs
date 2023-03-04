using GAF.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public MonoBehaviour introClip;

    private bool m_Playing;

    private bool m_Started;

    // Start is called before the first frame update
    private void Start()
    {
        m_Playing = false;
        m_Started = false;
    }

    // Update is called once per frame
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
        Destroy(introClip.gameObject);
    }
}