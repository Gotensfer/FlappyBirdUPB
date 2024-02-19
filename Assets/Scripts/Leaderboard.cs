using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject userEntryPrefab;
    [SerializeField] public RectTransform scrollviewContent;

    LeaderboardData data;

    public static Leaderboard instance;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(SaveEntry);
    }


    public void RetreiveLeaderboard()
    {
        string savedLeaderboardDataJson = PlayerPrefs.GetString("leaderboardData", "");

        if (string.IsNullOrEmpty(savedLeaderboardDataJson))
        {
            print("Leaderboard : No leaderboard data available");
        }
        else
        {
            try
            {
                data = JsonUtility.FromJson<LeaderboardData>(savedLeaderboardDataJson);

                data.userEntries = data.userEntries.OrderByDescending(entry => entry.score).ToList();

                SetLeaderboardScrollview();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Leaderboard : Something went wrong during leaderboard data retreival? Error : {e}");
            }           
        }
    }

    void SetLeaderboardScrollview()
    {
        ClearAllEntries();

        int len = data.userEntries.Count;
        for (int i = 0; i < len; i++)
        {
            UserEntryUIItem newUserEntryUI = CreateEntry();
            newUserEntryUI.SetUsername(data.userEntries[i].username);
            newUserEntryUI.SetScore(data.userEntries[i].score.ToString());
        }
    }

    UserEntryUIItem CreateEntry()
    {
        GameObject newUserEntry = Instantiate(userEntryPrefab);

        newUserEntry.transform.SetParent(scrollviewContent, false);

        return newUserEntry.GetComponent<UserEntryUIItem>();  
    }

    void ClearAllEntries()
    {
        int len = scrollviewContent.childCount;
        for (int i = len - 1; i >= 0; i--)
        {
            Destroy(scrollviewContent.GetChild(i).gameObject);
        }
    }

    public void SaveEntry()
    {
        print("Performed saveEntry");

        // Retrieve saved leaderboard data
        string savedLeaderboardDataJson = PlayerPrefs.GetString("leaderboardData", "");
        LeaderboardData savedData;

        if (!string.IsNullOrEmpty(savedLeaderboardDataJson))
        {
            try
            {
                savedData = JsonUtility.FromJson<LeaderboardData>(savedLeaderboardDataJson);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Leaderboard : Error parsing saved leaderboard data: {e}");
                return;
            }
        }
        else
        {
            // If no saved data exists, create a new leaderboard data object
            savedData = new LeaderboardData();
            savedData.userEntries = new List<LeaderboardUserEntry>();
        }

        // Check if there's an existing entry for the current username
        LeaderboardUserEntry existingEntry = savedData.userEntries.FirstOrDefault(entry => entry.username == GameManager.Instance.username);

        if (existingEntry != null)
        {
            // Update existing entry's score
            if (GameManager.Instance.score > existingEntry.score) existingEntry.score = GameManager.Instance.score;
        }
        else
        {
            // Create a new entry
            LeaderboardUserEntry newEntry = new LeaderboardUserEntry();
            newEntry.username = GameManager.Instance.username;
            newEntry.score = GameManager.Instance.score;
            savedData.userEntries.Add(newEntry);
        }

        // Sort the entries by score in descending order
        savedData.userEntries = savedData.userEntries.OrderByDescending(entry => entry.score).ToList();

        // Convert the updated data to JSON
        string updatedLeaderboardDataJson = JsonUtility.ToJson(savedData);

        // Save the updated leaderboard data to PlayerPrefs
        PlayerPrefs.SetString("leaderboardData", updatedLeaderboardDataJson);
        PlayerPrefs.Save(); // Ensure data is saved immediately
    }

}

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardUserEntry> userEntries;
}

[System.Serializable]
public class LeaderboardUserEntry
{
    public string username;
    public int score;
}