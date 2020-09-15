using UnityEngine;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        Debug.Log("Found.");
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        Debug.Log("Lost.");
    }
}