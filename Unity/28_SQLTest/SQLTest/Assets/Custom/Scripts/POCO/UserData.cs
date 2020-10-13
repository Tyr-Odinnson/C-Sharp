using System;

[Serializable]
public class UserData {
    public static UserData CurrentUserData;

    public UserData() {
    }

    public UserData(int _id, string _email, string _password) {
        id = _id;
        email = _email;
        password = _password;
    }

    public int id;
    public string email;
    public string password;
}
