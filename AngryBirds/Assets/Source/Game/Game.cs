using UnityEngine;

public class Game : MonoBehaviour
{
    public static ControlType currentControlType;

    public Transform spawnPoint;

    Control currentControl;
    GameObject objectToPlace;
    public void Awake()
    {
        if(currentControlType == ControlType.AR)
        {
            currentControl = Instantiate(Resources.Load<Control>("Controls/ARControl"));
        }
        else if(currentControlType == ControlType.VR)
        {
            currentControl = Instantiate(Resources.Load<Control>("Controls/VRControl"));
        }
        currentControl.transform.SetParent(spawnPoint);
        currentControl.transform.localPosition = Vector3.zero;
        currentControl.transform.localRotation = Quaternion.identity;
        currentControl.setIndicator(Instantiate(Resources.Load<GameObject>("PlacementIndicator")));

        objectToPlace = Resources.Load<GameObject>("GamePiece");

        initTools();
    }

    public void Update()
    {
        var placementPose = currentControl.getPlacementPose();
        if(placementPose != null)
        {
            Instantiate(objectToPlace, placementPose.pose.position, placementPose.pose.rotation);
        }
    }

    void initTools()
    {
        var debugControls = gameObject.AddComponent<VREditorControls>();
        debugControls.target = currentControl.transform;
    }
}

public enum ControlType
{
    VR,
    AR,
}