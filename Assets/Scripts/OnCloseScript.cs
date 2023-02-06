using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCloseScript : MonoBehaviour
{
    [SerializeField] private bool _needSave;
    public void OnClose()
    {
        if(_needSave) FindObjectOfType<SaveGame>().SaveProgress();
    }
}
