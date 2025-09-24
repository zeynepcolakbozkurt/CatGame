using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class MissClickDetector : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    private Vector2 touchStartPos;
    private float touchStartTime;

    public float maxTapDuration = 0.2f; // seconds
    public float maxTapDistance = 20f;  // pixels

    void Update()
    {
        // Touch start
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            touchStartTime = Time.time;
        }

        // Touch end
        if (Input.GetMouseButtonUp(0))
        {
            float duration = Time.time - touchStartTime;
            float distance = Vector2.Distance(touchStartPos, Input.mousePosition);

            bool isTap = duration <= maxTapDuration && distance <= maxTapDistance;

            if (!isTap) return; // Don't count drags/swipes

            bool clickedCat = false;

            // 1) UI kontrolü (Canvas üzerindeki Cat objeleri için)
            PointerEventData pointerData = new PointerEventData(eventSystem);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);

            foreach (var result in results)
            {
                GameObject hitObject = result.gameObject;
                if (hitObject.CompareTag("Cat") || 
                    (hitObject.transform.parent != null && hitObject.transform.parent.CompareTag("Cat")))
                {
                    clickedCat = true;
                    break;
                }
            }

            // 2) Dünya sahnesi (SpriteRenderer + Collider2D olan kediler için)
            if (!clickedCat)
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("Cat"))
                {
                    clickedCat = true;
                }
            }

            // 3) Sonuç
            if (!clickedCat)
            {
                LifeManager.Instance?.LoseLife(Input.mousePosition);
            }
        }
    }
}





/*public class MissClickDetector : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    private Vector2 touchStartPos;
    private float touchStartTime;

    public float maxTapDuration = 0.2f; // seconds
    public float maxTapDistance = 20f;  // pixels

    void Update()
    {
        // Touch start
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            touchStartTime = Time.time;
        }

        // Touch end
        if (Input.GetMouseButtonUp(0))
        {
            float duration = Time.time - touchStartTime;
            float distance = Vector2.Distance(touchStartPos, Input.mousePosition);

            bool isTap = duration <= maxTapDuration && distance <= maxTapDistance;

            if (!isTap) return; // Don't count drags/swipes

            PointerEventData pointerData = new PointerEventData(eventSystem);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);

            bool clickedCat = false;

            foreach (var result in results)
            {
                GameObject hitObject = result.gameObject;

                // Check if this object or its parent is tagged as "Cat"
                if (hitObject.CompareTag("Cat") || (hitObject.transform.parent != null && hitObject.transform.parent.CompareTag("Cat")))
                {
                    clickedCat = true;
                    break;
                }
            }


            if (!clickedCat)
            {
                LifeManager.Instance?.LoseLife(Input.mousePosition);
            }
        }
    }
}*/

