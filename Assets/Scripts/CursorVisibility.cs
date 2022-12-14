using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorVisibility : MonoBehaviour
{
    private void OnEnable()
    {
        if (!Application.isPlaying) { return; }
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        if (!Application.isPlaying) { return; }
        Cursor.visible = false;
    }

    private void Update()
    {
        Cursor.visible = GetComponent<Image>().enabled;
    }
}
