using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    private Coroutine waitCoroutine;
    public void Pause()
    {
        IsPaused = true;
    }

    public void Unpause()
    {
        if(waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
        }

        waitCoroutine = StartCoroutine(WaitToUnpause());
    }

    IEnumerator WaitToUnpause()
    {
        yield return new WaitForSeconds(0.1f);
        IsPaused = false;

        waitCoroutine = null;
    }
}
