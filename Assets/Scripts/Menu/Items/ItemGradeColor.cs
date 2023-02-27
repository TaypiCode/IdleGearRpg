using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGradeColor : MonoBehaviour
{
    [SerializeField] private Color[] _color;
    private static Color[] _staticColor;
    private void Awake()
    {
        _staticColor = _color;
    }


    public static Color GetGradeColor(int grade)
    {
        if(grade > 0)
            return _staticColor[grade - 1];
        Debug.Log("Grade color out of range");
        return Color.white;
    }
}
