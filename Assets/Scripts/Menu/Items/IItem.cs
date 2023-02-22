using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IItem
{
    void Set(ItemScriptableObject item, Item parent);
    void Use();
    void Select();

}
