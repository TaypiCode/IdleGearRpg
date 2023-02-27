using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CharacterItems _characterItems;
    private Save save = new Save();

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(){
        SaveProgress();
    }
#endif
    private void OnApplicationQuit()
    {
        SaveProgress();
    }

    public void SaveProgress()
    {
        save.playerItemsItemCount = _inventory.GetPlayerItemsItemCount();
        save.playerItemsItemId = _inventory.GetPlayerItemsItemId();
        save.playerItemsItemGrade = _inventory.GetPlayerItemsGrade();
        save.characterItemsItemId = _characterItems.GetItemsId();
        save.characterItemsItemGrade = _characterItems.GetItemsGrade();

        PlayerPrefs.SetString("SV", JsonUtility.ToJson(save));
        PlayerPrefs.Save();
    }
}
[Serializable]
public class Save
{
    public int[] playerItemsItemCount;
    public int[] playerItemsItemGrade;
    public string[] playerItemsItemId;
    public string[] characterItemsItemId;
    public int[] characterItemsItemGrade;
}