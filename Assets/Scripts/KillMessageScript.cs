using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillMessageScript : MonoBehaviour
{

    public string owner = "2312";
    public string target = "btis";

    Text killMessage;

    void Start()
    {
        Destroy(gameObject, 3f);

        killMessage = gameObject.GetComponentInChildren<Text>();
        killMessage.text = owner + " has killed " + target;
    }

    void Update()
    {
        killMessage.text = owner + " has killed " + target;
    }
}
