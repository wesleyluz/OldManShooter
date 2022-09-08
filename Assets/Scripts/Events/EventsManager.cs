using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
   
    public event Action <float,bool,int,GameObject> OnBulletHit;
    public event Action<bool,GameObject> IsDead;

    
    public void BulletHit(float damage, bool player,int HitedLayer, GameObject hited) 
    {
        if(OnBulletHit != null) 
        {
            OnBulletHit(damage,player,HitedLayer,hited);
        }
    }
    
    public void OnDead(bool Die, GameObject whoIsDie) 
    {
        if(IsDead != null) 
        {
            IsDead(Die, whoIsDie);
        }
    }


}
