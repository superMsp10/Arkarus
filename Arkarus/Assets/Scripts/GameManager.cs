using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif



public class GameManager : MonoBehaviour
{

    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    public GameObject scanGround;
    public Transform floorCollider;
    private bool m_IsQuitting = false;
    bool setupBase = true;
    public Camera FirstPersonCamera;

    public GameObject shootObject;
    public float shootPower = 10f;
    Vector2 startPos, endPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //_UpdateApplicationLifecycle();

        //if (setupBase)
        //{
        //    //UpdateARMeshes();
        //}
        //else
        //{
            UpdateShoot();
        //}
    }

    void UpdateShoot()
    {
        Touch touch;
        if (Input.touchCount < 1)
        {
            return;
        }

        touch = Input.GetTouch(0);
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            endPos = touch.position;

            Vector3 diff = (endPos - startPos);
            float distance = Vector2.Distance(diff , center);
            if (diff.x > 0 && diff.y < 0)
            {
                Debug.Log(distance);

                Vector3 force = FirstPersonCamera.transform.forward.normalized * shootPower * distance;
                GameObject g = Instantiate(shootObject, FirstPersonCamera.transform.position + FirstPersonCamera.transform.forward / 4, Quaternion.identity, null);
                g.transform.parent = transform;
                g.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
            }

           

        }
    }

    void UpdateARMeshes()
    {
        // Hide snackbar when currently tracking at least one plane.
        Session.GetTrackables<DetectedPlane>(m_AllPlanes);
        bool showSearchingUI = true;
        for (int i = 0; i < m_AllPlanes.Count; i++)
        {
            if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
            {
                showSearchingUI = false;
                if (m_AllPlanes[i].CenterPose.up == Vector3.up)
                {
                    if (m_AllPlanes[i].CenterPose.position.y < floorCollider.position.y)
                    {
                        floorCollider.position = new Vector3(0, m_AllPlanes[i].CenterPose.position.y, 0);
                    }
                }
                break;
            }
        }
        if (!showSearchingUI)
        {
            scanGround.SetActive(false);
            SetupBase();
        }
    }

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
