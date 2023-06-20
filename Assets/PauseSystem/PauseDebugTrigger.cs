
using System;
using UnityEngine;

public class PauseDebugTrigger : MonoBehaviour
{
    private Guid? pkey;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pkey == null) pkey = PauseManager.Pause();
            else
            {
                PauseManager.Resume(pkey.Value);
                pkey = null;
            }
        }
    }
}
