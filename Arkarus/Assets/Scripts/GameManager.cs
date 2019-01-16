using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    //public GameObject scanGround;
    //public Transform floorCollider;
    private bool m_IsQuitting = false;
    bool setupBase = true;
    public Camera FirstPersonCamera;
    public GhostSpawner spawner;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //_UpdateApplicationLifecycle();
    }



    //void UpdateARMeshes()
    //{
    //    // Hide snackbar when currently tracking at least one plane.
    //    Session.GetTrackables<DetectedPlane>(m_AllPlanes);
    //    bool showSearchingUI = true;
    //    for (int i = 0; i < m_AllPlanes.Count; i++)
    //    {
    //        if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
    //        {
    //            showSearchingUI = false;
    //            if (m_AllPlanes[i].CenterPose.up == Vector3.up)
    //            {
    //                if (m_AllPlanes[i].CenterPose.position.y < floorCollider.position.y)
    //                {
    //                    floorCollider.position = new Vector3(0, m_AllPlanes[i].CenterPose.position.y, 0);
    //                }
    //            }
    //            break;
    //        }
    //    }
    //    if (!showSearchingUI)
    //    {
    //        scanGround.SetActive(false);
    //        SetupBase();
    //    }
    //}

    void SetupBase()
    {
        setupBase = false;

    }

    private void _UpdateApplicationLifecycle()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            const int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (m_IsQuitting)
        {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }

    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, 0);
                toastObject.Call("show");
            }));
        }
    }


    /// <summary>
    /// Actually quit the application.
    /// </summary>
    private void _DoQuit()
    {
        Application.Quit();
    }
}
