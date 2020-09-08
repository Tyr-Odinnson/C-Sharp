using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CLerp : MonoBehaviour {

    public float value;
    public Vector3 start = -Vector3.one;
    public Vector3 end = Vector3.one;
    public Color colour;

    [Range(0f, 1f)]
    public float phase = 0.5f;

    private void Update() {
        value = Mathf.Lerp(6.3f, 9.8f, phase);
        
        //transform.position = Vector3.Lerp(start, end, phase);
        //transform.position = Vector3.Slerp(start, end, phase);
        colour = Color.Lerp(Color.red, Color.blue, phase);
        GetComponent<Renderer>().material.color = colour;

        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(Move());
        }
    }
    private IEnumerator Move() {
        float t = 0;
        float duration = 4;

        while (t < duration) {
        //while (Vector3.Distance(transform.position, end) > 0.1f) {

            transform.position = Vector3.Lerp(start, end, t / duration);
            //transform.position = Vector3.Lerp(transform.position, end, Time.deltaTime * 2);

            Debug.Log(t / duration);

            t += Time.deltaTime;
            yield return null;
        }
    }
}
