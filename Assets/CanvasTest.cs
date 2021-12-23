using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTest : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 1.0f;
    }
}
