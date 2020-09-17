using UnityEngine;

public class Gun : MonoBehaviour {
    public float spawnDistance = 1;
    public Bullet bullet;

    private Transform tCamera;

    private void Awake() {
        tCamera = Camera.main.transform;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && Player.Instance.hasGun) {
            Vector3 spawnPos = tCamera.position + (tCamera.forward * spawnDistance);
            Instantiate(bullet, spawnPos, Quaternion.identity);
        }
    }
}
