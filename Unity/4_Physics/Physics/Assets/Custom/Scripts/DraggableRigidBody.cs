
using System;
using UnityEngine;

public class DraggableRigidBody : MonoBehaviour
{
    public float force = 5;

    private Camera mainCamera;
    private Rigidbody selectedRigidbody;
    private Vector3 originalMousePosition;
    private Vector3 originalObjectPosition;
    private float selectionDistance;

    private void Awake() {
        mainCamera = Camera.main;
    }
    
    //updates every frame
    private void Update() {
        if (!mainCamera) {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {
            selectedRigidbody = GetRigidBodyFromMouseClick();
        }

        if (Input.GetMouseButtonUp(0)) {
            selectedRigidbody = null;
        }

    }

    private Rigidbody GetRigidBodyFromMouseClick() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            Rigidbody selected = hit.collider.gameObject.GetComponent<Rigidbody>();

            if (selected) {
                selectionDistance = Vector3.Distance(ray.origin, hit.point);
                originalMousePosition = GetMouseWorldPosition();
                originalObjectPosition = hit.transform.position;

                return selected;
            }
        }
        return null;
    }

    private void FixedUpdate() {
        if (selectedRigidbody) {
            Vector3 mousePositionOffset = GetMouseWorldPosition() - originalMousePosition;
            selectedRigidbody.velocity = ((originalMousePosition + mousePositionOffset) - selectedRigidbody.transform.position) * force;
        }    
    }

    private Vector3 GetMouseWorldPosition() {
        Vector2 mousePostition = Input.mousePosition;
        return mainCamera.ScreenToWorldPoint(new Vector3(mousePostition.x, mousePostition.y, selectionDistance));
    }
}
