using Oduncu.Events;
using UnityEngine;

namespace Oduncu
{
    public class BackgroundManager : MonoBehaviour
    {
        public GameObject normalBackground;
        public GameObject bossBackground;
        
        private void Awake()
        {
            BossSpawned.Subscribe(OnBossSpawned);
            BossKilled.Subscribe(OnBossKilled);
        }

        private void OnBossSpawned(object sender, BossSpawned.Args e)
        {
            normalBackground.SetActive(false);
            bossBackground.SetActive(true);
        }

        private void OnBossKilled(object sender, BossKilled.Args e)
        {
            normalBackground.SetActive(true);
            bossBackground.SetActive(false);
        }

        private void OnDestroy()
        {
            BossSpawned.Unsubscribe(OnBossSpawned);
            BossKilled.Unsubscribe(OnBossKilled);
        }
    }
    
    
}