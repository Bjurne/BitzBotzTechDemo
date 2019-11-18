using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrototypeChoiceButton : MonoBehaviour
{
    public ChoosePrototypeButton.PrototypeType type;
    public new string name;
    public Sprite sprite;
    public WeaponData weaponData;
    public IState aerialState;

    public Text nameText;

    public ChoosePrototypeButton.PrototypeChoice prototypeChoice;

    private ChoosePrototypeButton prototypeButton;
    
    public void PrototypeChosen()
    {
        prototypeButton = GetComponentInParent<ChoosePrototypeButton>();

        if (prototypeButton != null)
        {
            prototypeButton.NewChosenPrototypeChoice(prototypeChoice);
        }
        else
        {
            Debug.LogWarning("Prototype choice didnt resolve properly");
        }
    }
}
