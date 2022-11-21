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

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneSingle(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    #region RoomManager
    public async void SwitchRoom(int direction)
    {
        while (rooms == null || rooms.Count < 1)
        {
            await Task.Yield();
        }


        if (currentRoom + direction < rooms.Count)
        {
            SetRoomActive(currentRoom, false);
            SetRoomActive(currentRoom += direction, true);
        }
        else
        {
            SetRoomActive(currentRoom, false);
            currentRoom = 0;
            SwitchRoom(0);
        }
    }
    public void SetRoomActive(int index, bool isActive)
    {
        Room room = rooms[index];
        room.gameObject.SetActive(isActive);
    }

    public void AddRoom(Room room)
    {
        if (room != null)
            rooms.Add(room);
    }
    #endregion



}
