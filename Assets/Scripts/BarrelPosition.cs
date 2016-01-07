using UnityEngine;

public class BarrelPosition : MonoBehaviour {

    [SerializeField]
    Camera playerCam;

	void Start () {
        gameObject.GetComponent<Transform>().forward = playerCam.GetComponent<Transform>().forward;
	}
}
