using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private Material mat;

    private void Start() {
        mat = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other) {
        GetComponent<Renderer>().material.color = new Color(0.5f, 0, 0.5f);
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<Renderer>().material.color = Color.cyan;
    }
}
