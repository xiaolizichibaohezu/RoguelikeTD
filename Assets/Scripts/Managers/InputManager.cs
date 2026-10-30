using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movements;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    private DataManager dataManager = null;
    private MovementList movementList = null;
    private ActionId actionId = new ActionId();
    private Player player = null;

    private void ResetDataManager()
    {
        dataManager = DataManager.instance;
        if (dataManager != null) { movementList = dataManager.GetMovementList();Debug.Log("Movement list initialized."); }
        player = FindObjectOfType<Player>();
        if(player != null) { Debug.Log("Player found."); }
    }

    void Awake()
    {
        if(instance == null) { instance = this; }
        else { Destroy(this); }
        ResetDataManager();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveHandler();
        if(Input.GetKeyDown(movementList.GetKeyCode(actionId.quit)))
        {
            EventManager.TriggerEvent("QuitGame");
        }
    }

    private void MoveHandler()
    {
        if(player == null) { player = FindObjectOfType<Player>();Debug.Log("Player found."); }
        if (player == null || movementList == null) { return; }
        Vector2 direction = new Vector2();
        if (Input.GetKey(movementList.GetKeyCode(actionId.moveUp))) { direction += Vector2.up; }
        if (Input.GetKey(movementList.GetKeyCode(actionId.moveDown))) { direction += Vector2.down; }
        if (Input.GetKey(movementList.GetKeyCode(actionId.moveLeft))) { direction += Vector2.left; }
        if (Input.GetKey(movementList.GetKeyCode(actionId.moveRight))) { direction += Vector2.right; }
        if(direction != Vector2.zero)
        {
            direction.Normalize();
            direction *= player.GetSpeed();
            Debug.Log("Moving player as direction: " + direction);
            player.Move(direction);
        }
    }
}
