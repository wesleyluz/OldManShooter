using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterEntity : MonoBehaviour
{  
    //Esse código é responsável por Atributos e métodos que serão herdados pelo jogador e pelos monstros, aqui ficam os itens comuns de cada um deles
    
    
    
    // Injeção de dependencia
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
    private float _currentHealth;
    private bool _canShoot;
    private int _layerMask;
    private bool _walking;


    public string Tag { get; set; }
    //Acesso público
    public float CharacterVelocity => characterVelocity;
    public bool Dead => _dead;
    public float MaxHealth => maxHealth;
    public float CurrentHealth { set { _currentHealth = value; } get { return _currentHealth; } }
    
    public bool CanShoot {set { _canShoot = value; } }

    public Animator Animator;

    //Inicializa os status comuns
    public virtual void  InicialieStatus()
    {
        _walking = false;
        _dead = false;
        _canShoot = true;
        _currentHealth = maxHealth;
        _layerMask = gameObject.layer;
       
        
       
    }
    // Função de dano
    public virtual void TakeDamage(float damage, bool player,int hitlayer, GameObject whoshited) 
    {
        // Se quem foi acertado estiver em layers separadas e os objetos forem iguais o dano é distribuido
        // Objetos iguais previne que ambos os monstros levem dano quando só um for acertado
        //Layers separadas previnem que os monstros se acertem 
        if (hitlayer != _layerMask && whoshited == this.gameObject)
        {
            _currentHealth -= damage;
            
        }
        if(_currentHealth <= 0)
        {
            //Caso a vida acabe é chamada a função de morte
            Die(this.gameObject);
        }
    }
    public virtual void Aim(Vector3 targetDirection) 
    {
        //O monstro e o player miram de forma comum, alerando o angulo do seu pivot correspondente, o pivo gira no eixo entre o character e o alvo
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    public virtual void Die(GameObject whoIsDead) 
    {
        //Ao morrer, um evento é disparado e avisado quem se subescreveu
        _dead = true;
        _evetsManager.OnDead(_dead,whoIsDead);


    }

    public virtual void CureLife()
    {
        //Mecanica de cura para o player
        if (_currentHealth < maxHealth) { _currentHealth += 10; }
        
    }



    public virtual void Shoot() 
    {
        // Caso o tempo entre um tiro e outro não tenha acabado, o tiro não é executado
        if (!_canShoot) return;
        // Ao atirar é retirada uma munição da piscina
        GameObject bulletShooted = _bulletPool.GetBullet();
        // O transform é alterado para o local de onde sai o tiro dos characters
        bulletShooted.transform.position = shootDirection.position;
        bulletShooted.transform.rotation = shootDirection.rotation;
        // A munição recebe o objeto de quem ta atirando, será passado no evento ao acertar
        bulletShooted.GetComponent<BulletControl>().whosShooting = this.gameObject;
        //A munição se torna ativa na cena e executa sua função
        bulletShooted.SetActive(true);
        //É dado o cooldown entre os tiros
        StartCoroutine(CantShoot());
    }
    IEnumerator CantShoot()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootWaitTime);
        _canShoot = true;
    }


}
