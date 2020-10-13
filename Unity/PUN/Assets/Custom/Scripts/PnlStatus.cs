using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PnlStatus : MonoBehaviourPunCallbacks {
    public static PnlStatus Instance;

    private Image image;
    private Text text;

    private void Awake() {
        Instance = this;
        image = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();

        SetStatus();
    }

    public void SetStatus() {
        bool isConnected = PhotonNetwork.IsConnected;

        image.color = isConnected ? Color.green : Color.red;
        text.text = isConnected ? "Connected" : "Disconnected";
    }

#region PUN2 callbacks.
    public override void OnConnectedToMaster() {
        SetStatus();
    }

    public override void OnDisconnected(DisconnectCause _cause) {
        Debug.LogWarning(_cause);
        SetStatus();
    }
#endregion
}