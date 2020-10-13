using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks {
    public RectTransform rctRoomGUI;
    public InputField ifPlayer;
    public InputField ifRoom;
    public Button btnCreate;
    public Button btnJoin;
    public string gamePlaySceneName;

    private void Awake() {
        btnCreate.onClick.AddListener(Create);
        btnJoin.onClick.AddListener(Join);

        rctRoomGUI.gameObject.SetActive(false);
    }

#region Callbacks
    public override void OnJoinedLobby() {
        rctRoomGUI.gameObject.SetActive(true);
    }

    public override void OnJoinedRoom() {
        Debug.Log($"Joined room: {PhotonNetwork.CurrentRoom}.");
        PhotonNetwork.LoadLevel(gamePlaySceneName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.LogError($"Failed to join room. Error {returnCode}, {message}");
    }

    public override void OnCreatedRoom() {
        Debug.Log($"Created room: {PhotonNetwork.CurrentRoom}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.LogError($"Failed to create room. Error {returnCode}, {message}");
    }
    #endregion

    public void Create() {
        PhotonNetwork.NickName = ifPlayer.text;
        PhotonNetwork.CreateRoom(ifRoom.text, new RoomOptions { MaxPlayers = 4 }, null);
    }

    public void Join() {
        PhotonNetwork.NickName = ifPlayer.text;
        PhotonNetwork.JoinRoom(ifRoom.text, null);
    }
}
