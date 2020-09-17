using UnityEngine;

public class PickUpKey : MonoBehaviour {
    private void Update() {
        transform.Rotate(Vector3.up, Time.deltaTime * 180);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Player.Instance.hasKey = true;
            Destroy(gameObject);
        }
    }
}
