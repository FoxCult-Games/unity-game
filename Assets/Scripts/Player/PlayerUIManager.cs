using System;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI livesCounter;
        
        private CharacterController2D characterController2D;

        private void Awake()
        {
            characterController2D = GetComponent<CharacterController2D>();
        }

        private void Start()
        {
            RefreshLivesCounter();
        }

        public void RefreshLivesCounter()
        {
            livesCounter.text = "Lives: " + characterController2D.Health;
        }
    }
}