using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
    private Animator anim;

    private void Awake() {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && Player.Instance.hasKey) {
            anim.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && Player.Instance.hasKey) {
            anim.SetBool("IsOpen", false);
        }
    }
}
