using UnityEngine;

[ExecuteInEditMode]
public class CameraOrbit : MonoBehaviour {
    public static CameraOrbit Instance;

    public float targetHeight = 1.5f;
    public float cameraHeight = 2.5f;
    public float zoom = 10;
    public float mouseSensitivity = 5;
    public float zoomSensitivity = 10;
    public float zoomSmoothTime = 0.3f;
    public Vector2 minMaxPitch = new Vector2(-10, 75);
    public Vector2 minMaxZoom = new Vector2(5.5f, 14);
    
    public Transform target { get; set; }

    public float pitch = 5;
    public float yaw;

    private Transform pitchTransform;
    private Transform cameraTransform;
    private Vector3 velocity;

    private void Awake() {
        Instance = this;

        Initialize();
    }

    private void Update() {
        if (!target) { return; }

        GetInput();
        ClampValues();
    }

    private void FixedUpdate() {
        if (!target) { return; }

        if (!pitchTransform || !cameraTransform) {
            Initialize();
            return;
        }

        SetYaw();
        SetPitch();
        SetZoom();
    }

    private void LateUpdate() {
        if (!target) { return; }

        if (!pitchTransform || !cameraTransform) {
            Initialize();
            return;
        }

        FollowTarget();
        LookAtTarget();
    }

    private void Initialize() {
        pitchTransform = transform.GetChild(0);
        cameraTransform = Camera.main.transform;
    }

    public void SetTarget(Transform _transform) {
        target = _transform;
    }

    private void ClampValues() {
        pitch = Mathf.Clamp(pitch, minMaxPitch.x, minMaxPitch.y);
        zoom = Mathf.Clamp(zoom, minMaxZoom.x, minMaxZoom.y);
    }

    private void GetInput() {
        if (Input.GetMouseButton(1)) {
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        }

        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
    }

    private void LookAtTarget() {
        cameraTransform.LookAt(target.position + (Vector3.up * targetHeight));
    }

    private void FollowTarget() {
        transform.position = target.position + (Vector3.up * cameraHeight);
    }

    private void SetYaw() {
        transform.localEulerAngles = new Vector3(0, yaw, 0);
    }

    private void SetPitch() {
        pitchTransform.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    private void SetZoom() {
        cameraTransform.localPosition = Vector3.SmoothDamp(cameraTransform.localPosition, Vector3.back * zoom, ref velocity, zoomSmoothTime);
    }
}
