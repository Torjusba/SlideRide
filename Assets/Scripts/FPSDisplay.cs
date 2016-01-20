using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float avgTime = 0.0f;

    void Update()
    {
        avgTime += (Time.deltaTime - avgTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        float fps = 1.0f / avgTime;
        string text = string.Format("({0:F1} fps)", fps);

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;

        if (fps < 30)
            style.normal.textColor = Color.yellow;
        else if (fps < 10)
            style.normal.textColor = Color.red;
        else
            style.normal.textColor = Color.white;

        GUI.Label(rect, text, style);
    }
}