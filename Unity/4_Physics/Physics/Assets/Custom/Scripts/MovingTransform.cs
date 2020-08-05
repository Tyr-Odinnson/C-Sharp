using UnityEngine;

public class MovingTransform : MonoBehaviour
{
    private float speed = 5;

    // Update is called once per frame
    void Update() {
        Debug.Log($"{Input.GetAxis("Horizontal").ToString("f1")}, {Input.GetAxis("Vertical").ToString("f1")}");
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed;
    }
}
