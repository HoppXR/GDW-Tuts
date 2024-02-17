using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class NumberDisplayDefinition : MonoBehaviour
{
    public string _numericValue = "";
    private string _oldNumericValue = "";
    public bool _enableConversion = false;
    private bool _converted = false;

    public int _numDigits = 1;
    public GameObject _defaultObj;
    public Sprite _noNumberImage;

    public int _spacePadding = 5;
    private int _lastValuePadding = 5;

    public ScriptableNumberFont _numberSprites;

    public void CreateNewDigits()
    {
        _converted = false;

        for (int i = transform.childCount; i < _numDigits; i++)
        {
            //Add the digits as children of the number display object
            GameObject temp = Instantiate(_defaultObj, transform);
            temp.GetComponent <Image>().sprite = _noNumberImage;
        }

        UpdateSpacing();
    }

    public void UpdateSpacing()
    {
        for (int i = 0; i < _numDigits; i++)
        {
            GameObject temp = transform.GetChild(i).gameObject;
            RectTransform pos = temp.GetComponent <RectTransform>();
            pos.localPosition = new Vector3(i * (pos.sizeDelta.x * _spacePadding), 0, pos.localPosition.z);
        }
    }

    public void DeleteOldDigits()
    {
        _converted = false;

        for (int i = transform.childCount - 1; i >= _numDigits; i++)
        {
            //Remove the children from bottom
            DestroyImmediate(transform.GetChild(i).gameObject.GetComponent<Image>());
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    public void ConvertToSprites()
    {
        string splitVal = _numericValue;
        if (splitVal.Length > _numDigits)
        {
            Debug.LogWarning("Your number is longer that number of digits, increasing digits");
            _numDigits = splitVal.Length;
            CreateNewDigits();
        }
        else if (splitVal.Length < _numDigits)
        {
            Debug.LogWarning("Your number is shorter than the number of digits, decreasing digits");
            _numDigits = splitVal.Length;
            DeleteOldDigits();
        }

        for (int i = 0; i < _numDigits; i++)
        {
            //Update our digits to reflect the numbers
            transform.GetChild(i).GetComponent<Image>().sprite = _numberSprites._numberSprites[int.Parse(splitVal[i].ToString())];
        }

        _converted = true;
    }

    public void LockVariables()
    {
        if (_enableConversion)
        {
            _numDigits = _numericValue.Length;
        }
    }

    private void Update()
    {
        if (!_converted && _enableConversion)
        {
            ConvertToSprites();
        }
        
        LockVariables();

        if (gameObject.transform.childCount < _numDigits)
        {
            CreateNewDigits();
        }
        else if (gameObject.transform.childCount > _numDigits)
        {
            DeleteOldDigits();
        }

        if (_numericValue != _oldNumericValue)
        {
            _converted = false;
        }

        if (_spacePadding != _lastValuePadding)
        {
            UpdateSpacing();
        }


        _oldNumericValue = _numericValue;
        _lastValuePadding = _spacePadding;
    }
}
