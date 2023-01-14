using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    using Unity.VisualScripting;

    public interface ILevelManager
    {
        void ChangeScene(string scene);
        void LoadNextScene();
    }
    
    public class LevelManager : MonoBehaviour, ISubManager, ILevelManager
    {
        [SerializeField] private Animator animator;

        [SerializeField] private string nextSceneName;

        private IGameContext gameContext;
        
        private const string FADE_IN = "level_crossfade_close";
        private const string FADE_OUT = "level_crossfade_open";

        public void Initialize(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            animator.Play(FADE_OUT);
        }

        public void ChangeScene(string scene)
        {
            nextSceneName = scene;
            animator.Play(FADE_IN);
        }
    
        public void LoadNextScene()
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
