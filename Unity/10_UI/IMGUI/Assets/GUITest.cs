using UnityEngine;

[ExecuteInEditMode]
public class GUITest : MonoBehaviour { 

    public enum Anchor
{
    TOP_LEFT,
    TOP_RIGHT,
    BOTTOM_RIGHT,
    BOTTOM_LEFT,
    CENTRE
}

    public Rect rectTest = new Rect(5, 5, 100, 50);
    public Anchor anchor = Anchor.TOP_LEFT;
    // Update is called once per frame
    void OnGUI()
    {
        Rect newRect = rectTest;

        switch (anchor)
        {
        case Anchor.TOP_RIGHT:
            newRect.x = Screen.width - rectTest.width - rectTest.x;
            break;
        case Anchor.BOTTOM_RIGHT:
            newRect.x = Screen.width - rectTest.width - rectTest.x;
            newRect.y = Screen.height - rectTest.height - rectTest.y;
            break;
        case Anchor.BOTTOM_LEFT:
            newRect.y = Screen.height - rectTest.height - rectTest.y;
            break;
        case Anchor.CENTRE:
            newRect.x = Screen.width/2 - rectTest.width - rectTest.x;
            newRect.y = Screen.height/2 - rectTest.height - rectTest.y;
            break;
    }
        if (GUI.Button(newRect, "Hi"))
        {
            Debug.Log(Time.time);
        }
    }
}