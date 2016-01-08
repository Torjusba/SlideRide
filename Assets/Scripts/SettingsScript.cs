using UnityEngine;
using System.Collections;

public class SettingsScript : MonoBehaviour
{

    [SerializeField]
    private int startPerspective = 1;

    [SerializeField]
    private float startSensitivity = 3f;

    void Awake()
    {
        //Camera perspective
        if (!PlayerPrefs.HasKey("Perspective"))
        {
            PlayerPrefs.SetInt("Perspective", startPerspective);
        }

        if (!PlayerPrefs.HasKey("LookSensitivity"))
        {
            PlayerPrefs.SetFloat("LookSensitivity", startSensitivity);
        }

    }
}
