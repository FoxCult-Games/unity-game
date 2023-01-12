using UnityEngine;

namespace Managers
{
    using System;
    using System.Collections.Generic;

    public interface IGameContext
    {
        GameplayManager GameplayManager { get; }
        AudioManager AudioManager { get; }
        UIManager UIManager { get; }
        LevelManager LevelManager { get; }
    }

    public class GameContext : IGameContext
    {
        public GameplayManager GameplayManager { get; set; }
        public AudioManager AudioManager { get; set; }
        public UIManager UIManager { get; set; }
        public LevelManager LevelManager { get; set; }
    }

    public interface ISubManager
    {
        void Initialize(IGameContext gameContext);
    }
    
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }
        
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
                AudioManager = audioManager
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
