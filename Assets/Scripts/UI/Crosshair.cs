using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public GameObject crosshairPrefab;
    private GameObject crosshairInstance;
    private Camera mainCamera;

    void Start()
    {
        Cursor.visible = false; // Hide the default system cursor
        mainCamera = Camera.main;

        if (crosshairPrefab != null)
        {
            crosshairInstance = Instantiate(crosshairPrefab);
        }
    }

    void Update()
    {
        if (crosshairInstance != null)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Ensure crosshair stays on 2D plane
            crosshairInstance.transform.position = mousePos;
        }
    }
}
