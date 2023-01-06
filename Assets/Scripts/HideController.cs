using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{

    [SerializeField] private GameObject canvasToHide;
    void OnTriggerEnter2D(Collider2D col)
    {
        canvasToHide.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canvasToHide.gameObject.SetActive(false);
    }
}
