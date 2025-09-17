using UnityEngine;

public class CameraController : MonoBehaviour


{
    [Header("Zoom Settings")]
    public float zoomSpeed = 0.5f;
    public float minZoom = 2f;
    public float maxZoom = 15f;

    [Header("Bounds")]
    public SpriteRenderer backgroundSprite;

    void Start()
    {
        if (backgroundSprite != null)
        {
            // Kamera sahnenin ortasında başlasın
            Vector3 center = backgroundSprite.bounds.center;
            transform.position = new Vector3(center.x, center.y, transform.position.z);

            // Başlangıç zoomu clamp ile ayarla
            Camera.main.orthographicSize = Mathf.Clamp(15f, minZoom, maxZoom);
        }
    }

    void Update()
    {
        if (backgroundSprite == null) return;

        // Kamera hiçbir şekilde hareket etmesin
        Vector3 center = backgroundSprite.bounds.center;
        transform.position = new Vector3(center.x, center.y, transform.position.z);

        // Zoom istersen aktif et
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float newSize = Mathf.Clamp(
                Camera.main.orthographicSize - scroll * 1000 * zoomSpeed * Time.deltaTime,
                minZoom,
                maxZoom
            );
            Camera.main.orthographicSize = newSize;
        }
    }
}





/*{
    [Header("Zoom Settings")]
    public float zoomSpeed = 0.5f;
    public float minZoom = 2f;
    public float maxZoom = 15f;

    [Header("Pan Settings")]
    [Range(0.5f, 2f)]
    public float panSpeed = 1f;

    [Range(5f, 20f)]
    public float panSmoothness = 10f;

    [Header("Bounds")]
    public SpriteRenderer backgroundSprite;

    private Vector3 lastPanPosition;
    private int panFingerId;
    private bool wasZoomingLastFrame;
    private Vector2[] lastZoomPositions;

    private float zoomCooldown = 0.1f;
    private float zoomCooldownTimer = 0f;

    private Vector3 targetPosition;

    void Start()
    {
        Camera.main.orthographicSize = Mathf.Clamp(15f, minZoom, maxZoom);
        float camHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float leftEdgeX = backgroundSprite.bounds.min.x;
        float centerY = backgroundSprite.bounds.center.y;

        targetPosition = new Vector3(leftEdgeX + camHalfWidth, centerY, transform.position.z);
        transform.position = targetPosition;
    }

    void Update()
    {
        if (zoomCooldownTimer > 0f)
            zoomCooldownTimer -= Time.deltaTime;

        if (Input.touchSupported && Application.isMobilePlatform)
            HandleTouch();
        else
            HandleMouse();

        // Smooth movement toward target
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * panSmoothness
        );
    }

    void HandleTouch()
    {
        if (Input.touchCount == 1 && zoomCooldownTimer <= 0f)
        {
            wasZoomingLastFrame = false;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastPanPosition = touch.position;
                panFingerId = touch.fingerId;
            }
            else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
            {
                PanCamera(touch.position);
            }
        }
        else if (Input.touchCount == 2)
        {
            Vector2[] newPositions = { Input.GetTouch(0).position, Input.GetTouch(1).position };

            if (!wasZoomingLastFrame)
            {
                lastZoomPositions = newPositions;
                wasZoomingLastFrame = true;
            }
            else
            {
                float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                float offset = newDistance - oldDistance;

                ZoomCamera(offset, zoomSpeed);

                lastZoomPositions = newPositions;
                zoomCooldownTimer = zoomCooldown;
            }
        }
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
            lastPanPosition = Input.mousePosition;
        else if (Input.GetMouseButton(0))
            PanCamera(Input.mousePosition);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll * 1000, zoomSpeed);
    }

    void PanCamera(Vector3 newPanPosition)
    {
        // Convert screen delta to world delta using zoom and screen size
        Vector3 screenDelta = newPanPosition - lastPanPosition;
        float unitsPerPixel = 2f * Camera.main.orthographicSize / Screen.height;
        Vector3 worldDelta = new Vector3(screenDelta.x * unitsPerPixel, screenDelta.y * unitsPerPixel, 0);

        // Move target camera position
        targetPosition = ClampCameraPosition(targetPosition - worldDelta * panSpeed);
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0) return;

        float newSize = Mathf.Clamp(
            Camera.main.orthographicSize - offset * speed * Time.deltaTime,
            minZoom,
            CalculateMaxZoom()
        );

        Camera.main.orthographicSize = newSize;

        // Re-clamp target position to keep camera inside bounds
        targetPosition = ClampCameraPosition(targetPosition);
    }

    Vector3 ClampCameraPosition(Vector3 target)
    {
        if (backgroundSprite == null) return target;

        Bounds bounds = backgroundSprite.bounds;

        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        float minX = bounds.min.x + horzExtent;
        float maxX = bounds.max.x - horzExtent;
        float minY = bounds.min.y + vertExtent;
        float maxY = bounds.max.y - vertExtent;

        target.x = minX > maxX ? bounds.center.x : Mathf.Clamp(target.x, minX, maxX);
        target.y = minY > maxY ? bounds.center.y : Mathf.Clamp(target.y, minY, maxY);
        target.z = transform.position.z;

        return target;
    }

    float CalculateMaxZoom()
    {
        if (backgroundSprite == null) return maxZoom;

        Bounds bounds = backgroundSprite.bounds;
        float maxVert = bounds.size.y / 2f;
        float maxHorz = (bounds.size.x / 2f) * (Screen.height / (float)Screen.width);

        return Mathf.Min(maxZoom, Mathf.Min(maxVert, maxHorz));
    }
}*/
