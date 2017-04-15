using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackZoomMotion : MonoBehaviour {

    public Transform starScene;
    public bool trackRotations;

    SteamVR_TrackedController controller;
    bool tracking;
    Vector3 prev_position;

	void Start()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += Controller_TriggerClicked;
        controller.TriggerUnclicked += Controller_TriggerUnclicked; ;
    }

    private void Controller_TriggerClicked(object sender, ClickedEventArgs e)
    {
        prev_position = transform.position;
        tracking = true;
    }

    private void Controller_TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        tracking = false;
    }

    void Update()
    {
		if (tracking)
        {
            Vector3 center = starScene.position;
            Vector3 new_position = transform.position;
            float factor = (new_position - center).magnitude / (prev_position - center).magnitude;
            starScene.localScale *= factor;

            if (trackRotations)
            {
                Quaternion q = Quaternion.FromToRotation(prev_position - center, new_position - center);
                starScene.localRotation *= q;
            }
            prev_position = new_position;
        }
    }
}
