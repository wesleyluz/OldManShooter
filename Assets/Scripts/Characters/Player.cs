using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
public class Player : CharacterEntity
{

    [Inject] BulletsPooling _bulletPool;
    [SerializeField]
    private Transform shootDirection;
    
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
        _mainCamera = Camera.main;
        //Events subscribe
            //Input Events
        _playerControl.Player.Shoot.performed += _ => Shoot();


    }

    public override void Shoot() 
    {
        base.Shoot();
        Vector2 mousePosition = _playerControl.Player.Aim.ReadValue<Vector2>();
        mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        GameObject bulletShooted = _bulletPool.GetBullet();
        bulletShooted.transform.position = shootDirection.position;
        bulletShooted.transform.rotation = shootDirection.rotation;
        bulletShooted.SetActive(true);
        

    }

    public override void Die()
    {
        
    }

}
