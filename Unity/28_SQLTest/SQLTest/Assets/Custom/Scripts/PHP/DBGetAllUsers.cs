using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBGetAllUsers : MonoBehaviour {
    private const string URI = "http://localhost/_unity_projects/unity_test/getUsers.php";

    public string[] userStrings;
    public List<UserData> users = new List<UserData>();

    protected virtual void Awake() {
        StartCoroutine(GetData());
    }

    private IEnumerator GetData() {
        UnityWebRequest webRequest = UnityWebRequest.Get(URI);

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError) {
            Debug.Log("Error: " + webRequest.error);
        } else {
            Debug.Log(webRequest.downloadHandler.text);

            UnpackUsers(webRequest.downloadHandler.text);
        }
    }

    protected virtual void UnpackUsers(string _content) {
        userStrings = _content.Split(';');
        Array.Resize(ref userStrings, userStrings.Length - 1);

        users.Clear();
        for (int i = 0; i < userStrings.Length; i++) {
            users.Add(new UserData(
                      99,
                      GetDataValue(userStrings[i], "email"),
                      GetDataValue(userStrings[i], "password")
                      ));
        }
    }

    private string GetDataValue(string _data, string _propertyString) {
        _propertyString += ":";
        string value = _data.Substring(_data.IndexOf(_propertyString) + _propertyString.Length);
        value = value.Replace("<br>", "");


            if (value.Contains("|")) {
                value = value.Remove(value.IndexOf("|"));
        }

        return value;
    }
}
