using UnityEngine;
using System.Collections;

public class BulletFragmentRemover : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(remove());
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
