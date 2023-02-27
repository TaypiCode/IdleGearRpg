using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillOverview : MonoBehaviour
{
    [SerializeField] private GameObject _overviewCanvas;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    private SkillScriptableObject _showingSkill = null;
    public void HideOverview()
    {
        _showingSkill = null;
        _overviewCanvas.SetActive(false);
    }
    public void ShowOverview(SkillScriptableObject skill)
    {
        _showingSkill = skill;
        _image.sprite = skill.SkillImg;
        _nameText.text = skill.SkillName;
        _descriptionText.text = skill.SkillDescription;
        _overviewCanvas.SetActive(true);
    }
    public void CheckForHideBeforeDestroy(SkillScriptableObject skill)
    {
        if(_showingSkill == skill)
        {
            HideOverview();
        }
    }
}
