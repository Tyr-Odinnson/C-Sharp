using UnityEngine;

[ExecuteInEditMode]
public class CameraFixedFollow3D : MonoBehaviour {
    public Transform target;
    public float distance = 7;
    public float height = 5;
    public float targetHeight = 0;

    private void LateUpdate() {
        if (!target) { return; }

        transform.position = target.position + (Vector3.back * distance) + (Vector3.up * height);
        transform.LookAt(target.position + (Vector3.up * targetHeight));
    }
}
