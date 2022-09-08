using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class BulletsPooling : MonoBehaviour
{

    [Inject] private IInstantiator m_instantiator;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private int amout;
    [SerializeField]
    private bool expandable;

    private List<GameObject> _inPool;
    private List<GameObject> _outPool;

    private void Awake()
    {
        _inPool  = new List<GameObject>();
        _outPool = new List<GameObject>();

        for(int i =0; i< amout; i++) 
        {
            BulletsFabric();
        }
    }
    private void BulletsFabric() 
    {
        GameObject shell = m_instantiator.InstantiatePrefab(bullet);
        shell.transform.parent = transform;
        shell.SetActive(false);
        _inPool.Add(shell);
    }
    public GameObject GetBullet() 
    {
        int totalOnPool = _inPool.Count;
        if(totalOnPool == 0 && !expandable) 
        {
            return null;
        }
        else if(totalOnPool == 0)
        {
            BulletsFabric();
        }
        GameObject pipeBullet = _inPool[totalOnPool - 1];
        _inPool.RemoveAt(totalOnPool - 1);
        _outPool.Add(pipeBullet);

        return pipeBullet;
    }

    public void ReturnBullet(GameObject bullet) 
    {
        Debug.Assert(_outPool.Contains(bullet));
        bullet.SetActive(false);
        _outPool.Remove(bullet);
        _inPool.Add(bullet);
    }



   




}
