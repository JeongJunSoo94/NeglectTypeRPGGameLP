using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUI : MonoBehaviour
{
    Camera cameraMain;
    protected Ray ray;
    protected RaycastHit hit;
    public LayerMask mouseSideLayer;

    private void Awake()
    {
        cameraMain = Camera.main;
    }

    public virtual void TouchDragUpdate(ref Vector3 vec)
    {
        ray = cameraMain.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f, mouseSideLayer, QueryTriggerInteraction.Ignore))
        {
            vec = hit.point;
        }
    }
}
