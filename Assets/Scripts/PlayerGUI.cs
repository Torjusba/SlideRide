using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{

    public GUIStyle barStyle = new GUIStyle();
    public Texture emptyTexture;
    public Texture fullTexture;
    public Texture dotTexture;

    private int maxHealth;
    private int currentHealth;

    bool isMenu = false;
    PlayerController pController;
    FireWeapon fWeapon;
    GameObject escMenu;

    void Start()
    {
        emptyTexture = (Texture)Resources.Load("EmptyHealthBar");
        fullTexture = (Texture)Resources.Load("FullHealthBar");
        dotTexture = (Texture)Resources.Load("Dot");

        pController = gameObject.GetComponent<PlayerController>();
        fWeapon = gameObject.GetComponent<FireWeapon>();

        escMenu = GameObject.Find("EscMenu");
        escMenu.GetComponent<Canvas>().enabled = false;
    }

    private readonly float startX = 20f;
    private readonly float startY = 10f;
    private readonly float totalSizeY = 100f;

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
        //Health Bars
        GUI.Box(new Rect(startX, startY, (float)gameObject.GetComponent<Player>().maxHealth, totalSizeY), emptyTexture, barStyle);
        GUI.Box(new Rect(startX, startY, (float)gameObject.GetComponent<Player>().currentHealth, totalSizeY), fullTexture, barStyle);

        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
        GUI.Box(new Rect(Screen.width / 2 - 10, Screen.height / 2 - 10, 20, 20), dotTexture, barStyle);
        GUI.EndGroup();
    }
}
