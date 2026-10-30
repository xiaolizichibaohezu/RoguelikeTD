using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
        ResetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetGameManager()
    {
        EventManager.SubscribeToEvent(OnEventTriggered);
    }

    private void OnEventTriggered(string eventName, object[] parameters)
    {
        switch (eventName)
        {
            case "QuitGame":
                Debug.Log("Quitting game.");
                Application.Quit();
                EventManager.TriggerEvent("SaveData");
                break;
            default:
                Debug.LogWarning("Event not handled: " + eventName);
                break;
        }
    }
}
