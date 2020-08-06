using UnityEngine;

public class DraggableGameObject : MonoBehaviour
{
    private Vector3 originalPosition;

    private void OnMouseDown() {
        originalPosition = transform.position - GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDrag() {
        transform.position = GetMouseWorldPosition() + originalPosition;
    }
}
