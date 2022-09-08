using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class Player : CharacterEntity
{
    [Inject] private EventsManager _events;
     
    private GameController _playerControl;
    private Camera _mainCamera;


    private void Awake()
    {
        _playerControl = new GameController();
    }
    private void OnEnable()
    {
        _playerControl.Enable(); 
        
    }

    private void OnDisable()
    {
        _playerControl.Disable();
    }

    void Start() 
    {

        base.InicialieStatus();
        Tag = "Player";
        _mainCamera = Camera.main;
        //Events subscribe
        _events.OnBulletHit += TakeDamage;
            //Input Events
        _playerControl.Player.Shoot.performed += _ => Shoot();

    }

    private void Update()
    {
        Move();
        TrackAim();
        
    }

    

    public void Move()
    {
        Vector3 movement = _playerControl.Player.Move.ReadValue<Vector2>() * base.CharacterVelocity;
        transform.position += movement * Time.deltaTime;
    }

    public void TrackAim()
    {
        Vector2 mousePosition = _playerControl.Player.Aim.ReadValue<Vector2>();
        Vector3 mouseScreenPosition = new Vector3(mousePosition.x, mousePosition.y, _mainCamera.orthographicSize);
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position;
        Aim(targetDirection);
    }
   
    public override void TakeDamage(float damage, bool player, int hitlayer, GameObject whoshited)
    {   
        if(player)
            base.TakeDamage(damage, player,hitlayer,whoshited);
    }
   

}
