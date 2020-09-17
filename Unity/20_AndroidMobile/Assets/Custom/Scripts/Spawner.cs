using UnityEngine;

[ExecuteInEditMode]
public class Spawner : MonoBehaviour {
    public GameObject prefab;
    public int maxSpawns = 5;
    public float interval = 4;
    public Color gizmoColour = new Color(0, 1, 0, .3f);
    public LayerMask layerMask;

    protected int currentAmount;
    protected Transform newParent;

    public virtual void Awake() {
        Initialize();
    }

    public virtual void Initialize() {
        if (!Application.isPlaying) return;

        newParent = new GameObject(name + "Parent").transform;
        layerMask = ~layerMask;
        InvokeRepeating("Spawn", 0, interval);
    }

    private void OnDrawGizmos() {
        DrawBox();
    }

    public virtual void DrawBox() {
        Vector3 displayScale = SetDisplayScale();
        Vector3 displayPosition = SetDisplayPosition(displayScale);

        Gizmos.color = gizmoColour;
        Gizmos.DrawCube(displayPosition, displayScale);
    }

    private Vector3 SetDisplayPosition(Vector3 displayScale) {
        Vector3 displayPosition = transform.position;
        float halfScaleY = displayScale.y / 2;
        displayPosition.y += halfScaleY;

        return displayPosition;
    }

    private Vector3 SetDisplayScale() {
        Vector3 displayScale = transform.localScale;
        displayScale.y = 1;

        return displayScale;
    }

    public virtual void Spawn() {
        if (currentAmount >= maxSpawns) return;

        Vector3 randomPosition = GetRandomPosition();

        for (int i = 0; i < 10; i++) {
            Collider[] colliders = Physics.OverlapSphere(randomPosition, 2, layerMask);
            if (colliders.Length > 0) {
                randomPosition = GetRandomPosition();
                continue;
            }

            GameObject go = Instantiate(prefab, transform.position + randomPosition, transform.rotation);
            go.GetComponent<ISpawnable>().SetSpawner(this);
            go.transform.SetParent(newParent);

            currentAmount++;
            
            break;
        }
    }

    private Vector3 GetRandomPosition() {
        float scX = transform.localScale.x / 2;
        float scZ = transform.localScale.z / 2;
        float rX = Random.Range(-scX, scX);
        float rZ = Random.Range(-scZ, scZ);

        return new Vector3(rX, transform.position.y, rZ);
    }

    public void Deduct() {
        currentAmount--;
    }
}
