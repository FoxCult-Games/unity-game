using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectibles
{
    using Managers;

    [Serializable]
    public class CollectiblesVFX
    {
        public GameObject collectedParticles;
    }
    
    public class Collectibles : MonoBehaviour
    {
        private Animator animator;
        
        [SerializeField] private CollectiblesVFX collectiblesVFX;
        
        [SerializeField] private UnityEvent onCollected;

        [SerializeField] private CollectiblesTypes type;
        [SerializeField] private int amount = 1;
        
        private const string COLLECTED_ANIMATION = "Collected";

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            GameplayManager.Instance.GameContext.CollectiblesController.Collect(type, amount);
            animator.Play(COLLECTED_ANIMATION);

            onCollected?.Invoke();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void CollectedEffect()
        {
            Instantiate(collectiblesVFX.collectedParticles, transform.position, Quaternion.identity);
        }
    }
}
