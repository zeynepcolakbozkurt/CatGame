using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    public TextMeshProUGUI lifeText;
    public int lives = 5;

    public GameObject failIconPrefab;
    public Canvas canvas;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateLifeUI();
    }

    public void LoseLife(Vector2 screenPosition)
    {
        if (lives <= 0) return;

        lives--;
        UpdateLifeUI();
        ShowFailIcon(screenPosition);

        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            GameManager.Instance?.GameOver();
        }
    }

    void UpdateLifeUI()
    {
        lifeText.text = lives.ToString();
    }

    void ShowFailIcon(Vector2 screenPosition)
    {
        GameObject icon = Instantiate(failIconPrefab, canvas.transform);
        icon.transform.position = screenPosition;
        Destroy(icon, 1f);
    }
}
