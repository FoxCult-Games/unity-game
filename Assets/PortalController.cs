using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        
        audioSource.Play();

        GameplayManager.Instance.GameContext.LevelManager.ChangeScene(sceneToLoad);
    }
}
