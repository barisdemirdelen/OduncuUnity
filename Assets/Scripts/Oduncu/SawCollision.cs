using Oduncu.Events;
using UnityEngine;

namespace Oduncu
{
    public class SawCollision : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Boss")
            {
                BossKilled.Invoke(this, new BossKilled.Args(other.gameObject));
            }
            else
            {
                TreeKilled.Invoke(this, new TreeKilled.Args(other.gameObject));
            }
        }
    }
}
