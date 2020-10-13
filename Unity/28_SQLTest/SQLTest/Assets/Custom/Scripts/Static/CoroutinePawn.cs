using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinePawn : MonoBehaviour {
    public static CoroutinePawn Instance;

    private void Awake() {
        Instance = this;
    }
}
