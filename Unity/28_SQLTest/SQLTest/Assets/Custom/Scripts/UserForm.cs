    public Button btnLogin;

    private GameObject panel;

    private void OnEnable() {
        //subscribe
        Events.OnLogIn += OnLogin;
        Events.OnLogOut += OnLogOut;
    }

    private void OnDisable() {
        //unsubscribe
        Events.OnLogIn -= OnLogin;
        Events.OnLogOut -= OnLogOut;
    }

    private void Awake() {
        btnLogin.onClick.AddListener(() => { DBSignUp.SignUp});

        panel = transform.GetChild(0).gameObject;

        OnLogOut();
    }

    private void OnLogin() {
        panel.SetActive(false);
        inputEmail.text = "";
        inputPassword.text = "";
    }

    private void OnLogOut() {
        panel.SetActive(true);
    }
}
