using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    
    public event Action <float,bool,int> OnBulletHit;
    public event Action<int> Shooted;

    
    public void BulletHit(float damage, bool player, int hited) 
    {
        if(OnBulletHit != null) 
        {
            OnBulletHit(damage,player,hited);
        }
    }
    



}
