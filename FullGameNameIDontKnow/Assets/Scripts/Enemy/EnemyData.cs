using UnityEngine;
using GD.MinMaxSlider;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public new string name;

    [Header("Parameter")]
    public float health;
    public float damage;
    [MinMaxSlider(10, 10000)]
    public Vector2Int money;
    [MinMaxSlider(10, 1000)]
    public Vector2Int xp;

    [Header("Movement")]
    public float Mass;
    public float Speed;
    public float Acceleration;

    [Header("States")]
    public float attackRange;
    public float followRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
}
