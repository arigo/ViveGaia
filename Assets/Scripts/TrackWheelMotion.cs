using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackWheelMotion : MonoBehaviour {

    public Material star_material;

    SteamVR_TrackedController controller;
    bool tracking;
    float prev_position;

    void Start()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        controller.PadTouched += Controller_PadTouched;
        controller.PadUntouched += Controller_PadUntouched;
    }

    private void Controller_PadTouched(object sender, ClickedEventArgs e)
    {
        prev_position = e.padY;
        tracking = true;
    }

    private void Controller_PadUntouched(object sender, ClickedEventArgs e)
    {
        tracking = false;
    }

    void Update()
    {
        if (tracking)
        {
            float new_position = controller.controllerState.rAxis0.y;
            float delta = new_position - prev_position;
            star_material.SetFloat("_LightFactor", star_material.GetFloat("_LightFactor") * Mathf.Pow(2f, delta));
            prev_position = new_position;
        }
    }
}
