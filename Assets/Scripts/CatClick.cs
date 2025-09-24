
using UnityEngine;

using UnityEngine;

public class CatClick : MonoBehaviour
{
    private bool isFound = false;

    private void OnMouseDown()
    {
        if (!isFound && CompareTag("Cat"))
        {
            isFound = true;

            // Kedinin rengini değiştir
            GetComponent<SpriteRenderer>().color = Color.red;

            // CatManager'a bildir
            CatManager.Instance.CatFound();

            // GameManager'daki counter'ı güncelle
            GameManager.Instance?.FoundCat();//her kedi bulunduğunda counter artıyor
        }
    }
}
   /* private Image catImage; // the real visible cat sprite
      private bool isClicked = false;

      void Start()
      {
          // Find the specific child named "CatImage" and get its Image component
          Transform child = transform.Find("CatImage");
          if (child != null)
          {
              catImage = child.GetComponent<Image>();
          }
          else
          {
              Debug.LogError("CatImage child not found!");
          }

          // Connect the button click
          GetComponent<Button>().onClick.AddListener(OnCatClicked);
      }

      void OnCatClicked()
      {
          if (isClicked || catImage == null) return;

          isClicked = true;
          catImage.color = Color.green;

          GameManager.Instance?.FoundCat();
      }
}*/
