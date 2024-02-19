using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    // Singleton!
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);
    }

    public void UpdateScoreText()
    {
        Debug.Log("UIManager :: UpdateScoreText()");

        score.text = GameManager.Instance.score.ToString();
    }
}
