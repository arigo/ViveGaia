using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controls : MonoBehaviour
{
    public Transform sun;
    public Transform star_scene;
    public Material star_material;

    //VRTK_SDKManager vrtk_manager;
    PointCloud[] point_clouds;

    enum state_e { NONE, SCROLLING, LIGHTSHIFTING };
    state_e state = state_e.NONE;
    Transform action_controller;
    float initial_scale;
    float touchpad_position_y;
    /*Quaternion initial_rotation;*/

    private void Start()
    {
        star_material.SetFloat("_LightFactor", 1);
        /*
        foreach (var go in gameObject.scene.GetRootGameObjects())
        {
            if (vrtk_manager == null)
                vrtk_manager = go.GetComponent<VRTK_SDKManager>();
        }

        foreach (VRTK_ControllerEvents cev in vrtk_manager.GetComponentsInChildren<VRTK_ControllerEvents>())
        {
            cev.SubscribeToButtonAliasEvent(VRTK_ControllerEvents.ButtonAlias.Trigger_Press, true, EventClick);
            cev.SubscribeToButtonAliasEvent(VRTK_ControllerEvents.ButtonAlias.Trigger_Press, false, EventRelease);
            cev.TouchpadAxisChanged += Cev_TouchpadAxisChanged;
            cev.TouchpadTouchStart += Cev_TouchpadTouchStart;
            cev.TouchpadTouchEnd += Cev_TouchpadTouchEnd;
        }*/

        point_clouds = FindObjectsOfType<PointCloud>();
    }

#if false
    private void Cev_TouchpadTouchStart(object sender, ControllerInteractionEventArgs e)
    {
        if (state == state_e.NONE)
        {
            touchpad_position_y = e.touchpadAxis.y;
            state = state_e.LIGHTSHIFTING;
        }
    }

    private void Cev_TouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
    {
        if (state == state_e.LIGHTSHIFTING)
            state = state_e.NONE;
    }

    private void Cev_TouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        if (state == state_e.LIGHTSHIFTING)
        {
            float delta = e.touchpadAxis.y - touchpad_position_y;
            touchpad_position_y = e.touchpadAxis.y;

            float light_factor = Mathf.Pow(2f, delta);
            float prev_light = star_material.GetFloat("_LightFactor");
            star_material.SetFloat("_LightFactor", prev_light * light_factor);
        }
    }

    private void EventClick(object sender, ControllerInteractionEventArgs e)
    {
        GameObject controller = VRTK_DeviceFinder.GetControllerByIndex(e.controllerIndex, true);

        action_controller = controller.transform;
        initial_scale = star_scene.localScale.x;   /* assumed == y == z */
        initial_scale /= (action_controller.position - sun.position).magnitude;
        /*initial_rotation = star_scene.localRotation;
        initial_rotation *= Quaternion.Inverse(action_controller.rotation);*/
        state = state_e.SCROLLING;
    }

    private void EventRelease(object sender, ControllerInteractionEventArgs e)
    {
        state = state_e.NONE;
    }
#endif

private void Update()
    {
        if (state == state_e.SCROLLING)
        {
            float new_distance = (action_controller.position - sun.position).magnitude;
            star_scene.localScale = (initial_scale * new_distance) * Vector3.one;
            /*Quaternion new_rotation = action_controller.rotation;
            star_scene.localRotation = initial_rotation * new_rotation;*/
        }
    }
}
