using UnityEngine;


[ExecuteInEditMode]
public class CameraFixedFollow2D : MonoBehaviour
{
    public Transform target;
    public float height = 5;

    private void LateUpdate()
    {
        if (!target)
        {
            return;
        }
        transform.position = target.position + (Vector3.back * 10) + (Vector3.up * height);
    }
}
