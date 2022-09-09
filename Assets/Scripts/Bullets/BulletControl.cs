using System.Collections;
using UnityEngine;
using Zenject;

public class BulletControl : MonoBehaviour
{
    // Usando injeção de dependencia para receber as referencias do bullet pool e do event manager que serão utilizados ao longo do código
    [Inject] private BulletsPooling _bulletPooling;
    [Inject] private EventsManager _eventsManager;

    // Criando as váriáveis de controle do munição, velocidade que ela vai, tempo para retornar a piscina e o dano que ela causa

    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToreturn;
    
    private float damage = 20f;

    public GameObject whosShooting;
    private void OnEnable()
    {
        StartCoroutine(ReturnToPool());
    }

    void Update()
    {
        //move o tiro para frente
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    IEnumerator ReturnToPool() 
    {
        // Aguarda o tempo definido no Inspector para retornar a munição para a piscina
        yield return new WaitForSecondsRealtime(timeToreturn);
        
        _bulletPooling.ReturnBullet(gameObject);

    }

    private void OnTriggerEnter(Collider collision)
    {   
        //Salva a tag da colisão
        string tag = collision.tag;
        //Ignora o range dos monstros e também evita que os mostros se acertem
        if(tag != "Range" && whosShooting.layer != collision.gameObject.layer)
            _bulletPooling.ReturnBullet(gameObject);
        switch (tag) 
        {
            //Ao verificar quem foi acertado dispara o evento de hit
            // O evento ta sobreescrito pelos monstros e o jogador
            case "Player" :
                _eventsManager.BulletHit(damage, true, whosShooting.layer, collision.gameObject);
                return;
            case "Enemy":
                _eventsManager.BulletHit(damage, false,whosShooting.layer, collision.gameObject);
                return;
        }
        
    }
}
