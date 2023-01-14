using UnityEngine;

namespace Managers
{
    using System;
    using System.Collections.Generic;
    using Collectibles;

    public interface IGameContext
    {
        IGameplayManager GameplayManager { get; }
        IAudioManager AudioManager { get; }
        IUIManager UIManager { get; }
        ILevelManager LevelManager { get; }
        ICollectiblesController CollectiblesController { get; }
    }

    public class GameContext : IGameContext
    {
        public IGameplayManager GameplayManager { get; set; }
        public IAudioManager AudioManager { get; set; }
        public IUIManager UIManager { get; set; }
        public ILevelManager LevelManager { get; set; }
        public ICollectiblesController CollectiblesController { get; set; }
    }

    public interface ISubManager
    {
        void Initialize(IGameContext gameContext);
    }

    public interface IGameplayManager
    {
        IGameContext GameContext { get; }
    }
    
    public class GameplayManager : MonoBehaviour, IGameplayManager
    {
        public static IGameplayManager Instance { get; private set; }
        
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private AudioManager audioManager;
        
        private IGameContext gameContext;

        private List<ISubManager> subManagers; 

        public IGameContext GameContext => gameContext;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            gameContext = new GameContext
            {
                GameplayManager = this,
                LevelManager = levelManager,
                UIManager = uiManager,
                AudioManager = audioManager,
                CollectiblesController = new CollectiblesController(gameContext),
            };
            
            subManagers = new List<ISubManager>
            {
                levelManager,
                uiManager,
                audioManager
            };
            
            foreach (var subManager in subManagers)
            {
                subManager.Initialize(gameContext);
            }
        }
    }
}
