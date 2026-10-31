using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float actGap = 0.5f;
    [SerializeField]private bool _getTowerUp = false;
    [SerializeField]private float distance = 0.35f;
    public bool getTowerUp
    {
        get { return _getTowerUp; }
    }
    private string towerType = TowerType.normal;
    private Player player = null;

    public void PickedUp(Player player)
    {
        this.player = player;
        _getTowerUp = true;
    }

    public void PutDown()
    {
        this.player = null;
        _getTowerUp = false;
    }

    private void FollowPlayer()
    {
        if(getTowerUp && player != null)
        {
            Vector2 vector2 = player.transform.position;
            Quaternion t_quaternion = player.transform.rotation;
            Vector2 t_vector2 = t_quaternion * new Vector2(distance, 0f);
            transform.position = t_vector2 + vector2;
            transform.rotation = t_quaternion;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
}
