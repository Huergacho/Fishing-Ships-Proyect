using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor",menuName ="BaseActor",order = 1)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private float maxHealth;
    public float MaxHealth => maxHealth;
    [SerializeField] private float maxSpeed;
    public float MaxSpeed => maxSpeed;
    [SerializeField] private float rotSpeed;
    public float RotSpeed => rotSpeed;
}
