using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpawnable {
    public ParticleSystem prefabEffect;
    public Gradient colourOverLifeTime;
    public Gradient warningColor;
    public AnimationCurve warningScale;
    public float duration = 5;
    public LayerMask layerMask;

    public Spawner spawner { get; set; }
    
    private Rigidbody rb;
    private ParticleSystem.MainModule psMain;
    private Material material;
    private SpriteRenderer warningSign;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        psMain = GetComponentInChildren<ParticleSystem>().main;
        material = GetComponent<Renderer>().material;
        warningSign = GetComponentInChildren<SpriteRenderer>();

        StartCoroutine(Countdown());
    }

    private void Update() {
        SetWarningSign();
    }

    public void PickUp() {
        rb.isKinematic = true;
    }
    
    public void Drop() {
        rb.isKinematic = false;
    }

    private void SetWarningSign() {
        Vector3 pos = transform.position;
        pos.y = 0.1f;
        warningSign.transform.position = pos;
        warningSign.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private IEnumerator Countdown() {
        float t = 0;

        while (t < duration) {
            float progress = t / duration;

            psMain.startColor = colourOverLifeTime.Evaluate(progress) * 2;
            material.SetColor("_Emission", colourOverLifeTime.Evaluate(progress));
            material.SetFloat("_Progress", progress);
            warningSign.color = warningColor.Evaluate(progress);
            warningSign.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 4.5f, warningScale.Evaluate(progress));

            t += Time.deltaTime;
            yield return null;
        }
        
        Explode();
    }

    private void Explode() {
        Instantiate(prefabEffect, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, layerMask);
        if (colliders.Length > 0) {
            foreach (Collider enemy in colliders) {
                enemy.GetComponent<Enemy>().Degrade();
            }
        }

        Destroy(gameObject);
    }

    public void SetSpawner(Spawner _spawner) {
        spawner = _spawner;
    }

    public void OnDestroy() {
        spawner.Deduct();
    }
}
