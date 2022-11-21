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
    public void GoToScreen(GameObject screen)
    {
        screen.gameObject.SetActive(true);
        GameManager.Instance.GetCameraSettings().FollowTarget(screen.transform);
        GameManager.Instance.GetCameraSettings().LookAt(screen.transform);
        Transform pPosition = screen.transform.Find("PlayerPosition");
        if (pPosition)
        {
            Player player = FindObjectOfType<Player>();
            player.DisableInventory();
            player.transform.position = pPosition.transform.position;
        }
    }
}
