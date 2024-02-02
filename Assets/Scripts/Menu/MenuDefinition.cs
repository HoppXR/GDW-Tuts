using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuType
{
    HORIZONTAL,
    VERTICAL,
}

public class MenuDefinition : MonoBehaviour
{
    public MenuType _menuType = MenuType.HORIZONTAL;
    public List<GameObject> _menuButtonObjects = new List<GameObject>();

    private List<ButtonDefinition> _menuButtonDefinitions = new List<ButtonDefinition>();
    private List<Button> _menuButtons = new List<Button>();
    private List<Animator> _menuAnimators = new List<Animator>();

    public void Start()
    {
        //Searches and grabs components
        for (int i = 0 ; i < _menuButtonObjects.Count ; i++)
        {
            _menuButtonDefinitions.Add(_menuButtonObjects[i].GetComponent<ButtonDefinition>());
            _menuButtons.Add(_menuButtonObjects[i].GetComponent<Button>());
            
            //Grab out animator component if it exists
            Animator temp = null;
            _menuButtonObjects[i].TryGetComponent(out temp);
            
            //If there's no animator it'll be null
            //We'll check for null when we use the animator
            _menuAnimators.Add(temp);
        }
    }

    public MenuType GetMenuType()
    {
        return _menuType;
    }

    public int GetButtonCount()
    {
        return _menuButtonObjects.Count;
    }

    public List<ButtonDefinition> GetButtonDefinitions()
    {
        return _menuButtonDefinitions;
    }

    public List<Button> GetButtons()
    {
        return _menuButtons;
    }

    public List<Animator> GetAnimators()
    {
        return _menuAnimators;
    }
}
