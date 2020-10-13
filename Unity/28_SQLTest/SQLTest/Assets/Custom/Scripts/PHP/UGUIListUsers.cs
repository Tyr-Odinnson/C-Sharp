using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUIListUsers : DBGetAllUsers {
    UGUIUserEntry[] entries;

    protected override void Awake() {
        base.Awake();

        entries = GetComponentsInChildren<UGUIUserEntry>();
    }

    protected override void UnpackUsers(string _content) {
        base.UnpackUsers(_content);

        for (int i = 0; i < entries.Length; i++) {
            entries[i].Set(users[i].id.ToString(), users[i].email);
        }
    }
}
