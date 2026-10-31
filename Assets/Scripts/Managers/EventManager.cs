using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<string, object[]> OnEventTriggered;
    public static void TriggerEvent(string eventName, params object[] parameters)
    {
        OnEventTriggered?.Invoke(eventName, parameters);
    }
    public static void SubscribeToEvent(Action<string, object[]> listener)
    {
        OnEventTriggered += listener;
    }
    public static void UnsubscribeFromEvent(Action<string, object[]> listener)
    {
        OnEventTriggered -= listener;
    }
}
