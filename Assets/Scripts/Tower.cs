using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected float actGap = 0.5f;
    private List<Enemy> enemiesInRange = new();
    private Enemy target = null;
    private Transform gun = null;
    [SerializeField]private bool _getTowerUp = false;
    [SerializeField]private float distance = 0.35f;
    protected string towerType = TowerType.defaultType;
    private Player player = null;

    public bool getTowerUp
    {
        get { return _getTowerUp; }
    }

    protected virtual void ResetTower()
    {
        _getTowerUp = false;
        player = null;
        actGap = 0.5f;
        gun = transform.GetChild(0);
    }

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

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy != null && !enemiesInRange.Contains(enemy)) { enemiesInRange.Add(enemy); }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && enemiesInRange.Contains(enemy)) { enemiesInRange.Remove(enemy); }
        }
    }

    protected void ChooseTarget()
    {
        if (enemiesInRange.Count != 0)
        {
            target = enemiesInRange[0];
            foreach (Enemy enemy in enemiesInRange)
            {
                if(enemy == null)
                {
                    enemiesInRange.Remove(enemy);
                    continue;
                }
                if (target.GetSignNum() > enemy.GetSignNum() || (target.GetSignNum() == enemy.GetSignNum() && target.GetDistance() > enemy.GetDistance()))
                {
                    target = enemy;
                }
            }
        }
        else { target = null; }
    }

    private IEnumerator UpdateTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            ChooseTarget();
            yield return wait;
        }
    }

    protected virtual void FaceToTarget()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;
            Vector3 direction = (targetPos - gun.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            gun.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    protected virtual void Awake()
    {
        ResetTower();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        StartCoroutine(UpdateTarget());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FollowPlayer();
        FaceToTarget();
    }
}
