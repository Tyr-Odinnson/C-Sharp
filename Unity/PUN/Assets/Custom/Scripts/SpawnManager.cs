using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public string prefabPath = "Prefabs/";

    private void Awake() {
        PhotonNetwork.Instantiate(prefabPath, transform.position, Quaternion.identity);
    }
}
