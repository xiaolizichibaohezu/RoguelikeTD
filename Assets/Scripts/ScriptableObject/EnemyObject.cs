using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EnemyObject : ScriptableObject
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _speed;

    public int maxHealth => _health;
    public int maxDamage => _damage;
    public float maxSpeed => _speed;
}
