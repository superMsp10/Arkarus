using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class StartMenu : MonoBehaviour
{
    public string PlaySceneName;
    public TextMeshProUGUI text;
    public Color color1, color2;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        text.faceColor = Color.Lerp(color1, color2, Mathf.Abs(Mathf.Sin(Time.time/2)));
        text.outlineColor = Color.Lerp(color2, color1, Mathf.Abs(Mathf.Sin(Time.time/2)));

    }

    public void StartPlayScene()
    {
        SceneManager.LoadScene(PlaySceneName);
    }
}
