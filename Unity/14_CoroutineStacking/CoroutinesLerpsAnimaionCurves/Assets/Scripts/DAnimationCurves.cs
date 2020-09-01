using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DAnimationCurves : MonoBehaviour
{
    public Vector3 start = -Vector3.one;
    public Vector3 end = Vector3.one;
    public AnimationCurve curve;
    public Gradient gradient;

    [Range(0f, 1f)]
    public float phase = 0.5f;

    private void Update() {
        Move();
        ChangeColor();
    }

    private void ChangeColor() {
        GetComponent<Renderer>().material.color = gradient.Evaluate(phase);
    }

    private void Move() {
        Vector3 v = Vector3.Lerp(start, end, phase);

        float y = (curve.Evaluate(phase) * 2 - 1) * 5;
        v.y = y;

        transform.position = v;

        //transform.position = Vector3.Lerp(start, end, curve.Evaluate(phase));
    }
}
