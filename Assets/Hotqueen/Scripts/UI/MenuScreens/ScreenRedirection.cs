using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenRedirection : MonoBehaviour
{

    public void GoToScene(string scene)
    {
        GameManager.Instance.LoadSceneAdditive(scene);
        GameManager.Instance.SwitchRoom(0);
    }
}
