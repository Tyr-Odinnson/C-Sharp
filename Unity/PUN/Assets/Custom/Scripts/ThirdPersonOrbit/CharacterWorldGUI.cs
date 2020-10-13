using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWorldGUI : MonoBehaviour {
    public TextMeshProUGUI text;
    public Image imgHp;

    public void SetText(string _name) {
        text.text = _name;
    }

    public void SetHealthGauge(float _fillAmount) {
        imgHp.fillAmount = _fillAmount;
    }

    private void LateUpdate() {
        Vector3 direction = (transform.position - Camera.main.transform.position).normalized;
        transform.LookAt(transform.position + direction);
    }
}
