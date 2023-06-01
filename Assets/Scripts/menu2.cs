using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu2 : MonoBehaviour
{
    [SerializeField]
    Transform box;
    [SerializeField]
    CanvasGroup back;

    void OnEnable()
    {
        back.alpha = 0;
        back.LeanAlpha(1, 0.5f);

        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 1f).setEaseInExpo().delay = 0.1f;
    }

    public void CloseDialog()
    {
        back.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 1f).setEaseInExpo().setOnComplete(Complete);
    }

    private void Complete()
    {
        gameObject.SetActive(false);
    }
}
