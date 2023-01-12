using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class HideController : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private GameObject canvasToHide;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) {
            audioSource.Play();
            
            canvasToHide.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) {
            audioSource.Stop();
            
            canvasToHide.gameObject.SetActive(false);
        }
    }
}
