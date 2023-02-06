using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTextObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _force;
    [SerializeField] private float _showTime;
    [SerializeField] private float _animSpeed;

    private Timer _timer;
    private bool _needAnim;
    private float _currentScale;
    private Vector3 _endAnimScale = new Vector3(0, 0, 1);
    private void Awake()
    {
        _timer = this.gameObject.AddComponent<Timer>();
    }
    private void LateUpdate()
    {
        _text.enabled = IsShowing();
        if (_needAnim && IsShowing())
        {
            _currentScale = Mathf.Lerp(_currentScale, 0, _animSpeed * Time.deltaTime);
            _rectTransform.localScale = new Vector3(_currentScale, _currentScale, 1);
            if (_rectTransform.localScale == _endAnimScale)
            {
                _needAnim = false;
            }
        }
    }
    public void ShowText(string val, Vector3 pos)
    {
        _rectTransform.position = pos;
        _timer.SetTimer(_showTime);
        _text.enabled = true;
        _text.text = val;
        float x = Random.Range(-60, 60);
        float y = Random.Range(40, 80);
        _rb.velocity = Vector2.zero;
        _rb.AddForce(_rectTransform.right * x * _force + _rectTransform.up * y * _force, ForceMode2D.Impulse);
        _rectTransform.localScale = Vector3.one;
        _currentScale = 1;
        _needAnim = true;
    }
    public bool IsShowing()
    {
        return _timer.IsWorking();
    }
}
