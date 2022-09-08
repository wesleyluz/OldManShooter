using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameMaster : MonoBehaviour
{
    [Inject] private IInstantiator m_instantiator;
    [Inject] private EventsManager _eventsManager;
    

    [SerializeField]
    private GameObject monster;
    
    [SerializeField]
    private List<Transform> spawners;

    private int _deathcount;
   

    private void Awake()
    {
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
        Debug.Log(place);
        GameObject _monster = m_instantiator.InstantiatePrefab(monster);
        _monster.transform.position = spawners[place].position;
        _monster.SetActive(true);
        
    }

    private void DeadandRestart(bool isdead,GameObject whosDead) 
    {
        switch (whosDead.tag)
        {
            case "Player":
                Debug.Log("Morreu");
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
        }
        else if(_deathcount < 2) 
        {
            monster.SetActive(false);
            monster.transform.position = spawners[_deathcount].position;
            monster.GetComponent<Enemy>().Revive();
            _deathcount++;

        }
        StartCoroutine(WaitToRespawn(monster));
        
    }


    IEnumerator WaitToRespawn(GameObject monsterToRespawn) 
    {
        yield return new WaitForSeconds(3f);
        monsterToRespawn.SetActive(true);


    }

    
}
