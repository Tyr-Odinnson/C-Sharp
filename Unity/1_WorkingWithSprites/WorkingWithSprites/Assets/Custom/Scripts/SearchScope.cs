using UnityEngine;

public class SearchScope : MonoBehaviour {
    private Camera mainCamera;
    private SpriteRenderer spr;

    private void Awake() {
        mainCamera = Camera.main;
        spr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        // Get the mousePosition. Z must be set depending on the camera.
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;

        // Convert the mouse position from screenspace into worldspace. Doctor the zPos.
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        mouseWorldPosition.z = transform.position.z;

        // Get the direction for raycast. And get the distance it should reach.
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, mouseWorldPosition);

        Debug.DrawRay(transform.position, direction * distance, Color.magenta);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);
        
        Debug.Log(hit.transform?.name);
        spr.color = hit ? Color.green : Color.red;
    }
}