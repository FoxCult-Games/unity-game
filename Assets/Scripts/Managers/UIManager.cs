namespace Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Player;
    using UI;

    public interface IUIManager
    {
        void RefreshLivesCounter();
        void RefreshCollectiblesCounter(CollectiblesTypes type);
    }

    public class UIManager : MonoBehaviour, ISubManager, IUIManager
    {
        [SerializeField] private GameObject Canvas;
        
        [SerializeField] private Transform livesCounter;
        [SerializeField] private GameObject heartPrefab;
        
        [SerializeField] private CharacterController2D characterController2D;
        [SerializeField] private List<CollectiblesView> views;
        
        private IGameContext gameContext;
        
        
        public void Initialize(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            
            for (int i = 0; i < characterController2D.Health; i++)
            {
                Instantiate(heartPrefab, livesCounter);
            }

            foreach (var view in views)
            {
                view.Initialize(gameContext);
            }
        }

        public void RefreshLivesCounter()
        {
            if (characterController2D.Health <= 0)
                return;
            
            livesCounter.Cast<Transform>().ToArray()[characterController2D.Health - 1].gameObject.SetActive(false);
        }

        public void RefreshCollectiblesCounter(CollectiblesTypes type)
        {
            views.FirstOrDefault((view) => view.CollectibleType == type)?.Refresh();
        }
    }
}