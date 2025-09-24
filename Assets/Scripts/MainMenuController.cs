using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Start()
    {
        int lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        if (levelText != null)
            levelText.text = "Level " + lastLevel;
    }

    public void PlayGame()
    {
        int lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        SceneManager.LoadScene(lastLevel);
    }

    public void QuitGame()
    {
        Debug.Log("Oyun Kapatılıyor...");
        Application.Quit();
    }
}


/*using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Start()
    {
        int lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        levelText.text = "Level: " + lastLevel;
    }

    public void PlayGame()
    {
        int nextLevel = PlayerPrefs.GetInt("LastLevel", 1);
        SceneManager.LoadScene(nextLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}*/