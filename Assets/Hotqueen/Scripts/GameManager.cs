using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    private List<Room> rooms = new List<Room>();
    private int currentRoom;

    #region Scene
    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneSingle(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        instance = null;
    }
    #endregion

    #region MainUI
    public CameraSettings GetCameraSettings()
    {
        return GameObject.FindObjectOfType<CameraSettings>();
    }

    public GameObject GetMainUI()
    {
        return GameObject.Find("MainUI");
    }
    public GameObject GetCreditsUI()
    {
        return GameObject.Find("CreditsUI");
    }
    #endregion

    #region RoomManager
    public async void SwitchRoom(int direction)
    {
        //if there is no room in list rooms 
        //wait until room is initialized
        while (rooms == null || rooms.Count < 1)
        {
            await Task.Yield();
        }

        //deactivate current room
        SetRoomActive(currentRoom, false);

        currentRoom += direction;
        Debug.Log("CurrentRoom index " + currentRoom);

        if (currentRoom > rooms.Count - 1)
        {
            currentRoom = 0;
        }
        else if (currentRoom < 0)
        {
            currentRoom = rooms.Count - 1;
        }

        SetRoomActive(currentRoom, true);
    }

    public void SetRoomActive(int index, bool isActive)
    {
        // Debug.Log("set active");
        Room room = rooms[index];
        room.gameObject.SetActive(isActive);
    }

    public void AddRoom(Room room)
    {
        if (room != null)
            rooms.Add(room);
    }

    public void DisableCurrentRoom()
    {
        SetRoomActive(currentRoom, false);
    }
    #endregion
}
public class ScreenRedirection
{
    public static void GoToScene(string scene)
    {
        GameManager.Instance.LoadSceneAdditive(scene);
        GameManager.Instance.SwitchRoom(0);
    }
    public static void Reload()
    {
        GameManager.Instance.ReloadGame();
    }
    public static void GoToScreen(GameObject screen)
    {
        screen.gameObject.SetActive(true);
        GameManager.Instance.GetCameraSettings().FollowTarget(screen.transform);
        GameManager.Instance.GetCameraSettings().LookAt(screen.transform);
        Transform pPosition = screen.transform.Find("PlayerPosition");
        if (pPosition)
        {
            Player player = GameObject.FindObjectOfType<Player>();
            player.DisableInventory();
            player.transform.position = pPosition.transform.position;
        }
    }
}
