using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private Material mat;

   

    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
        mat.color = new Color(0.5f, 0, 0.5f);
    }

    private void OnCollisionExit(Collision collision) {
        mat.color = Color.cyan;
    }
}