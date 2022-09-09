using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class BulletsPooling : MonoBehaviour
{
    // Esse c�digo � respons�vel pelo controle da piscina de muni��es
    [Inject] private IInstantiator m_instantiator;

    // Recebe o prefab a ser instanciado
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    // quantidade de muni��o na piscina
    private int amout;
    // Se precisa expandir a quantidade
    [SerializeField]
    private bool expandable;
    // Muni��es na piscina
    private List<GameObject> _inPool;
    // Muni��es usadas
    private List<GameObject> _outPool;

    private void Awake()
    {   
        //Inicializa as listas
        _inPool  = new List<GameObject>();
        _outPool = new List<GameObject>();
        // coloca a quantidade setada de muni��es na piscina
        for(int i =0; i< amout; i++) 
        {
            BulletsFabric();
        }
    }
    private void BulletsFabric() 
    {
        //Instancia o objeto utilizando o instanciador do zenject para previnir erros de instancia
        GameObject shell = m_instantiator.InstantiatePrefab(bullet);
        // coloca as muni��es como filhos da piscina
        shell.transform.parent = transform;
        // Desliga a muni��o
        shell.SetActive(false);
        // Adiciona a lista de controle
        _inPool.Add(shell);
    }
    public GameObject GetBullet() 
    {
        //Verifica se a quantidade de muni��es na piscina � suficiente
        int totalOnPool = _inPool.Count;
        // Se n�o for expand�vel retorna null
        if(totalOnPool == 0 && !expandable) 
        {
            return null;
        }
        // se for expand�vel faz mais muni��es
        else if(totalOnPool == 0)
        {
            BulletsFabric();
        }
        // retira a ultima muni��o da piscina para ser usada
        GameObject pipeBullet = _inPool[totalOnPool - 1];
        _inPool.RemoveAt(totalOnPool - 1);
        // adiciona ela na lista de usados 
        _outPool.Add(pipeBullet);
        // retorna a muni��o para o solicitante
        return pipeBullet;
    }

    public void ReturnBullet(GameObject bullet) 
    {
        // Retorna a muni��o para a piscina depois de usar
        Debug.Assert(_outPool.Contains(bullet));
        bullet.SetActive(false);
        _outPool.Remove(bullet);
        _inPool.Add(bullet);
    }



   




}
