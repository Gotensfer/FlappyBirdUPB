using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserEntryUIItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI username;
    [SerializeField] TextMeshProUGUI score;

    public void SetUsername(string username)
    {
        this.username.text = username;
    }

    public void SetScore(string score)
    {
        this.score.text = score;
    }
}
