using Player;
using UnityEngine;

namespace Enemies
{
    using System;

    public class EnemyBroccoli : Enemy
    {
        private bool isTriggered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            isTriggered = true;
            
            TakeDamage(transform.position - other.transform.position, 1);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            isTriggered = false;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Player") || isTriggered) return;

            col.gameObject.GetComponent<CharacterController2D>().TakeDamage(col.transform.position - transform.position, 1);
        }
    }
}
