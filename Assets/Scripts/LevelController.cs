using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    [SerializeField] private Animator transition;
    [SerializeField] float transitionTime = 1.0f;

    [SerializeField] private UnityEvent onSceneOpenTransition;
    [SerializeField] private UnityEvent<int> onSceneCloseTransition;

    private void Awake()
    {
        if(!Instance) Instance = this;

        DontDestroyOnLoad(Instance.gameObject);
    }

    public void ClosingAnimation(string sceneName)
    {
        int levelIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);

        SceneManager.LoadScene(levelIndex);

        onSceneCloseTransition?.Invoke(levelIndex);
        onSceneOpenTransition?.Invoke();
    }
}
