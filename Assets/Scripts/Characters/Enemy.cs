using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class Enemy : CharacterEntity
{
    [Inject] private EventsManager _events;
    void Start()
    {
        base.InicialieStatus();
        
        _events.OnBulletHit += TakeDamage;
    }

    public override void TakeDamage(float damage, bool player, int hitlayer)
    {
        if(!player)    
            base.TakeDamage(damage, player,hitlayer);
    }



}
