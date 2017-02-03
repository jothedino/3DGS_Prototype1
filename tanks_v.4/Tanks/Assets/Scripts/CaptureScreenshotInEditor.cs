using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CaptureScreenshotInEditor : MonoBehaviour
{

    private string fileName;
    public KeyCode key;

    void LateUpdate()
    {
        if (Input.GetKeyDown(key))
            Capture();
    }

    public void Capture()
    {
        fileName = "Screenshot_" + System.DateTime.Now.ToString("_hh-mm-ss_yyyy-MM-dd") + ".png";
        Application.CaptureScreenshot(fileName);
        Debug.Log("Screenshot captured: " + Application.dataPath + "/" + fileName);
    }
}
