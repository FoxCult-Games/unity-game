namespace Managers
{
    using System.Linq;
    using UnityEngine;
    using Player;

    public class UIManager : MonoBehaviour, ISubManager
    {
        [SerializeField] private GameObject Canvas;
        
        [SerializeField] private Transform livesCounter;
        [SerializeField] private GameObject heartPrefab;
        
        [SerializeField] private CharacterController2D characterController2D;

        private IGameContext gameContext;
        
        public void Initialize(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            
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