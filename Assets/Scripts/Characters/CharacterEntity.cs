using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterEntity : MonoBehaviour
{   // Injeção de dependencia
    [Inject] private BulletsPooling _bulletPool;
    [Inject] private EventsManager _eventsManager;

    //  variáveis privadas no inspector
    
    [SerializeField]
    private float maxHealth;
    
    [SerializeField]
    private float shootWaitTime;
    
    [SerializeField]
    private float characterVelocity ;

    [SerializeField]
    private Transform shootDirection;
  
  
    // Variáveis privadas
    private bool _alive;
    [SerializeField]
    private float _currentHealth;
    private bool _canShoot;
    private int _layerMask;

    //Acesso público
    public float CharacterVelocity => characterVelocity;
    

    public virtual void  InicialieStatus()
    {
        _alive = true;
        _canShoot = true;
        _currentHealth = maxHealth;
        _layerMask = gameObject.layer;
       
    }
    public virtual void TakeDamage(float damage, bool player,int hitlayer) 
    {
        Debug.Log(hitlayer +" "+ _layerMask);
        if (hitlayer != _layerMask)
        {
            _currentHealth -= damage;
        }
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die() 
    {
        _alive = false;
    }

    //public virtual void CureLife() 
    //{

    //}

   

    public virtual void Shoot() 
    {

        if (!_canShoot) return;
        GameObject bulletShooted = _bulletPool.GetBullet();
        bulletShooted.transform.position = shootDirection.position;
        bulletShooted.transform.rotation = shootDirection.rotation;
        bulletShooted.GetComponent<BulletControl>().whosShooting = _layerMask;
        bulletShooted.SetActive(true);
        StartCoroutine(CanShoot());
    }
    IEnumerator CanShoot()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootWaitTime);
        _canShoot = true;
    }


}
