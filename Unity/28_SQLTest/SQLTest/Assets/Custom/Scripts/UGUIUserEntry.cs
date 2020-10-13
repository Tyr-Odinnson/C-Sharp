using UnityEngine;
using UnityEngine.UI;

public class UGUIUserEntry : MonoBehaviour {
    public Text txtID;
    public Text txtEmail;

    public void Set(string _id, string _email) {
        txtID.text = _id;
        txtEmail.text = _email;
    }
}
