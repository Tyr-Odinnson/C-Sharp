using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectPlacerTap : MonoBehaviour
{
    public GameObject prefabPlaceable;
    public GameObject placementIndicator;
    
    private ARRaycastManager arRaycastManager;
    private Pose pose;
    private bool isPoseValid;

    // Start is called before the first frame update
    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SetPose();
        UpdatePlacementIndicator();
        if (isPoseValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Instantiate(prefabPlaceable, pose.position, pose.rotation);
        }
    }

    private void SetPose()
    {
        Vector2 screenCentre = new Vector2(Screen.width, Screen.height) / 2;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCentre, hits, TrackableType.Planes);
        hits = hits.OrderBy((h) => h.distance).ToList();

        isPoseValid = hits.Count > 0;

        if (isPoseValid)
        {
            pose = hits[0].pose;

/*            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraBearings = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            pose.rotation = Quaternion.LookRotation(cameraBearings);*/
        }
    }

    private void UpdatePlacementIndicator()
    {
        placementIndicator.SetActive(isPoseValid);
        placementIndicator.transform.SetPositionAndRotation(pose.position, pose.rotation);
    }
}
