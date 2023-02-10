using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Canvas _thisCanvas;
    protected void HideItem()
    {
        _thisCanvas.enabled = false;
    }
    protected void ShowItem()
    {
        _thisCanvas.enabled = true;
    }
}
