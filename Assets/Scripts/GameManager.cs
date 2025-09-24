using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI counterText; // Inspector'dan atayın

    [Header("Cats")]
    public int totalCatsToFind = 15;
    private int foundCount = 0;

    void Awake()
    {
        // Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Eski Coroutine ve sayıları temizle
        if (countdownText != null) countdownText.text = "";
        StopAllCoroutines();
    }

    void Start()
    {
        // CatManager’dan leveldaki aktif kedi sayısını al
        if (CatManager.Instance != null)
            totalCatsToFind = CatManager.Instance.catsToShow;

        UpdateCounterText();
    }

    // Her kedi bulunduğunda çağrılır
    public void FoundCat()
    {
        foundCount++;
        UpdateCounterText();

        if (foundCount >= totalCatsToFind)
        {
            GameWin();
        }
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
            counterText.text = foundCount + "/" + totalCatsToFind;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    public void GameWin()
    {
        StartCoroutine(GameWinSequence());
    }

    private void ShowMessage(string message)
    {
        if (messagePanel != null && messageText != null)
        {
            messagePanel.SetActive(!string.IsNullOrEmpty(message));
            messageText.text = message;
        }
    }

private IEnumerator GameWinSequence()
{
    // 1. Mesaj: Bravo!
    ShowMessage("Bravo! Bütün kedileri buldun!");
    if (countdownText != null)
        countdownText.text = ""; // countdown text temiz
    yield return new WaitForSeconds(2f);

    // 2. Mesaj: Yeni Bölüm Yükleniyor...
    ShowMessage("Yeni Bölüm Yükleniyor..."); // ShowMessage artık sadece messageText değil
    if (countdownText != null)
        countdownText.text = ""; // countdownText artık boş
    yield return new WaitForSeconds(2f);

    // 3. Next level kaydet ve MainMenu
    int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
    PlayerPrefs.SetInt("LastLevel", nextIndex);
    PlayerPrefs.Save();

    SceneManager.LoadScene("MainMenu");
}

    private IEnumerator GameOverSequence()
    {
        // 1. Mesaj: Game Over
        ShowMessage("Game Over");
        if (countdownText != null) countdownText.text = "";
        yield return new WaitForSeconds(2f);

        // 2. Mesaj: Menüye dönülüyor...
        ShowMessage("");
        if (countdownText != null) countdownText.text = "Menüye Dönülüyor...";
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainMenu");
    }
}


/*using UnityEngine;
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
}*/