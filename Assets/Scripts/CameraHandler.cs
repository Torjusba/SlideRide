using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
    int currentPerspective;
    Vector3 thirdPersonPos;
    Vector3 firstPersonPos;
    [SerializeField]
    Vector3 targetPos;
    Camera cam;

    void Start()
    {
        currentPerspective = PlayerPrefs.GetInt("Perspective");
        thirdPersonPos = new Vector3(0f, 1.5f, -5.5f);
        firstPersonPos = new Vector3(0f, 0f, 0f);
        cam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (currentPerspective != PlayerPrefs.GetInt("Perspective"))
        {
            currentPerspective = PlayerPrefs.GetInt("Perspective");
            switch (currentPerspective)
            {
                case 1:
                    targetPos = firstPersonPos;
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer("LocalPlayer"));
                    break;
                case 3:
                    targetPos = thirdPersonPos;
                    cam.cullingMask |= 1 << LayerMask.NameToLayer("LocalPlayer");
                    break;
            }

            StartCoroutine(ChangePerspective());
        }
    }

    IEnumerator ChangePerspective()
    {
        while (gameObject.GetComponent<Transform>().localPosition != targetPos)
        {
            gameObject.GetComponent<Transform>().localPosition = Vector3.Lerp(gameObject.GetComponent<Transform>().localPosition, targetPos, 5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
