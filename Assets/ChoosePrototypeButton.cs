using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePrototypeButton : MonoBehaviour
{
    public enum PrototypeType { Weapon, Aerial, None }
    public PrototypeType prototypeType;

    public List<PrototypeChoice> prototypeChoices;
    private List<GameObject> allChoiceButtons;

    private Vector3 buttonPosition;
    public float spacingBetweenButtons;

    public Text nameText;

    public GameObject prototypeChoiceButtonPrefab;

    public ModuleManager moduleManager;
    public int targetPrototypeIndex;

    [System.Serializable]
    public class PrototypeChoice
    {
        public PrototypeType type;
        public string name;
        public Sprite sprite;
        public WeaponData weaponData;
        public IState aerialState;
    }

    public void Start()
    {
        buttonPosition = transform.position;
        allChoiceButtons = new List<GameObject>();
        foreach (PrototypeChoice choice in prototypeChoices)
        {
            buttonPosition.y = buttonPosition.y + spacingBetweenButtons;

            GameObject newChoiseButton = Instantiate(prototypeChoiceButtonPrefab, buttonPosition, Quaternion.identity);

            PrototypeChoiceButton button = newChoiseButton.GetComponent<PrototypeChoiceButton>();

            button.type = choice.type;
            button.name = choice.name;
            button.sprite = choice.sprite;
            button.weaponData = choice.weaponData;
            button.nameText.text = choice.name;


            if (choice.type == PrototypeType.Aerial)
            {
                if (choice.name == "NegativeGravity")
                {
                    choice.aerialState = moduleManager.NegativeGravityState();
                }
                if (choice.name == "ReversedDashing")
                {
                    choice.aerialState = moduleManager.ReversedDashingState();
                }
                if (choice.name == "Dashing")
                {
                    choice.aerialState = moduleManager.DashingState();
                }
                if (choice.name == "Hovering")
                {
                    choice.aerialState = moduleManager.HoveringState();
                }
                if (choice.name == "JetPackState")
                {
                    choice.aerialState = moduleManager.JetPackState();
                }
            }

            button.prototypeChoice = choice;

            newChoiseButton.transform.SetParent(transform);
            newChoiseButton.SetActive(false);
            allChoiceButtons.Add(newChoiseButton);
        }

        nameText.text = prototypeType.ToString();
    }

    public void TogglePrototypeChoices()
    {
        foreach (GameObject choice in allChoiceButtons)
        {
            if (choice.activeInHierarchy) choice.SetActive(false);
            else choice.SetActive(true);
        }
    }

    public void NewChosenPrototypeChoice(PrototypeChoice chosenPrototype)
    {
        nameText.text = chosenPrototype.name;
        TogglePrototypeChoices();

        //WeaponData newWeaponData = new WeaponData();
        //newWeaponData = chosenPrototype.weaponData;
        switch (chosenPrototype.type)
        {
            case PrototypeType.Weapon:
                Debug.Log(chosenPrototype.weaponData);
                moduleManager.prototypeWeaponDatas[targetPrototypeIndex] = chosenPrototype.weaponData;
                break;
            case PrototypeType.Aerial:
                Debug.Log("chosenPrototype.aeiralState = " + chosenPrototype.aerialState);
                if (targetPrototypeIndex == 0)
                {
                    moduleManager.aerialTechLevelOneState = chosenPrototype.aerialState;
                }
                else if (targetPrototypeIndex == 1)
                {
                    moduleManager.aerialTechLevelTwoState = chosenPrototype.aerialState;
                }
                else if (targetPrototypeIndex == 2)
                {
                    moduleManager.aerialTechLevelThreeState = chosenPrototype.aerialState;
                }
                else
                {
                    Debug.LogWarning("Aerial State Prototype wasn't properly created. targetPrototypeIndex or name is incorrect");
                }
                break;
            case PrototypeType.None:
                break;
            default:
                break;
        }
    }
}
