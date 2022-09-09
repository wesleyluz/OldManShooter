using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterEntity : MonoBehaviour
{  
    //Esse c�digo � respons�vel por Atributos e m�todos que ser�o herdados pelo jogador e pelos monstros, aqui ficam os itens comuns de cada um deles
    
    
    
    // Inje��o de dependencia
    [Inject] private BulletsPooling _bulletPool;
    [Inject] private EventsManager _evetsManager;

    //  vari�veis privadas no inspector

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

   
    // Vari�veis privadas
    private bool _dead;
    private float _currentHealth;
    private bool _canShoot;
    private int _layerMask;
    private bool _walking;


    public string Tag { get; set; }
    //Acesso p�blico
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
    // Fun��o de dano
    public virtual void TakeDamage(float damage, bool player,int hitlayer, GameObject whoshited) 
    {
        // Se quem foi acertado estiver em layers separadas e os objetos forem iguais o dano � distribuido
        // Objetos iguais previne que ambos os monstros levem dano quando s� um for acertado
        //Layers separadas previnem que os monstros se acertem 
        if (hitlayer != _layerMask && whoshited == this.gameObject)
        {
            _currentHealth -= damage;
            
        }
        if(_currentHealth <= 0)
        {
            //Caso a vida acabe � chamada a fun��o de morte
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
        //Ao morrer, um evento � disparado e avisado quem se subescreveu
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
        // Caso o tempo entre um tiro e outro n�o tenha acabado, o tiro n�o � executado
        if (!_canShoot) return;
        // Ao atirar � retirada uma muni��o da piscina
        GameObject bulletShooted = _bulletPool.GetBullet();
        // O transform � alterado para o local de onde sai o tiro dos characters
        bulletShooted.transform.position = shootDirection.position;
        bulletShooted.transform.rotation = shootDirection.rotation;
        // A muni��o recebe o objeto de quem ta atirando, ser� passado no evento ao acertar
        bulletShooted.GetComponent<BulletControl>().whosShooting = this.gameObject;
        //A muni��o se torna ativa na cena e executa sua fun��o
        bulletShooted.SetActive(true);
        //� dado o cooldown entre os tiros
        StartCoroutine(CantShoot());
    }
    IEnumerator CantShoot()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootWaitTime);
        _canShoot = true;
    }


}
