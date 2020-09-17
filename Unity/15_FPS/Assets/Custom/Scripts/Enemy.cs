using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour {
    private int hp = 10;
    private NavMeshAgent agent;
    private Material material;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponent<Renderer>().material;
    }

    private void Update() {
        agent.SetDestination(Player.Instance.transform.position);

        material.color = Player.Instance.isInCombat ? Color.red : Color.cyan;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Bullet") {
            Bullet bullet = collision.transform.GetComponent<Bullet>();

            if (bullet.rb.velocity.magnitude > 0.5f && Player.Instance.isInCombat) {
            bullet.Explode();

                hp--;

                if (hp <= 0) {
                    Destroy(gameObject);
                }
            }
        }
    }
}
