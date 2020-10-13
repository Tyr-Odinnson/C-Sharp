using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnConnect : MonoBehaviourPunCallbacks, IPointerUpHandler {
    private Text txt;
    
    private void Awake() {
        txt = transform.GetChild(0).GetComponent<Text>();
    }

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.Disconnect();
        } else {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Connected!");
        txt.text = "Disconnect";
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log(cause.ToString());
        txt.text = "Connect";
    }
}
