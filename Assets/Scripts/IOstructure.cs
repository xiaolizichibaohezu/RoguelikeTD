using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;


namespace Movements
{
    class ActionId
    {
        public string moveUp = "moveUp";
        public string moveDown = "moveDown";
        public string moveLeft = "moveLeft";
        public string moveRight = "moveRight";
        public string quit = "quit";

    }

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