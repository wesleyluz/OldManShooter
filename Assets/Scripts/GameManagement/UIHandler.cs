using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using TMPro;
public class UIHandler : MonoBehaviour
{
    [Inject] private EventsManager _eventsManager;
    
    
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

    private int tutorialController;
    private void Awake()
    {
        Time.timeScale = 0f;
    }
    private void Start()
    {   
        _eventsManager.OnWinGame   += ShowVictoryScreen;
        _eventsManager.OnGameOver += ShowGameOverScreen;
        _eventsManager.OnScoreUpdate += WaveUpdate;
        TutorialObject.SetActive(true);
        WaveScore.text = "WAVE: 0" ;
        tutorialController = 0;
        
        Tutorial();
        
    }

    public void Tutorial() 
    {
       
        tutorialText.text = tutorialTexts.Texts[tutorialController];
        tutorialController++;
        if(tutorialController >= tutorialTexts.Texts.Count) 
        {
            nextButton.SetActive(false);
        }
       
    }

    public void StartGame() 
    {
        TutorialObject.SetActive(false);
        TurnOffAll();
        Time.timeScale = 1f;
    }
    public void WaveUpdate(int number) 
    {
        WaveScore.text = "WAVE: " + number;
    }
    public void ShowVictoryScreen() 
    {
        MenuObjects.SetActive(true);
        WinObjects.SetActive(true);
    }

    public void ShowGameOverScreen() 
    {
        MenuObjects.SetActive(true);
        GameOverObjects.SetActive(true);
    }

    public void TurnOffAll() 
    {
        GameOverObjects.SetActive(false);
        MenuObjects.SetActive(false);
        WinObjects.SetActive(false);
    }

    public void RestartScene() 
    {
        SceneManager.LoadScene("Oldman");
    }
}
