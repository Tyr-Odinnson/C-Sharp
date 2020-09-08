using System.Collections;
using UnityEngine;

public class BCoroutineStacking : MonoBehaviour {
    public float shrinkRate = 2.0f;
    public float growthRate = 8.0f;

    private void OnMouseDown() {
        StartCoroutine(Respond());
    }

    void Update() {
        if (transform.localScale.magnitude > 2) {
            transform.localScale -= Vector3.one * shrinkRate * Time.deltaTime;
        }
    }

    private IEnumerator Respond() {
        float t = 0;
        float duration = 0.125f;

        while (t < duration) {
            transform.localScale += Vector3.one * growthRate * Time.deltaTime;

            t += Time.deltaTime;
            yield return null;
        }
    }
}
