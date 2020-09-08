using System.Collections;
using UnityEngine;

public class BasicTimer : MonoBehaviour {
    #region Awful timer.
    private float currentTime;
    private bool isExpired;

    private void AwfulCountDown() {
        if (currentTime < 3) {
            currentTime += Time.deltaTime;
        } else if (!isExpired) {
            isExpired = true;
            Debug.Log("Time up!");
        }
    }
    #endregion
    private Coroutine corRunTimer;

    private void Awake() {
        Debug.Log("Press 'Q' to start.");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && corRunTimer == null) {
            corRunTimer = StartCoroutine(RunTimer());
        }
    }

    private IEnumerator RunTimer() {
        Debug.Log("Please wait 3 seconds.");

        yield return new WaitForSeconds(3);

        Debug.Log("Wait again, but watch the time.");

        float f = 0;
        while (f < 3) {
            f += Time.deltaTime;
            Debug.Log(f);
            yield return null;
        }

        Debug.Log("Time's up! Press 'A' to continue.");

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A));
        Debug.Log("Hold 'A' for as long as you want.");
        yield return new WaitWhile(() => Input.GetKey(KeyCode.A));
        Debug.Log("Release 'A' to continue.");
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.A));

        Debug.Log("Press A again. Release and wait a moment.");
        while (!Input.GetKeyDown(KeyCode.A)) {
            yield return null;
        }

		Debug.Log("Yielding 'null' to skip frames for no real reason at all...");
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        Debug.Log("Yielding 'null' again... this time in a loop...");
        for (int i = 0; i < 20; i++) {
            yield return null;
        }

        yield return StartCoroutine(DelayForNoReason());

        Debug.Log("All done! Releasing coroutine.");
        corRunTimer = null;
    }

    private IEnumerator DelayForNoReason() {
        Debug.Log("Now wait for 2 seconds.");
        yield return new WaitForSeconds(2);
    }
}
