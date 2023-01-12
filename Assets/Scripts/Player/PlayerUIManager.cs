using System;
using TMPro;
using UnityEngine;

namespace Player
{
    using System.Linq;

    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject Canvas;
        
        [SerializeField] private Transform livesCounter;
        [SerializeField] private GameObject heartPrefab;
        
        private CharacterController2D characterController2D;

        private void Awake()
        {
            characterController2D = GetComponent<CharacterController2D>();
        }

        private void Start()
        {
            for (int i = 0; i < characterController2D.Health; i++)
            {
                Instantiate(heartPrefab, livesCounter);
            }
        }

        public void RefreshLivesCounter()
        {
            if (characterController2D.Health <= 0)
                return;
            
            livesCounter.Cast<Transform>().ToArray()[characterController2D.Health - 1].gameObject.SetActive(false);
        }
    }
}