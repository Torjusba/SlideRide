using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillMessageScript : MonoBehaviour {
    
    public Player owner;
    public Player target;

	void Start () {
        Text killMessage = gameObject.GetComponentInChildren<Text>();
        killMessage.text = owner.name + " has killed " + target.name;

        StartCoroutine(DeleteTimer());
	}	

    IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);

    }
}
