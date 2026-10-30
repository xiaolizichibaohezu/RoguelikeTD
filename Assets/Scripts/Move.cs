using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Character chara_s = null;
    GameObject chara = null;
    Vector2 move;

    private void Reset()
    {
        if (chara == null) { chara_s = GetComponent<Character>(); }
        if (chara_s == null) { chara = GetComponent<GameObject>(); }
    }

    private void MoveUp(Character chara_s)
    {
        move.Set(0, 1);
        move.Normalize();
        move.y *= chara_s.GetSpeed();
    }

    private void MoveDown(Character chara_s)
    {
        move.Set(0, -1);
        move.Normalize();
        move.y *= chara_s.GetSpeed();
    }

    private void MoveLeft(Character chara_s)
    {
        move.Set(-1, 0);
        move.Normalize();
        move.x *= chara_s.GetSpeed();
    }

    private void MoveRight(Character chara_s)
    {
        move.Set(1, 0);
        move.Normalize();
        move.x *= chara_s.GetSpeed();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
