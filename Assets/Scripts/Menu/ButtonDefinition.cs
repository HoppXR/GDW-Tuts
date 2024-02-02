using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDefinition : MonoBehaviour
{
    public bool _animated = false;
    public Color _unselectedTint = Color.grey;
    public Color _selectedTint = Color.white;
    public bool _selected = false;
    private Button _button;
    private Image _image;
    private Animator _animator;

    private bool _disableControls = false;
    
    void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        
        //Is this item animated
        _animated = TryGetComponent<Animator>(out _animator);

        if (!_animated)
        {
            if (_selected)
            {
                _image.color = _selectedTint;
            }
            else
            {
                _image.color = _unselectedTint;
            }
        }
    }

    public void SwappedTo()
    {
        _selected = true;

        //If there's an animator, update the selected bool to true
        if (_animated)
        {
            _animator.SetBool("Selected", _selected);
        }
        //If there's no animator, tint the button to show selected
        else
        {
            _image.color = _selectedTint;
        }
    }

    public void SwappedOff()
    {
        _selected = false;

        //If there's an animator, update the selected bool to false
        if (_animated)
        {
            _animator.SetBool("Selected", _selected);
        }
        //If there's no animator, tint the button to show unselected
        else
        {
            _image.color = _unselectedTint;
        }
    }

    public void ClickButton()
    {
        if (!_disableControls)
        {
            _disableControls = true;
            
            _button.onClick.Invoke();

            _disableControls = false;
        }
    }

    public bool GetDisableControls()
    {
        return _disableControls;
    }
}
