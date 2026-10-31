using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Transform roadSignTransform = null;
    private RoadSign roadSign = null;
    public EnemyObject _enemyObject;

    public void SetRoadSign(RoadSign nxtSign)
    {
        roadSign = nxtSign;
        roadSignTransform = nxtSign.transform;
    }

    public int GetSignNum()
    {
        if (roadSign != null)
        {
            return roadSign.signId;
        }
        return -1;
    }

    public double GetDistance()
    {
        if (roadSignTransform != null)
        {
            return Vector2.Distance(transform.position, roadSignTransform.position);
        }
        return -1;
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    protected override void ResetCharacter()
    {
        maxHealthPoint = _enemyObject.maxHealth;
        maxMoveSpeed = _enemyObject.maxSpeed;
        base.ResetCharacter();
    }

    private void Awake()
    {
        ResetCharacter();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (roadSignTransform != null) 
        {
            Vector2 dir = (Vector2)roadSignTransform.position - (Vector2)transform.position;
            if (dir.sqrMagnitude < 0.01f) { return; }   // 已经到达，靠 OnTriggerEnter2D 切下一个
            Move(dir.normalized); 
        }
    }
}
