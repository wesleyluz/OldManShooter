using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterEntity : MonoBehaviour
{   // Injeção de dependencia
    [Inject] private BulletsPooling _bulletPool;
    [Inject] private EventsManager _evetsManager;

    //  variáveis privadas no inspector

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float shootWaitTime;

    [SerializeField]
    private float characterVelocity;

    [SerializeField]
    private Transform shootDirection;

    [SerializeField]
    private Transform pivot;

    // Variáveis privadas
    private bool _dead;
    [SerializeField]
    private float _currentHealth;
    private bool _canShoot;
    private int _layerMask;

    public string Tag { get; set; }
    //Acesso público
    public float CharacterVelocity => characterVelocity;
    public bool Dead => _dead;
    public float MaxHealth => maxHealth;
    public float CurrentHealth { set { _currentHealth = value; } }
    
    public bool CanShoot {set { _canShoot = value; } }

    public virtual void  InicialieStatus()
    {
        _dead = false;
        _canShoot = true;
        _currentHealth = maxHealth;
        _layerMask = gameObject.layer;
       
    }
    public virtual void TakeDamage(float damage, bool player,int hitlayer, GameObject whoshited) 
    {

        if (hitlayer != _layerMask && whoshited == this.gameObject)
        {
            _currentHealth -= damage;
        }
        if(_currentHealth <= 0)
        {
            Die(this.gameObject);
        }
    }
    public virtual void Aim(Vector3 targetDirection) 
    {
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    public virtual void Die(GameObject whoIsDead) 
    {
        _dead = true;
        _evetsManager.OnDead(_dead,whoIsDead);


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
        bulletShooted.GetComponent<BulletControl>().whosShooting = this.gameObject;
        bulletShooted.SetActive(true);
        StartCoroutine(CantShoot());
    }
    IEnumerator CantShoot()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootWaitTime);
        _canShoot = true;
    }


}
