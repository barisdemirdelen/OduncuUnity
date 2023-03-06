using System;
using Oduncu.Events;
using UnityEngine;

namespace Oduncu
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource saw;
        public AudioSource sawHit;
        public AudioSource treeWalk;
        public AudioSource bossWalk;
        public AudioSource bossMusic;

        private void Awake()
        {
            TreeSpawned.Subscribe(PlayTreeWalkSound);
            NoTreesLeft.Subscribe(StopTreeWalkSound);
            BossSpawned.Subscribe(PlayBossMusic);
            TreeKilled.Subscribe(PlaySawHitSound);
            BossKilled.Subscribe(StopBossMusic);
        }

        private void Start()
        {
            saw.Stop();
            saw.Play();
        }

        private void PlayTreeWalkSound(object sender, TreeSpawned.Args e)
        {
            if (!treeWalk.isPlaying)
            {
                treeWalk.Play();
            }
        }

        private void StopTreeWalkSound(object sender, EventArgs e)
        {
            treeWalk.Stop();
        }

        private void PlayBossMusic(object sender, BossSpawned.Args e)
        {
            if (!bossMusic.isPlaying)
            {
                bossMusic.Play();
            }
        }

        private void StopBossMusic(object sender, BossKilled.Args e)
        {
            bossMusic.Stop();
        }

        private void PlaySawHitSound(object sender, TreeKilled.Args e)
        {
            sawHit.Stop();
            sawHit.Play();
        }

        private void OnDestroy()
        {
            TreeSpawned.Unsubscribe(PlayTreeWalkSound);
            NoTreesLeft.Unsubscribe(StopTreeWalkSound);
            BossSpawned.Unsubscribe(PlayBossMusic);
            TreeKilled.Unsubscribe(PlaySawHitSound);
            BossKilled.Unsubscribe(StopBossMusic);
        }
    }
}