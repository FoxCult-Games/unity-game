using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    [SerializeField] private Animator animator;

    private string nextSceneName;

    private const string FADE_IN = "level_crossfade_close";
    private const string FADE_OUT = "level_crossfade_open";

    private void Awake()
    {
        if(!Instance) Instance = this;

        DontDestroyOnLoad(Instance.gameObject);
    }

    private void Start()
    {
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
