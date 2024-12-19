using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;
    [SerializeField] Canvas gameOverUi;
    [SerializeField] Canvas WaveUI;
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public GameState currentState;
    void Awake()
    {
        // Implementazione del pattern Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantieni questo oggetto quando si cambia scena
        }
        else
        {
            Destroy(gameObject); // Distruggi l'istanza duplicata
        }
    }
    void Start()
    {
        // Imposta lo stato iniziale del gioco
        ChangeState(GameState.MainMenu);
        
    }
    private void Update()
    {
        gameOverUi = GameObject.FindGameObjectWithTag("GameOverUI").GetComponent<Canvas>();
    }
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        // Aggiungi qui la logica per gestire il cambiamento di stato
        switch (currentState)
        {
            case GameState.MainMenu:
                // Mostra il menu principale
                ShowMainMenu();
                break;
            case GameState.Playing:
                // Avvia il gioco

                StartGame();
                break;
            case GameState.Paused:
                // Metti in pausa il gioco
                PauseGame();
                break;
            case GameState.GameOver:
                // Mostra la schermata di game over
                ShowGameOver();
                break;
        }
    }

    void ShowMainMenu()
    {
        // Logica per mostrare il menu principale
        // Ad esempio, attivare il canvas del menu principale
        Debug.Log("Mostra il menu principale");
    }

    void StartGame()
    {
        // Logica per avviare il gioco
        // Ad esempio, caricare la scena di gioco
        Debug.Log("Inizia il gioco");
        
        gameOverUi.enabled = false;
        SceneManager.LoadScene("GameScene"); // Assicurati di avere una scena chiamata "GameScene"
    }

    void PauseGame()
    {
        // Logica per mettere in pausa il gioco
        Debug.Log("Gioco in pausa");
    }

    void ShowGameOver()
    {
        // Logica per mostrare la schermata di game over
        gameOverUi.enabled = true;
        Debug.Log("Game Over");
    }

    public void OnPlayButtonPressed()
    {
        ChangeState(GameState.Playing);
    }
    
}
