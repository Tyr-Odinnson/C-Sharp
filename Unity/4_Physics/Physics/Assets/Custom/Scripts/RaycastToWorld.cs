using UnityEngine;

public class RaycastToWorld : MonoBehaviour {
    public GameObject hoveredObject;
    public LayerMask layerMask;

    private Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        if (hoveredObject && Input.GetMouseButtonDown(0)) {
            Destroy(hoveredObject);
        }
    }

    private void FixedUpdate() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) {
            hoveredObject = hit.transform.gameObject;
            //Destroy(hoveredObject);
            hit.transform.position += Vector3.up;
        } else {
            hoveredObject = null;
        }

        Debug.DrawLine(ray.origin, hit.point, Color.cyan);

        //Vector3 direction = (hit.point - ray.origin).normalized;
        //float distance = Vector3.Distance(ray.origin, hit.point);

        //Debug.DrawRay(ray.origin, direction * distance, Color.magenta);
    }
}
