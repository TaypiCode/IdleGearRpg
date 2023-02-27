using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _durationText;
    private BuffSkillScriptable _buffScriptable;
    private Timer _durationTimer;
    public BuffSkillScriptable BuffScriptable { get => _buffScriptable; }
    private SkillOverview _skillOverview;
    private void Start()
    {
        _skillOverview = FindObjectOfType<SkillOverview>();
    }
    private void FixedUpdate()
    {
        UpdateDurationText(_durationTimer.GetTime());
        if (_durationTimer.IsWorking() == false)
        {
            Remove();
        }
    }
    public void Set(BuffSkillScriptable buffScriptable)
    {
        _buffScriptable = buffScriptable;
        if(_durationTimer!= null)
        {
            Destroy(_durationTimer);
        }
        _durationTimer = gameObject.AddComponent<Timer>();
        _durationTimer.SetTimer(_buffScriptable.Duration);
        UpdateDurationText(_buffScriptable.Duration);
    }
    private void UpdateDurationText(float val)
    {
        _durationText.text = StringConverter.ConvertToFormat(val);
    }
    public void Remove()
    {
        _skillOverview.CheckForHideBeforeDestroy(_buffScriptable);
        Destroy(this.gameObject);
    }
    public void ShowOverview()
    {
        _skillOverview.ShowOverview(_buffScriptable);
    }
    public void HideOverview()
    {
        _skillOverview.HideOverview();
    }
}
