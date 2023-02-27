using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [SerializeField] private GameObject _cooldownCanvas;
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private TextMeshProUGUI _cooldownText;
    public void StartCooldown(float time)
    {
        _cooldownSlider.maxValue = time;
        SetCooldownTime(time);
    }
    public void SetCooldownTime(float val)
    {
        if(val > 0)
        {
            _cooldownCanvas.SetActive(true);
            _cooldownSlider.value = val;
            _cooldownText.text = StringConverter.ConvertToFormat(val);
        }
        else
        {
            _cooldownCanvas.SetActive(false);
            return;
        }
    }
}
