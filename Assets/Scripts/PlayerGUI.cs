using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{

    public GUIStyle dotStyle = new GUIStyle();
    public Texture dotTexture;

    bool isMenu = false;

    PlayerController pController;
    FireWeapon fWeapon;
    GameObject escMenu;

    void Start()
    {
        dotTexture = (Texture)Resources.Load("Dot");

        pController = gameObject.GetComponent<PlayerController>();
        fWeapon = gameObject.GetComponent<FireWeapon>();

        escMenu = GameObject.Find("EscMenu");
        escMenu.GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (!isMenu)
            {
                //Enable menu
                escMenu.GetComponent<Canvas>().enabled = true;
                isMenu = true;

                //Disable player controls
                pController.enabled = false;
                fWeapon.enabled = false;

                //Enable cursor
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                //Disable menu
                escMenu.GetComponent<Canvas>().enabled = false;
                isMenu = false;

                //Enable player controls
                pController.enabled = true;
                fWeapon.enabled = true;

                //Disable cursor
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }


    void OnGUI()
    {
        //Crosshair
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
        GUI.Box(new Rect(Screen.width / 2 - 10, Screen.height / 2 - 10, 20, 20), dotTexture, dotStyle);
        GUI.EndGroup();
    }
}
