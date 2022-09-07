using System.Collections.Generic;
using UnityEngine;
public class BulletsPooling : MonoBehaviour
{


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
        bullet.SetActive(false);
        _outPool.Remove(bullet);
        _inPool.Add(bullet);
    }



    private void BulletsFabric() 
    {
        GameObject shell = Instantiate(bullet);
        shell.transform.parent = transform;
        shell.SetActive(false);
        _inPool.Add(shell);
    }




}
