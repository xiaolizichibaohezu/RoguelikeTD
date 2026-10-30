using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Movements;

class GameData
{
    public MovementList movementList;
}

public class DataManager : MonoBehaviour
{
    private ActionId actionId = new ActionId();
    private GameData gameData = null;
    public static DataManager instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
        if (gameData == null) { ResetGameManager(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetGameManager()
    {
        gameData = new GameData()
        {
            movementList = new MovementList()
        };
        LoadData();
        EventManager.SubscribeToEvent(OnEventTriggered);
    }

    private void OnDestroy()
    {
        EventManager.UnsubscribeFromEvent(OnEventTriggered);
    }

    private void OnEventTriggered(string eventName, object[] parameters)
    {
        switch (eventName)
        {
            case "SaveData":
                SaveData();
                break;
            case "LoadData":
                LoadData();
                break;
            default:
                Debug.LogWarning("Event not handled: " + eventName);
                break;
        }
    }

    private void SaveData()
    {
        gameData.movementList.ToList();
        string json = JsonUtility.ToJson(gameData,true);
        string filePath = Application.streamingAssetsPath + "/PlayerMovementMaps.json";

        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine(json);
            sw.Close();
            sw.Dispose();
            Debug.Log("Data saved:" + json);
        }
    }

    private void LoadData()
    {
        Debug.Log("Loading data...");
        string filePath = Application.streamingAssetsPath + "/PlayerMovementMaps.json";
        if (File.Exists(filePath))
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();
                Debug.Log("Data loaded: " + json);
                gameData = JsonUtility.FromJson<GameData>(json);
                gameData.movementList.FromList();
                sr.Close();
                sr.Dispose();
            }
        }
        else { GenerateData(); }
    }

    private void GenerateData()
    {
        gameData.movementList.BindMap(actionId.moveUp, KeyCode.W);
        gameData.movementList.BindMap(actionId.moveDown, KeyCode.S);
        gameData.movementList.BindMap(actionId.moveLeft, KeyCode.A);
        gameData.movementList.BindMap(actionId.moveRight, KeyCode.D);
        gameData.movementList.BindMap(actionId.quit, KeyCode.Escape);
        gameData.movementList.ToList();
        Debug.Log("Movement data generated.");
    }

    public MovementList GetMovementList()
    {
        return gameData.movementList;
    }
}
