using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oduncu
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject titleStable;
        public GameObject titleMoving;

        public AudioSource sawTurnOn;
        
        private void Start()
        {
            titleStable.SetActive(true);
            titleMoving.SetActive(false);
        }

        public async void OnStartPressed()
        {
            titleStable.SetActive(false);
            titleMoving.SetActive(true);
            
            sawTurnOn.Play();

            await Task.Delay(1744);

            SceneManager.LoadScene("Game");
        }
    }
}
