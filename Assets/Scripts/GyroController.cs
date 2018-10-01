using UnityEngine;
using System.Collections;

public class GyroController : MonoBehaviour
{
    public GameObject ControlledObject
    {
        get { return controlledObject; }
        set
        {
            controlledObject = value;
            ResetOrientation();
        }
    }

    public bool Paused { get; set; }

    Quaternion qRefObject = Quaternion.identity;
    Quaternion qRefGyro = Quaternion.identity;
	Quaternion qRefGyroLeft = Quaternion.identity;
    Gyroscope gyro;

    GameObject controlledObject;

    void Awake()
    {
        Paused = false;
    }

    // Use this for initialization
    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
        gyro.updateInterval = 0.01f;
    }

    void OnGUI()
    {
        //GUILayout.Label("Gyroscope attitude : " + gyro.attitude);
        //GUILayout.Label("Gyroscope gravity : " + gyro.gravity);
        //GUILayout.Label("Gyroscope rotationRate : " + gyro.rotationRate);
        //GUILayout.Label("Gyroscope rotationRateUnbiased : " + gyro.rotationRateUnbiased);
        //GUILayout.Label("Gyroscope updateInterval : " + gyro.updateInterval);
        //GUILayout.Label("Gyroscope userAcceleration : " + gyro.userAcceleration);
        //GUILayout.Label("Ref camera rotation:" + qRefObject);
        //GUILayout.Label("Ref gyro attitude:" + qRefGyro);
    }

    // LOOK-1.d:
    // Converts the data returned from gyro from right-handed base to left-handed base.
    // Your device may require a different conversion
    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


    // Update is called once per frame
    void Update()
    {
        if (controlledObject != null && !Paused)
        {
            // TODO-1.d & TODO-2.a:
            //   rotate the camera or cube based on qRefObject, qRefGyro and current 
            //   data from gyroscope
        }
    }

    public void ResetOrientation()
    {
        if (controlledObject == null)
        {
            return;
        }
        qRefObject = controlledObject.transform.rotation;
        qRefGyro = ConvertRotation(Input.gyro.attitude);
		qRefGyroLeft = Input.gyro.attitude;
    }

    //// Possible helper function to smooth between gyro and Vuforia
    //public void UpdateOrientation(float deltatime)
    //{
    //        float smooth = 1f;
    //        qRefCam = Quaternion.Slerp(qRefCam, transform.rotation, smooth * deltatime);
    //        qRefGyro = Quaternion.Slerp(qRefGyro, ConvertRotation(gyro.attitude), smooth * deltatime);
    //    }
    //}

}
