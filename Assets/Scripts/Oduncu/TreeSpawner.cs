using System;
using System.Collections.Generic;
using Oduncu.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Oduncu
{
    public class TreeSpawner : MonoBehaviour
    {
        public float treeSpeed = 30f;
        public float treeCreationRatio = 0.05f;
        public float bossCreationRatio = 0.025f;
        public float updateRate = 0.05f;

        private HashSet<GameObject> m_Trees;
        private GameObject m_Boss;

        private float m_DeltaTime = 0f;

        private void Awake()
        {
            TreeKilled.Subscribe(OnTreeKilled);
            BossKilled.Subscribe(OnBossKilled);
        }

        private void Start()
        {
            m_Trees = new HashSet<GameObject>();
            NoTreesLeft.Invoke(this, EventArgs.Empty);
        }

        private void Update()
        {
            m_DeltaTime += Time.deltaTime;
            if (m_DeltaTime <= updateRate)
            {
                return;
            }

            m_DeltaTime -= updateRate;

            if (treeCreationRatio > Random.Range(0f, 1f))
            {
                if (m_Boss == null && bossCreationRatio >= Random.Range(0f, 1f))
                {
                    m_Boss = SpawnBoss();
                }
                else
                {
                    m_Trees.Add(SpawnTree());
                }
            }

            treeSpeed *= 1500f / 1499f;
            treeCreationRatio *= 1500f / 1499f;
        }

        private GameObject SpawnTree()
        {
            var tree = new GameObject();
            TreeSpawned.Invoke(this, new TreeSpawned.Args(tree));
            return tree;
        }

        private GameObject SpawnBoss()
        {
            var boss = new GameObject();
            BossSpawned.Invoke(this, new BossSpawned.Args(boss));
            return boss;
        }

        private void OnTreeKilled(object sender, TreeKilled.Args e)
        {
            m_Trees.Remove(e.GameObject);

            if (m_Trees.Count == 0 && m_Boss == null)
            {
                NoTreesLeft.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnBossKilled(object sender, BossKilled.Args e)
        {
            m_Boss = null;

            if (m_Trees.Count == 0)
            {
                NoTreesLeft.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnDestroy()
        {
            TreeKilled.Unsubscribe(OnTreeKilled);
            BossKilled.Unsubscribe(OnBossKilled);
        }
    }
}