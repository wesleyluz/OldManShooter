using System.Collections;
using UnityEngine;
using Zenject;
public class Enemy : CharacterEntity
{
    // Esse código é um filho do CharacterEntity herdando todos os atributos e mudando apenas nas competências necessárias
    
    
    [Inject] private EventsManager _events;
    [Inject] private Player _player;

    // Referencia do player
    [SerializeField]
    private Transform _playerTransform;
    // verifica se o player está no alcance
    [SerializeField]
    private bool _inRange;
   


    void Start()
    {   // Inicializa os status comuns, vida, dano, layer e etc
        base.InicialieStatus();
        Tag = "Enemy";
        // recebe a referencia do player
        _playerTransform = _player.transform;
        // subescreve no evento de ser acertado
        _events.OnBulletHit += TakeDamage;
    }

    private void Update()
    {
        // Comportamento da IA
        TrackPlayer();
        
    }

    public void Revive() 
    {
        //Caso o monstro possa ser revivido, são resetados alguns status.
        _inRange = false;
        CanShoot = true;
        CurrentHealth = MaxHealth;
        

    }
    public override void TakeDamage(float damage, bool player, int hitlayer, GameObject whoshited)
    {
        //Ao evento ser chamado, verifica se quem foi acertado foi o monstro ou o player, no caso do monstro ele leva dano.
        if(!player)    
            base.TakeDamage(damage, player,hitlayer, whoshited);
    }
    public void TrackPlayer() 
    {
        //A Ia se comporta na perseguição e ataque
        //Se o player estiver no alcance do monstro, ele irá mirar e disparar contra o player
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
            //Caso o player não esteja no alcance do monstro, ele irá perseguir o player
            Move();
        }
        
    }

    private void Move() 
    {

        // Movimenta o monstro até o player entrar no alcance
        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, base.CharacterVelocity * Time.deltaTime);
        //Liga a animação de caminhar
        Animator.SetBool("Walking", true);

    }

    // Se o player sai ou entra no collider do alcance do monstro, a váriavel de alcance muda de acordo com o estado após um tempo
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

    // Espera um tempo para mudar o status o tempo que a IA leva para "perceber" se pode ou não atacar o player 
    IEnumerator WaitForsearch(float waitTime) 
    {
        yield return new WaitForSeconds(waitTime);
        _inRange = !_inRange;
    }





}
