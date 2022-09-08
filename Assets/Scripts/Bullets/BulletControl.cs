using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletControl : MonoBehaviour
{
    [Inject] private BulletsPooling _bulletPooling;
    [Inject] private EventsManager _eventsManager;

    [SerializeField]
    private float speed;

    private float damage = 20f;

    public GameObject whosShooting;
    private void OnEnable()
    {
        StartCoroutine(ReturnToPool());
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    IEnumerator ReturnToPool() 
    {
        yield return new WaitForSecondsRealtime(5f);
        //Debug.Log(gameObject);
        _bulletPooling.ReturnBullet(gameObject);

    }

    private void OnTriggerEnter(Collider collision)
    {
        string tag = collision.tag;
        if(tag != "Range" && whosShooting.layer != collision.gameObject.layer)
            _bulletPooling.ReturnBullet(gameObject);
        switch (tag) 
        {
            case "Player" :
                _eventsManager.BulletHit(damage, true, whosShooting.layer, collision.gameObject);
                return;
            case "Enemy":
                _eventsManager.BulletHit(damage, false,whosShooting.layer, collision.gameObject);
                return;
        }
        
    }
}
