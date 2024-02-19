using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeToReloadScene;

    [Space, SerializeField] private UnityEvent onStartGame;
    [SerializeField] public UnityEvent onGameOver, onIncreaseScore;

    public int score
    {
        get;
        private set;
    }

    public bool isGameOver
    {
        get;
        private set;
    }

    public string username;

    // Singleton!
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            username = PlayerPrefs.GetString("username", "usuario");
        }   
        else
            DestroyImmediate(this.gameObject);
    }

    public void StartGame()
    {
        Debug.Log("GameManager :: StartGame()");

        onStartGame?.Invoke();
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        Debug.Log("GameManager :: GameOver()");

        isGameOver = true;

        onGameOver?.Invoke();

        StartCoroutine(ReloadScene());
    }

    public void IncreaseScore()
    {
        Debug.Log("GameManager :: IncreaseScore()");

        score++;

        onIncreaseScore?.Invoke();
    }

    public void UpdateUsername(string username)
    {
        if (username.Length < 1) username = "usuario";

        PlayerPrefs.SetString("username", username);
        this.username = username;
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(timeToReloadScene);

        Debug.Log("GameManager :: ReloadScene()");

        SceneManager.LoadScene(0);
    }
}
