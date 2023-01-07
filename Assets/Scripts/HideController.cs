using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{

    [SerializeField] private GameObject canvasToHide;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.compareTag("player")) {
            canvasToHide.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (col.compareTag("player")) {
            canvasToHide.gameObject.SetActive(false);
        }
    }
}
