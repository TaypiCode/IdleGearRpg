using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Promocode : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TextMeshProUGUI _answerText;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private List<PromocodeScriptable> _promocodes;
    private string _enteredCodeText;
    private List<string> _activatedPromocodes = new List<string>();
    private PromocodeScriptable _enteredPromocode;
    private Save save;
    private bool _activatedPerSession = false;
    private void Awake()
    {
        save = new Save();
        if (PlayerPrefs.HasKey("SV"))//save
        {
            save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            if (save.activatedPromocodes != null)
            {
                _activatedPromocodes.AddRange(save.activatedPromocodes);
            }
        }
    }
    public void ShowCanvas()
    {
        _canvas.SetActive(true);
        _enteredCodeText = "";
        _answerText.text = "";
    }
    public void TryAcceptPromocode()
    {
        if (GetEnteredPromocode())
        {
            if (CanActivate() && _activatedPerSession == false)
            {
                switch (_enteredPromocode.GetRewardType)
                {
                    case PromocodeScriptable.RewardType.Item:
                        ItemScriptableObject item = _enteredPromocode.RewardItem;
                        _answerText.text = "Получен предмет : " + item.ItemName + ", в количестве " + _enteredPromocode.Count + " шт";
                        _inventory.CreateItem(item, item.StartGrade, 0, _enteredPromocode.Count);
                        break;
                    default:
                        break;
                }
                _activatedPromocodes.Add(_enteredPromocode.itemId);
                _activatedPerSession = true;
            }
            else
            {
                _answerText.text = "Промокод уже активирован";
            }
        }
        else
        {
            _answerText.text = "Не верный промокод";
        }
    }
    private bool GetEnteredPromocode()
    {
        
        for (int i = 0; i < _promocodes.Count; i++)
        {
            if (_promocodes[i].Code == _enteredCodeText)
            {
                _enteredPromocode = _promocodes[i];
                return true;
            }
        }
        return false;
    }
    private bool CanActivate()
    {
        for (int i = 0; i < _activatedPromocodes.Count; i++)
        {
            if (_activatedPromocodes[i] == _enteredPromocode.itemId)
            {
                return false;
            }
        }
        return true;
    }
    public void FillEnterCodeText(string s)
    {
        _enteredCodeText = s;
    }
    public string[] GetActivatedPromocodes()
    {
        return _activatedPromocodes.ToArray();
    }
}
