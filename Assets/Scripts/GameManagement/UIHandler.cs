using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using TMPro;
public class UIHandler : MonoBehaviour
{

    //Esse c�digo � respons�vel por controlar toda a UI do game, al�m de resetar o jogo

    [Inject] private EventsManager _eventsManager;
    
    // Objetos de UI que ser�o inseridos no inspector
    [SerializeField]
    private GameObject GameOverObjects;
    [SerializeField]
    private GameObject MenuObjects;
    [SerializeField]
    private GameObject WinObjects;
    [SerializeField]
    private TextMeshProUGUI WaveScore;

    [SerializeField]
    private GameObject TutorialObject;
    
    [SerializeField]
    private TextMeshProUGUI tutorialText;
    
    [SerializeField]
    private TutorialText tutorialTexts;
    
    [SerializeField]
    private GameObject nextButton;

    // controle dos textos do tutorial
    private int tutorialController;
    private void Awake()
    {
        //Pausa o game
        Time.timeScale = 0f;
    }
    private void Start()
    {   
        // Subescreve nos eventos, assim que eles s�o disparados � executada a fun��o correspondente
        _eventsManager.OnWinGame   += ShowVictoryScreen;
        _eventsManager.OnGameOver += ShowGameOverScreen;
        _eventsManager.OnScoreUpdate += WaveUpdate;
        
        //Ativa o tutorial
        TutorialObject.SetActive(true);
        
        // Seta o controle para 0
        tutorialController = 0;
        
        Tutorial();
        
    }

    public void Tutorial() 
    {
        // Carrega cada texto do scriptable object e mostra em tela sempre que o bot�o next � apertado
        tutorialText.text = tutorialTexts.Texts[tutorialController];
        tutorialController++;
        // Verifica se ainda tem textos do tutorial
        if(tutorialController >= tutorialTexts.Texts.Count) 
        {
            nextButton.SetActive(false);
        }
       
    }

    public void StartGame() 
    {
        //Ao apertar o bot�o start o tutorial � desligado e inicia o jogo
        TutorialObject.SetActive(false);
        //Garante que os outros elementos estejam desligados
        TurnOffAll();
        // Resume o jogo
        Time.timeScale = 1f;
    }
    public void WaveUpdate(int number) 
    {
        //Sempre que acaba uma onda de monstros, o
        WaveScore.text = "WAVE: " + number;
    }
    public void ShowVictoryScreen() 
    {
        //Se acabar todas as ondas, liga o panel com todos os objetos e mostra a tela de vit�ria com a op��o de jogar de novo
        MenuObjects.SetActive(true);
        WinObjects.SetActive(true);
    }

    public void ShowGameOverScreen() 
    {
        // Se o jogador morrer, liga o panel com todos os objetos e mostra a tela de derrota com a op��o de jogar de novo
        MenuObjects.SetActive(true);
        GameOverObjects.SetActive(true);
    }

    public void TurnOffAll() 
    {
        //Desliga todos os objetos de UI menos o tutorial
        GameOverObjects.SetActive(false);
        MenuObjects.SetActive(false);
        WinObjects.SetActive(false);
    }

    public void RestartScene() 
    {
        //Reseta o game
        SceneManager.LoadScene("Oldman");
    }
}
