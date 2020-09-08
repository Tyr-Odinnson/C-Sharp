using UnityEngine;


[ExecuteInEditMode]
public class EInverseLerp : MonoBehaviour
{
    public Gradient gradient;
    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor() {
        float t = Mathf.InverseLerp(3, 9, transform.position.x);

        GetComponent<Renderer>().material.color = gradient.Evaluate(t);
    }
}
