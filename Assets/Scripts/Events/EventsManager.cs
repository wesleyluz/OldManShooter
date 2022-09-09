using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    // Esse código é responsável por armazenar todas as funções de Eventos, todos os eventos existentes no jogo são construidos aqui
   
    // Define o acerto da munição, são passados o dano, quem acertou, a layer acertada e o objeto acertado
    public event Action <float,bool,int,GameObject> OnBulletHit;
    // Define se algum character morreu e qual morreu 
    public event Action<bool,GameObject> IsDead; 
    // Atualiza o número de Waves
    public event Action <int> OnScoreUpdate;
    // Ao matar um inimigo
    public event Action OnKill;
    // Ao perder o jogo 
    public event Action OnGameOver;
    // Ao vencer o jogo
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
