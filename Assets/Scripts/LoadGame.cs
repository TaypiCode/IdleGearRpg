using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CharacterItems _characterItems;
    private Save _save;
    private void Awake()
    {
        _save = new Save();
        if (PlayerPrefs.HasKey("SV"))
        {
            _save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            _inventory.CreateFromSave(_save.playerItemsItemId, _save.playerItemsItemGrade,_save.playerItemsItemCount);
            _characterItems.SetItemsFromSave(_save.characterItemsItemId,_save.characterItemsItemGrade);
        }
    }
}
