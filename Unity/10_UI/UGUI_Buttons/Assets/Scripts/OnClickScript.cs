using UnityEngine;
using UnityEngine.UI;

public class OnClickScript : MonoBehaviour
{
    public bool isTrue;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            Debug.Log(isTrue); 
        });
    }
}
