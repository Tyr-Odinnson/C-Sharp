using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public static class DBSignUp {

    public const string URI_SIGN_UP = "http://localhost/_unity_projects/unity_test/getUsers.php";



    private static bool IsValidEmailAddress(string s) {
        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return regex.IsMatch(s);
    }

    private static IEnumerator CreateUser (string _email, string _password) {
        WWWForm form = new WWWForm();
        form.AddField("email", _email);
        form.AddField("password", _password);

        UnityWebRequest webRequest = UnityWebRequest.Post(URI_SIGN_UP, form);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError) {
            Debug.Log("Error: " + webRequest.error);
        } else {
            string contents = webRequest.downloadHandler.text;

            if (contents.Contains("Error")) {
                Debug.Log
            }
        }
    }
}
