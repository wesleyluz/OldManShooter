using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class BulletsPooling : MonoBehaviour
{
    // Esse código é responsável pelo controle da piscina de munições
    [Inject] private IInstantiator m_instantiator;

    // Recebe o prefab a ser instanciado
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    // quantidade de munição na piscina
    private int amout;
    // Se precisa expandir a quantidade
    [SerializeField]
    private bool expandable;
    // Munições na piscina
    private List<GameObject> _inPool;
    // Munições usadas
    private List<GameObject> _outPool;

    private void Awake()
    {   
        //Inicializa as listas
        _inPool  = new List<GameObject>();
        _outPool = new List<GameObject>();
        // coloca a quantidade setada de munições na piscina
        for(int i =0; i< amout; i++) 
        {
            BulletsFabric();
        }
    }
    private void BulletsFabric() 
    {
        //Instancia o objeto utilizando o instanciador do zenject para previnir erros de instancia
        GameObject shell = m_instantiator.InstantiatePrefab(bullet);
        // coloca as munições como filhos da piscina
        shell.transform.parent = transform;
        // Desliga a munição
        shell.SetActive(false);
        // Adiciona a lista de controle
        _inPool.Add(shell);
    }
    public GameObject GetBullet() 
    {
        //Verifica se a quantidade de munições na piscina é suficiente
        int totalOnPool = _inPool.Count;
        // Se não for expandível retorna null
        if(totalOnPool == 0 && !expandable) 
        {
            return null;
        }
        // se for expandível faz mais munições
        else if(totalOnPool == 0)
        {
            BulletsFabric();
        }
        // retira a ultima munição da piscina para ser usada
        GameObject pipeBullet = _inPool[totalOnPool - 1];
        _inPool.RemoveAt(totalOnPool - 1);
        // adiciona ela na lista de usados 
        _outPool.Add(pipeBullet);
        // retorna a munição para o solicitante
        return pipeBullet;
    }

    public void ReturnBullet(GameObject bullet) 
    {
        // Retorna a munição para a piscina depois de usar
        Debug.Assert(_outPool.Contains(bullet));
        bullet.SetActive(false);
        _outPool.Remove(bullet);
        _inPool.Add(bullet);
    }



   




}
