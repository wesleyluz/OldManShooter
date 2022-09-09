using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class GameMaster : MonoBehaviour
{
    [Inject] private IInstantiator m_instantiator;
    [Inject] private EventsManager _eventsManager;
    

    [SerializeField]
    private List<GameObject> monsterList;
    
    [SerializeField]
    private List<Transform> spawners;

    private int _deathcount;
    private int _wincount;

    private void Awake()
    {
        _wincount = 0;
        _deathcount = 0;
        for (int i = 0; i < spawners.Count; i++)
        {
            SpawnerFirstMonster(i);
        }
    }
    private void Start()
    {
        _eventsManager.IsDead += DeadandRestart;
    }
    private void SpawnerFirstMonster(int place)
    {
        
        GameObject _monster = m_instantiator.InstantiatePrefab(monsterList[place]);
        _monster.transform.position = spawners[place].position;
        _monster.SetActive(true);
        
    }

    private void DeadandRestart(bool isdead,GameObject whosDead) 
    {
        switch (whosDead.tag)
        {
            case "Player":
                _eventsManager.GameOver();
                break;
            case "Enemy":
                MonsterRespawner(whosDead);
                break;

        }
    }

    private void MonsterRespawner(GameObject monster) 
    {
        if(_deathcount >= 2) 
        {
            _deathcount = 0;
            _wincount++;
            _eventsManager.UpdateWave(_wincount);
        }
        else if(_deathcount < 2 && _wincount < 3) 
        {
            
            monster.transform.position = spawners[_deathcount].position;
            monster.GetComponent<Enemy>().Revive();
            _eventsManager.KillMonster();
            _deathcount++;
            StartCoroutine(WaitToRespawn(monster));

        }
        else if(_wincount >=3)
        {

            // Vence o jogo, mostrar tela de vitória
            StartCoroutine(TurnOffandWin());
            
        }
        monster.SetActive(false);
        
        
        
    }


    IEnumerator WaitToRespawn(GameObject monsterToRespawn) 
    {
        yield return new WaitForSeconds(3f);
        monsterToRespawn.SetActive(true);


    }
    IEnumerator TurnOffandWin()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            monsterList[i].SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        _eventsManager.WinGame();
    }
    
}
