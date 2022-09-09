using UnityEngine;
using Zenject;
using UnityEngine.UI;
public class Player : CharacterEntity
{
    //Esse código deriva do CharacterEntity e contem os metodos e atributos comuns, além dos métodos competentes ao jogador


    [Inject] private EventsManager _events;

    // Recebe a barra de vida que fica na UI
    [SerializeField]
    private Slider HealthBarFill;

    // Input system
    private GameController _playerControl;
    private Camera _mainCamera;


    private void Awake()
    {
        // Cria um Inputsystem para controlar o personagem
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
        //Define o tamanho e a quantidade da barra de vida
        HealthBarFill.maxValue = MaxHealth;
        HealthBarFill.value = CurrentHealth;
        Tag = "Player";

        _mainCamera = Camera.main;
        //Events inscritos
        _events.OnBulletHit += TakeDamage;
        _events.OnKill += CureLife;
            //Evento do input system
        _playerControl.Player.Shoot.performed += _ => Shoot();

    }

    private void Update()
    {
        Move();
        TrackAim();
        
    }

    

    public void Move()
    {
        //Ao apertar um dos botões WASD, o player é movido na direção e na velocidade que ele possui
        Vector3 movement = _playerControl.Player.Move.ReadValue<Vector2>() * CharacterVelocity;
        transform.position += movement * Time.deltaTime;
        // Se o vetor de movimento não for 0, ele está se movendo e animação é tocada
        if (movement != Vector3.zero)
        {
            Animator.SetBool("Walking", true);
        }
        else
        {
            // para a animação ao parar de se mover
            Animator.SetBool("Walking", false);
        }
    }

    public void TrackAim()
    {   // Pega a posição do mouse, converte para a posição em tela
        Vector2 mousePosition = _playerControl.Player.Aim.ReadValue<Vector2>();
        Vector3 mouseScreenPosition = new Vector3(mousePosition.x, mousePosition.y, _mainCamera.orthographicSize);
        // Converte em coordenadas no mundo do jogo
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        // Define o alvo como a diferença entre as coordenadas do mouse e posição do player 
        Vector3 targetDirection = mouseWorldPosition - transform.position;
        // Mira na direção do alvo
        Aim(targetDirection);
    }
   
    public override void TakeDamage(float damage, bool player, int hitlayer, GameObject whoshited)
    {
        // Se o player ainda estiver com vida e foi acertado é descontado a vida e a barra atualizada
        if (player && CurrentHealth>=0)
        {
            base.TakeDamage(damage, player, hitlayer, whoshited);
            HealthBarFill.value -= damage;

        }

    }
   

}
