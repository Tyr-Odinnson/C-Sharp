public delegate void Method();

public class Events {
    public static event Method OnLogIn;
    public static void LogIn() {
        OnLogIn?.Invoke();
    }

    public static event Method OnLogOut;
    public static void LogOut() {
        OnLogOut?.Invoke();
    }
}
