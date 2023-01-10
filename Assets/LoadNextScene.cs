using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene : MonoBehaviour
{
    private void ChangeScene()
    {
        LevelController.Instance.LoadNextScene();
    }
}
