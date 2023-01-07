using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectibles
{
    public class Collectibles : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCollected;

        [SerializeField] private CollectiblesTypes type;
        [SerializeField] private int amount = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            CollectiblesController.Instance.Collect(type, amount);

            onCollected?.Invoke();
        }
    }
}
