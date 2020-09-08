using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowSmooth : MonoBehaviour {
    public float dampTime = 0.15f;
    public Transform target;
    public bool useLimitX;
    public Vector2 minMaxX = new Vector2(-100, 100);
    public bool useLimitY;
    public Vector2 minMaxY = new Vector2(-100, 100);

    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;

    void Awake() {
        mainCamera = GetComponent<Camera>();
    }

    void FixedUpdate() {
        if (Application.isPlaying) {
            MoveInGame();
        } else {
            MoveInEditor();
        }

        DebugCameraLimits();
    }

    private void MoveInGame() {
        if (target) {
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, target.position, ref velocity, dampTime);
            newPosition.z = transform.position.z;

            transform.position = RestrictCameraPosition(newPosition);
        }
    }

    private Vector3 RestrictCameraPosition(Vector3 _newPosition) {
        if (useLimitX) {
            _newPosition.x = Mathf.Clamp(_newPosition.x, minMaxX.x, minMaxX.y);
        }

        if (useLimitY) {
            _newPosition.y = Mathf.Clamp(_newPosition.y, minMaxY.x, minMaxY.y);
        }

        return _newPosition;
    }

    private void MoveInEditor() {
        if (!mainCamera) {
            mainCamera = GetComponent<Camera>();
        }

        float xPos = target.position.x;
        float yPos = target.position.y;

        if (useLimitX) {
            xPos = Mathf.Clamp(target.position.x, minMaxX.x, minMaxX.y);
        }
        if (useLimitY) {
            yPos = Mathf.Clamp(target.position.y, minMaxY.x, minMaxY.y);
        }

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }

    private void DebugCameraLimits() {
        Color debugColour = Color.magenta;

        if (useLimitY) {
            float centerOfY = Mathf.Lerp(minMaxY.x, minMaxY.y, 0.5f);

            Vector3 debugStart = new Vector2(minMaxX.x, centerOfY);
            Vector3 debugEnd = new Vector2(minMaxX.y, centerOfY);

            Debug.DrawLine(debugStart, debugEnd, debugColour);
        }

        if (useLimitX) {
            float centerOfX = Mathf.Lerp(minMaxX.x, minMaxX.y, 0.5f);

            Vector3 debugStart = new Vector2(centerOfX, minMaxY.x);
            Vector3 debugEnd = new Vector2(centerOfX, minMaxY.y);

            Debug.DrawLine(debugStart, debugEnd, debugColour);
        }
    }
}
