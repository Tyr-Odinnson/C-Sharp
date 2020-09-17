using UnityEngine;

[ExecuteInEditMode]
public class Player : MonoBehaviour
{
    public Material material;

    private void Update() {
        if (!material) return;

        material.SetVector("_WorldPosition", transform.position);
    }
}
