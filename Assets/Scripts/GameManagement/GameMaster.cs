using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class GameMaster : MonoBehaviour
{
    // Esse código é responsável pela manutenção do coreloop do game

    [Inject] private IInstantiator m_instantiator;
    [Inject] private EventsManager _eventsManager;
    

    [SerializeField]
    private List<GameObject> monsterList;
    
    [SerializeField]
    private List<Transform> spawners;

    private int _deathcount;
    private int _wincount;

    private void Awake()
    {   // Seta a contagem para vitória para 0 e o número de mortos para 0
        _wincount = 0;
        _deathcount = 0;
        //Spawna os dois primeiros monstros
        for (int i = 0; i < spawners.Count; i++)
        {
            SpawnerFirstMonster(i);
        }
    }
    private void Start()
    {
        // Subescreve no evento de morte
        _eventsManager.IsDead += DeadandRestart;
    }
    private void SpawnerFirstMonster(int place)
    {
        // Coloca os dois tipos de monstro em cena instanciados em seus repectivos Respawners   
        GameObject _monster = m_instantiator.InstantiatePrefab(monsterList[place]);
        _monster.transform.position = spawners[place].position;
        //Liga o monstro
        _monster.SetActive(true);
        
    }

    private void DeadandRestart(bool isdead,GameObject whosDead) 
    {
        //Verifica quem morreu
        switch (whosDead.tag)
        {
            case "Player":
                // Quando o player morre é chamado o evento de game over
                _eventsManager.GameOver();
                break;
            case "Enemy":
                //Quando o monstro morre, é chamado o evento de controle e respawn
                MonsterRespawner(whosDead);
                break;

        }
    }

    private void MonsterRespawner(GameObject monster) 
    {
        //Se a quantidade de monstros mortos for maior ou igual a 2
        if(_deathcount >= 2) 
        {
            // Reseta a contagem
            _deathcount = 0;
            //Passa uma onda
            _wincount++;
            //Atualiza o numero de ondas na UI
            _eventsManager.UpdateWave(_wincount);
        }
        //Se ainda não for maior que dois ou as ondas não tiverem acabadas
        else if(_deathcount < 2 && _wincount < 3) 
        {
            // retorna os monstros pro ponto de respawner
            monster.transform.position = spawners[_deathcount].position;
            //Reseta os status
            monster.GetComponent<Enemy>().Revive();
            //Chama o evento de monstros mortos
            _eventsManager.KillMonster();
            //Aumenta o numero de mortes
            _deathcount++;
            // Inicia o processo de respawn
            StartCoroutine(WaitToRespawn(monster));

        }
        else if(_wincount >=3)
        {

            // Vence o jogo, mostrar tela de vitória
            StartCoroutine(TurnOffandWin());
            
        }
        //Desliga o monstro ao matar
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
