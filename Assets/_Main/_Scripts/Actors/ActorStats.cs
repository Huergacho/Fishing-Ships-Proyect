using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor",menuName ="BaseActor",order = 1)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxSpeed;
}
