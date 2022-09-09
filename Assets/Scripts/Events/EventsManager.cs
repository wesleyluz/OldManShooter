using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
   
    public event Action <float,bool,int,GameObject> OnBulletHit;
    public event Action<bool,GameObject> IsDead; 
    public event Action <int> OnScoreUpdate;
    public event Action OnKill;
    public event Action OnGameOver;
    public event Action OnWinGame;
   
    
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

    public void KillMonster() 
    {
        if (OnKill != null) 
        {
            OnKill();
        }

    }

    public void GameOver() 
    {
        if(OnGameOver != null) 
        {
            OnGameOver();
        }
    }

    public void WinGame() 
    {
        if(OnWinGame != null) 
        {
            OnWinGame();
        }
    }

    public void UpdateWave(int score)
    {
        if(OnScoreUpdate != null) 
        {
            OnScoreUpdate(score);
        }
    }

}
