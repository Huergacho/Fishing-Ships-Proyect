using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : MonoBehaviour
{
    [SerializeField] protected ActorStats actorStats;
    protected float speed;
    [SerializeField] protected float health;

    protected virtual void Start()
    {
        speed = actorStats.MaxSpeed;
        health = actorStats.MaxHealth;
    }
    protected virtual void TakeDamage(float damage)
    {
        health -= damage;
    }
    protected virtual void RecoverHealth(float recovery)
    {
        health += recovery * Time.deltaTime;
    }




}
