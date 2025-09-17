using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI countdownText;

    public int totalCatsToFind = 15;
    private int foundCount = 0;

    public TextMeshProUGUI counterText; // Assign this in the Inspector

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Application.targetFrameRate = 144;
        // QualitySettings.vSyncCount = 0;

        // Get number of cats to find from CatManager
        if (CatManager.Instance != null)
        {
            totalCatsToFind = CatManager.Instance.catsToShow;
        }

        UpdateCounterText();
    }

    public void FoundCat()
    {
        foundCount++;
        UpdateCounterText();

        if (foundCount >= totalCatsToFind)
        {
            GameManager.Instance?.GameWin();
        }
    }

    void UpdateCounterText()
    {
        counterText.text = foundCount + "/" + totalCatsToFind;
    }

    public void GameOver()
    {
        ShowMessage("Game Over");
        StartCoroutine(RestartCountdown());
    }

    public void GameWin()
    {
        ShowMessage("You Found All the Cats!");
        StartCoroutine(RestartCountdown());
    }

    private void ShowMessage(string message)
    {
        messagePanel.SetActive(true);
        messageText.text = message;
    }

    private IEnumerator RestartCountdown()
    {
        countdownText.text = "";
        for (int i = 3; i >= 1; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}