using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionId
{
    public static readonly string moveUp = "moveUp";
    public static readonly string moveDown = "moveDown";
    public static readonly string moveLeft = "moveLeft";
    public static readonly string moveRight = "moveRight";
    public static readonly string quit = "quit";
    public static readonly string getTowerUp = "getTowerUp";
    public static readonly string setTowerDown = "setTowerDown";

}

public static class DamageType
{
    public static readonly string normal = "normal";
}

public static class TowerType
{
    public static readonly string normal = "normal";
}

namespace Movements
{

    [System.Serializable]
    public struct KeyBinding
    {
        public string actionId;
        public KeyCode keyCode;
    }

    [System.Serializable]
    public class MovementList
    {
        private Dictionary<string, KeyCode> keyCommandMap = new Dictionary<string, KeyCode>();
        public List<KeyBinding> keyBindings = new List<KeyBinding>();
        public void BindMap(string actionId, KeyCode key)
        {
            keyCommandMap[actionId] = key;
        }
        public void UnbindMap(string actionId)
        {
            if (keyCommandMap.ContainsKey(actionId))
            {
                keyCommandMap.Remove(actionId);
            }
        }
        public KeyCode GetKeyCode(string actionId)
        {
            return keyCommandMap[actionId];
        }
        public void ToList()
        {
            keyBindings.Clear();
            foreach (var kvp in keyCommandMap)
            {
                keyBindings.Add(new KeyBinding { actionId = kvp.Key, keyCode = kvp.Value });
            }
            Debug.Log("Movement list converted to list:" + keyBindings.Count);
            foreach (var binding in keyBindings)
            {
                Debug.Log("Action: " + binding.actionId + ", Key: " + binding.keyCode);
            }
        }
        public void FromList()
        {
            keyCommandMap.Clear();
            foreach (var binding in keyBindings)
            {
                keyCommandMap[binding.actionId] = binding.keyCode;
            }
        }
    }
}


/*
public class MoveUpCommand : ICommand
{
    private readonly Character _character;
    private readonly Vector2 _vector2;

    public MoveUpCommand(Character character, Vector2 vector2)
    {
        _character = character;
        _vector2 = vector2;
    }
    public void Execute()
    {
        _character.Move(_vector2);
    }

    public void Undo()
    {
        // Implementation for undoing move up
        return;
    }
}
*/