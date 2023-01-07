using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public void StartClosingAnimation(string sceneName)
    {
        LevelController.Instance.ClosingAnimation(sceneName);
    }
}
