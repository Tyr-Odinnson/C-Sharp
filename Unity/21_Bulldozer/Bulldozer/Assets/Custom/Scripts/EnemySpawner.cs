using UnityEngine;

[ExecuteInEditMode]
public class EnemySpawner : Spawner {
    private Transform[] transforms;

    public override void Initialize() {
        base.Initialize();
        if (!Application.isPlaying) return;

        transforms = GetComponentsInChildren<Transform>();
    }

    public override void Spawn() {
        if (currentAmount >= maxSpawns) {
            return;
        }

        int r = Random.Range(0, transforms.Length);
        GameObject go = Instantiate(prefab, transforms[r].position, transforms[r].rotation);
        go.GetComponent<ISpawnable>().SetSpawner(this);
        go.transform.SetParent(newParent);

        currentAmount++;
    }

    public override void DrawBox() {
        if (transforms == null || transforms.Length < 1) {
            transforms = GetComponentsInChildren<Transform>();
        }

        foreach (Transform t in transforms) {
            Gizmos.color = gizmoColour;
            Gizmos.DrawCube(t.position, t.localScale / 2);
        }
    }
}
