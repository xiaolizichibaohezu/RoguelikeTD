using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public abstract class Character : MonoBehaviour
{
    private Rigidbody2D _rb2D = null;
    private float maxHealthPoint = 100;
    private float healthPoint;
    private float moveSpeed;
    private float maxMoveSpeed = 2;

    public Rigidbody2D rd2D
    {
        get
        {
            if(_rb2D == null) { _rb2D = GetComponent<Rigidbody2D>(); }
            return _rb2D;
        }
    }

    public virtual void HealthRecovery(float point)
    {
        if(healthPoint + point >= maxHealthPoint) { healthPoint = maxHealthPoint; }
        else { healthPoint += point; }
    }

    public virtual void TakeDamage(float point)
    {
        if(healthPoint - point <= 0)
        {
            healthPoint = 0;
            Die();
        }
        else { healthPoint -= point; }
    }

    public virtual void SpeedAdjust(float precent)
    {
        moveSpeed *= precent;
    }

    public virtual void SpeedRecovery()
    {
        moveSpeed = maxMoveSpeed;
    }

    protected abstract void Die();

    public float GetHealth()
    {
        return healthPoint;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    private void ResetCharacter()
    {
        healthPoint = maxHealthPoint;
        moveSpeed = maxMoveSpeed;
    }

    public virtual void Move(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            Rigidbody2D currentRb2D = rd2D;
            if(currentRb2D != null){ _rb2D.MovePosition(_rb2D.position + direction * moveSpeed * Time.fixedDeltaTime); }
        }
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
    void Update()
    {
        
    }
}
