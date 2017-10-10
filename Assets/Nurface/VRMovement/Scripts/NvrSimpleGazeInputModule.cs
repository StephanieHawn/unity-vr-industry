using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.VR;
// To use:
// 1. Drag onto your EventSystem game object.
// 2. Move above any other Input Modules (eg: StandaloneInputModule & TouchInputModule) as they will fight over selections.
public class NvrSimpleGazeInputModule : PointerInputModule {
    public RaycastResult CurrentRaycast;
    private PointerEventData pointerEventData;
    private GameObject currentLookAtHandler;
    // Center of screen point. This is different for GoogleVR and Native Unity VR modes
    private Vector2 centerOfScreen;

    protected override void Start() {
        // Set the CenterOfScreen var
        SetCenterOfScreen();
    }

    public override void Process() {
        HandleLook();
        HandleSelection();
    }

    void HandleLook() {
        if (pointerEventData == null) {
            pointerEventData = new PointerEventData(eventSystem);
        }
        // fake a pointer always being at the center of the screen
        pointerEventData.position = centerOfScreen;
        pointerEventData.delta = Vector2.zero;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, raycastResults);
        CurrentRaycast = pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);
        ProcessMove(pointerEventData);
    }

    void HandleSelection() {
        if (pointerEventData.pointerEnter != null) {
            // if the ui receiver has changed, reset the gaze delay timer
            GameObject handler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointerEventData.pointerEnter);
            if (currentLookAtHandler != handler) {
                currentLookAtHandler = handler;
            }

            // if we have a handler and it's time to click, do it now
            if (currentLookAtHandler != null && (Input.GetButtonDown("Fire1"))) {
                ExecuteEvents.ExecuteHierarchy(currentLookAtHandler, pointerEventData, ExecuteEvents.pointerDownHandler);
                ExecuteEvents.ExecuteHierarchy(currentLookAtHandler, pointerEventData, ExecuteEvents.pointerUpHandler);
                ExecuteEvents.ExecuteHierarchy(currentLookAtHandler, pointerEventData, ExecuteEvents.pointerClickHandler);
            }
        }
        else {
            currentLookAtHandler = null;
        }
    }

    public void SetCenterOfScreen() {
#if UNITY_5_5_2
        centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
#else
        // GearVR, or native Daydream/cardboard (pre 5.5.2)
        if (VRSettings.enabled == true) {
            // Center of screen is center of eye texture width
            centerOfScreen = new Vector2((float)VRSettings.eyeTextureWidth / 2, (float)VRSettings.eyeTextureHeight / 2);
        }
        // VR support is not on (Using Cardboard SDK)
        else {
            // Get center of screen the normal way
            centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        }
#endif
    }
}