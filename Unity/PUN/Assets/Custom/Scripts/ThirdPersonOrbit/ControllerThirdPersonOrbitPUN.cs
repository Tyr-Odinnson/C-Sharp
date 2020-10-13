using Photon.Pun;
using System;
using UnityEngine;

public class ControllerThirdPersonOrbitPUN : ControllerThirdPersonOrbit, IPunObservable
{
    public int hp = 10;
    public string prfBulletPath = "Prefabs/";
    public float bulletSpawnDistance = 1.0f;
    public CharacterWorldGUI gui;

    private int maxHP;
    private Vector3 latestPosition;

    protected override void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (photonView.IsMine) {
            FindObjectOfType<CameraOrbit>()?.SetTarget(transform);

            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 20;
        }

        gameObject.tag = photonView.IsMine ? "Player" : "Enemy";
        gui.SetText(photonView.Owner.NickName);
        maxHP = hp;
    }

    protected override void Update() {
        if (photonView.IsMine) {
        base.Update();
        } else {
            SmoothMove();
        }
        
    }

    private void SmoothMove() {
        transform.position = Vector3.Lerp(transform.position, latestPosition, Time.deltaTime * 10);
    }

    protected override void FixedUpdate() {
        if (photonView.IsMine) {
        base.FixedUpdate();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
        } else if (stream.IsReading) {
            latestPosition = (Vector3)stream.ReceiveNext();
        }
    }
}
