using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    
    public event Action <float> OnBulletHit;
    
    public void BulletHit(float damage) 
    {
        if(OnBulletHit != null) 
        {
            OnBulletHit(damage);
        }
    }



}
