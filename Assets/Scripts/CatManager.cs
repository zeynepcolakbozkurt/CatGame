using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CatManager : MonoBehaviour
{
    public static CatManager Instance;

    public int catsToShow = 15;
    public Transform catsParent;

    private int totalCats;
    private int foundCats;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        List<GameObject> allCats = new List<GameObject>();

        foreach (Transform child in catsParent)
        {
            allCats.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < catsToShow && allCats.Count > 0; i++)
        {
            int index = UnityEngine.Random.Range(0, allCats.Count);
            allCats[index].SetActive(true);
            allCats.RemoveAt(index);
        }

        totalCats = 0;
        foreach (Transform child in catsParent)
        {
            if (child.gameObject.activeSelf)
                totalCats++;
        }

        foundCats = 0;
        Debug.Log("Toplam kedi: " + totalCats);
    }

    public void CatFound()
    {
        foundCats++;
        Debug.Log("Bulunan kedi: " + foundCats + " / " + totalCats);

        if (foundCats >= totalCats)
        {
            GameManager.Instance?.GameWin();
        }
    }
}



/*using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public class CatManager : MonoBehaviour
{
    public static CatManager Instance;

    public int catsToShow = 15;
    public Transform catsParent;

    private int totalCats;
    private int foundCats;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        List<GameObject> allCats = new List<GameObject>();

        foreach (Transform child in catsParent)
        {
            allCats.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < catsToShow && allCats.Count > 0; i++)
        {
            int index = UnityEngine.Random.Range(0, allCats.Count);
            allCats[index].SetActive(true);
            allCats.RemoveAt(index);
        }

        // Gerçekte aktif olan kedileri say
        totalCats = 0;
        foreach (Transform child in catsParent)
        {
            if (child.gameObject.activeSelf)
                totalCats++;
        }

        foundCats = 0;
        Debug.Log("Toplam kedi: " + totalCats);
    }

    public void CatFound()
    {
        foundCats++;
        Debug.Log("Bulunan kedi: " + foundCats + " / " + totalCats);

        if (foundCats >= totalCats)
        {
            Debug.Log("Bütün kediler bulundu! Yeni sahneye geçiliyor...");

            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int nextIndex = currentIndex + 1;

            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.Log("Son sahneydi!");
            }
        }
    }
}*/


/*using UnityEngine;
using System.Collections.Generic;

public class CatManager : MonoBehaviour
{
    public static CatManager Instance;

    public int catsToShow = 15;
    public Transform catsParent;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        List<GameObject> allCats = new List<GameObject>();

        foreach (Transform child in catsParent)
        {
            allCats.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < catsToShow; i++)
        {
            int index = Random.Range(0, allCats.Count);
            allCats[index].SetActive(true);
            allCats.RemoveAt(index);
        }
    }
}*/
