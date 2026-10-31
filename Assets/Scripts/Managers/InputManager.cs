using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movements;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    private DataManager dataManager = null;
    private MovementList movementList = null;
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
        KeyHandler();
        if(Input.GetKeyDown(movementList.GetKeyCode(ActionId.quit)))
        {
            EventManager.TriggerEvent(EventId.QuitGame);
        }
    }

    private void MoveHandler()
    {
        if(player == null) { player = FindObjectOfType<Player>();Debug.Log("Player found."); }
        if (player == null || movementList == null) { return; }
        Vector2 direction = new Vector2();
        if (Input.GetKey(movementList.GetKeyCode(ActionId.moveUp))) { direction += Vector2.up; }
        if (Input.GetKey(movementList.GetKeyCode(ActionId.moveDown))) { direction += Vector2.down; }
        if (Input.GetKey(movementList.GetKeyCode(ActionId.moveLeft))) { direction += Vector2.left; }
        if (Input.GetKey(movementList.GetKeyCode(ActionId.moveRight))) { direction += Vector2.right; }
        if(direction != Vector2.zero)
        {
            direction.Normalize();
            direction *= player.GetSpeed();
            //Debug.Log("Moving player as direction: " + direction);
            player.Move(direction);
        }
    }

    private void KeyHandler()
    {
        MoveHandler();
        if(Input.GetKeyDown(movementList.GetKeyCode(ActionId.getTowerUp))) { player.GetTowerUp(); }   
        if(Input.GetKeyDown(movementList.GetKeyCode(ActionId.setTowerDown))) { player.SetTowerDown(); }
    }
}
