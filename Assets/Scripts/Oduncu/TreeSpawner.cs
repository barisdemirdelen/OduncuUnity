using System;
using System.Collections.Generic;
using DG.Tweening;
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

        public List<GameObject> treePrefabs;
        public GameObject bossPrefab;
        public GameObject treeContainer;

        private HashSet<GameObject> m_Trees;
        private GameObject m_Boss;

        private bool m_Flipping = false;

        private float m_DeltaTime = 0f;

        private void Awake()
        {
            TreeKilled.Subscribe(OnTreeKilled);
            BossKilled.Subscribe(OnBossKilled);
            AdamKilled.Subscribe(OnAdamKilled);
        }

        private void Start()
        {
            m_Trees = new HashSet<GameObject>();
            NoTreesLeft.Invoke(this, new NoTreesLeft.Args());
        }

        private void Update()
        {

            
            m_DeltaTime += Time.deltaTime;
            if (m_DeltaTime <= updateRate)
            {
                return;
            }

            m_DeltaTime -= updateRate;
            
            if (m_Flipping)
            {
                foreach (var tree in m_Trees)
                {
                    var localScale = tree.transform.localScale;
                    tree.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                }
                return;
            }

            var r = Random.Range(0f, 1f);
            if (treeCreationRatio > r)
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
            var treePrefab = treePrefabs[Random.Range(0, treePrefabs.Count)];
            var tree = Spawn(treePrefab, 240f);
            TreeSpawned.Invoke(this, new TreeSpawned.Args(tree));
            return tree;
        }

        private GameObject SpawnBoss()
        {
            var boss = Spawn(bossPrefab, 760f);
            boss.name = "Boss";

            BossSpawned.Invoke(this, new BossSpawned.Args(boss));
            return boss;
        }

        private GameObject Spawn(GameObject prefab, float offset)
        {
            var tree = Instantiate(prefab, treeContainer.transform);
            var position = tree.transform.localPosition;

            if (Random.Range(0f, 1f) < 0.5f)
            {
                tree.transform.localPosition = new Vector3(((RectTransform)treeContainer.transform).rect.xMax + offset,
                    position.y, position.z);
                tree.GetComponent<Rigidbody2D>().velocity = new Vector2(-treeSpeed, 0);
            }
            else
            {
                tree.transform.localPosition = new Vector3(((RectTransform)treeContainer.transform).rect.xMin - offset,
                    position.y, position.z);
                tree.GetComponent<Rigidbody2D>().velocity = new Vector2(treeSpeed, 0);
                var localScale = tree.transform.localScale;
                localScale = new Vector2(-localScale.x, localScale.y);
                tree.transform.localScale = localScale;
            }

            return tree;
        }

        private void OnTreeKilled(object sender, TreeKilled.Args e)
        {
            m_Trees.Remove(e.gameObject);
            var localTransform = e.gameObject.transform;
            var localPosition = localTransform.localPosition;

            e.gameObject.GetComponent<Rigidbody2D>().simulated = false;

            var rotation = localTransform.localScale.x > 0 ? -90f : 90f;
            
            DOTween.Sequence().Join( e.gameObject.transform.DOLocalMove(
                    new Vector3(localPosition.x, localPosition.y - 1080, localPosition.z),
                    2.5f).SetEase(Ease.Linear)).Join(e.gameObject.transform.DOLocalRotate(
                new Vector3(0f, 0f, rotation),
                2.5f).SetEase(Ease.Linear)).OnComplete(() => Destroy(e.gameObject));
            
           

            if (m_Trees.Count == 0 && m_Boss == null)
            {
                NoTreesLeft.Invoke(this, new NoTreesLeft.Args());
            }
        }

        private void OnBossKilled(object sender, BossKilled.Args e)
        {
            var localTransform =  e.gameObject.transform;
            var localPosition = localTransform.localPosition;
            e.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            e.gameObject.transform.DOLocalMove(
                new Vector3(localPosition.x, localPosition.y - 1080, localPosition.z),
                4f).SetEase(Ease.Linear).OnComplete(() => Destroy(e.gameObject));
            m_Boss = null;

            if (m_Trees.Count == 0)
            {
                NoTreesLeft.Invoke(this, new NoTreesLeft.Args());
            }
        }
        
        
        private void OnAdamKilled(object sender, AdamKilled.Args e)
        {
            m_Flipping = true;

            foreach (var tree in m_Trees)
            {
                var rigidBody = tree.GetComponent<Rigidbody2D>();
                rigidBody.velocity = new Vector2(0, 0);
                rigidBody.simulated = false;
            }
        }


        private void OnDestroy()
        {
            Destroy(treeContainer);

            TreeKilled.Unsubscribe(OnTreeKilled);
            BossKilled.Unsubscribe(OnBossKilled);
            AdamKilled.Unsubscribe(OnAdamKilled);
        }
    }
}