using System.Collections.Generic;
using UnityEngine;

public class BombCarrier : MonoBehaviour {
    private List<Transform> bombs = new List<Transform>();
    private Transform carriedBomb;

    protected virtual void Update() {
        if (Input.GetButtonDown("Submit")) {
            Interact();
        }

        Carry();
    }

    protected void Interact() {
        if (carriedBomb) {
            Drop();
        } else {
            PickUp();
        }
    }

    private void Drop() {
        carriedBomb.GetComponent<Bomb>().Drop();
        carriedBomb = null;
    }

    private void Carry() {
        if (carriedBomb) {
            carriedBomb.position = transform.position;
        }
    }

    private void PickUp() {
        CleanList();
            
        if (bombs.Count > 0 && !carriedBomb) {
            carriedBomb = bombs[0];
            carriedBomb.GetComponent<Bomb>().PickUp();
        }
    }

    private void CleanList() {
        if (bombs.Count > 0) {
            for (int i = bombs.Count - 1; i >= 0; i--) {
                if (bombs[i] == null) {
                    bombs.RemoveAt(i);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Bomb") {
            bombs.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Bomb") {
            bombs.Remove(other.transform);
        }
    }
}
