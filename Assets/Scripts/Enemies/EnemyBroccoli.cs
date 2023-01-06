using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyBroccoli : Enemy
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            TakeDamage(transform.position - other.transform.position, 1);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;

            col.gameObject.GetComponent<CharacterController2D>().TakeDamage(col.transform.position - transform.position, 1);
        }
    }
}
