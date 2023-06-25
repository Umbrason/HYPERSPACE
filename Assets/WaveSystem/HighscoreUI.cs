
using TMPro;
using UnityEngine;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void OnEnable()
    {
        text.text = $" Highscore: {PlayerPrefs.GetInt("Highscore")}";
    }
}
