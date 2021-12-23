using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Action : MonoBehaviour
{
    public static IEnumerator DelayedAction(float delay, UnityAction action)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Wtf");
        action?.Invoke();
    }
}
