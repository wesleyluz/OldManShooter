using System.Collections;
using UnityEngine;
using Zenject;
public class Enemy : CharacterEntity
{
    [Inject] private EventsManager _events;
    [Inject] private Player _player;

    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private bool _inRange;
   


    void Start()
    {
        base.InicialieStatus();
        Tag = "Enemy";
        _playerTransform = _player.transform;
        _events.OnBulletHit += TakeDamage;
    }

    private void Update()
    {
        TrackPlayer();
        
    }

    public void Revive() 
    {
        _inRange = false;
        CanShoot = true;
        CurrentHealth = MaxHealth;
        

    }
    public override void TakeDamage(float damage, bool player, int hitlayer, GameObject whoshited)
    {
        if(!player)    
            base.TakeDamage(damage, player,hitlayer, whoshited);
    }
    public void TrackPlayer() 
    {
        if (_inRange)
        {
            Vector3 playerPosition = _playerTransform.position;
            Vector3 targetDirection = playerPosition - transform.position;
            Aim(targetDirection);
            Shoot();
            Animator.SetBool("Walking", false);
        }
        else 
        {
            Move();
        }
        
    }

    private void Move() 
    {

        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, base.CharacterVelocity * Time.deltaTime);
        Animator.SetBool("Walking", true);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>()) 
        {
            StartCoroutine(WaitForsearch(1.8f));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>()) 
        {
            StartCoroutine(WaitForsearch(2f));
            
        }
    }

    IEnumerator WaitForsearch(float waitTime) 
    {
        yield return new WaitForSeconds(waitTime);
        _inRange = !_inRange;
    }





}
