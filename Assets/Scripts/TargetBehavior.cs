using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;
using System;

public class TargetBehavior : MonoBehaviour, ITrackableEventHandler
{
    /** Look-3.1.b
     *  ARCamera is the main camera used by Vuforia. CameraProxy is an empty gameobject that receives gyroscope data from GyroController.
     *  We need a proxy object to handle gyro data because we cannot directly change ARCamera's transform component while Vuforia is active. 
    **/
    public GameObject ARCamera;
    public GameObject CameraProxy;
    public Button TrackButton;
    public Button ShootFrontButton;

    private GyroController CameraGyro;
    private bool tracked = true;
    private bool imageTargetTracking = true;
    private Vector3[] pos;
    private Quaternion[] rot;

    // Use this for initialization
    void Start () {
        CameraGyro = GetComponent<GyroController>();
        CameraGyro.Paused = true;
       
        Debug.Assert(CameraGyro.ControlledObject != null); 

        var mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        TrackButton.onClick.AddListener(ToggleTracking);
    }


    /** Todo-3.1.b
     *  Notice how CameraProxy copies the transform data from ARCamera when there is tracking. 
     *  The dot product ensures that when Vuforia occassionally returns glitchy data for a frame or two, the changes aren't erroneously applied.
    **/
    void LateUpdate() {
        if (tracked) {
            float dot = Vector3.Dot(ARCamera.transform.forward, CameraProxy.transform.forward);
            if (dot > 0) {
                CameraProxy.transform.position = ARCamera.transform.position;
                CameraProxy.transform.rotation = ARCamera.transform.rotation;
            }
        }
    }


    void ToggleTracking() {
        Tracker imageTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        imageTargetTracking = !imageTargetTracking;
        if (imageTargetTracking) {
            imageTracker.Start();
        }
        else {
            imageTracker.Stop();
        }
    }

    /** Look-3.1.b
        Take a look at the next 4 functions and notice how data from either ARCamera or CameraProxy is returned based on whether or not there is tracking. 
    **/ 
    public Vector3 GetPhonePosition() {
        if (tracked) {
            return ARCamera.transform.position;
        }
        else {
            return CameraProxy.transform.position;
        }
    }


    public Quaternion GetPhoneRotation() {
        if (tracked) {
            return ARCamera.transform.rotation;
        }
        else {
            return CameraProxy.transform.rotation;
        }
    }


    public Vector3 GetPhoneUp() {
        if (tracked) {
            return -ARCamera.transform.up;
        }
        else {
            Vector3 up = CameraProxy.transform.up;
            return new Vector3(-up.x, -up.z, -up.y);
        }
    }


    public Vector3 GetPhoneForward() {
        if (tracked) {
            return ARCamera.transform.forward;
        }
        else {
            return CameraProxy.transform.forward;
        }
    }


    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        switch (newStatus)
        {
		case TrackableBehaviour.Status.TRACKED:
                // Target in camera. Use the ARCamera's tracking and turn off GyroController's tracking for the CameraProxy.
                // TODO-3.1.b 

                TrackButton.image.color = Color.green;
                break;
            case TrackableBehaviour.Status.EXTENDED_TRACKED:
                // Target not in camera, but Vuforia can still calculate position and orientation and update ARCamera.
                // Use the ARCamera's tracking and turn off GyroController's tracking for the CameraProxy.
                // TODO-3.1.b

                TrackButton.image.color = Color.yellow;
                break;
            default:
                // Tracking is lost completely. Switch tracking to use the gyro-controlled CameraProxy instead. 
                // TODO-3.1.b

                TrackButton.image.color = Color.red;
                break;
        }
        
    }

}
