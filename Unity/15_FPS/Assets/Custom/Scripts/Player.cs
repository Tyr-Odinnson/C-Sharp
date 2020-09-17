using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public static Player Instance;

    public bool hasKey;
    public bool hasGun;
    public bool isInCombat;

    private void Awake() {
        Instance = this;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "KillBox" || collision.transform.tag == "Enemy") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.contacts[0].normal == Vector3.up) {
            isInCombat = collision.transform.tag == "BattleField";
        }
    }
}
