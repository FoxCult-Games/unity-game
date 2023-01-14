using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class LoadNextScene : MonoBehaviour
{
    private void ChangeScene()
    {
        GameplayManager.Instance.GameContext.LevelManager.LoadNextScene();
    }
}
