using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CrossfadeAnimation : MonoBehaviour
{
    public void LoadScene()
    {
        LevelController.Instance.LoadNextScene();
    }
}
