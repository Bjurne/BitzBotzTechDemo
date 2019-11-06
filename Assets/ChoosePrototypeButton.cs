using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePrototypeButton : MonoBehaviour
{
    public enum PrototypeType { Weapon, Aerial, None }
    public PrototypeType prototypeType;

    public List<PrototypeChoice> prototypeChoices;
    private List<GameObject> allChoiceButtons;

    private Vector3 buttonPosition;
    public float spacingBetweenButtons;

    public GameObject prototypeChoiceButtonPrefab;

    [System.Serializable]
    public class PrototypeChoice
    {
        public PrototypeType type;
        public string name;
        public Sprite sprite;
    }

    public void Start()
    {
        allChoiceButtons = new List<GameObject>();
        foreach (PrototypeChoice choice in prototypeChoices)
        {
            buttonPosition.y = buttonPosition.y + spacingBetweenButtons;

            GameObject newChoiseButton = Instantiate(prototypeChoiceButtonPrefab, buttonPosition, Quaternion.identity);

            PrototypeChoiceButton button = newChoiseButton.GetComponent<PrototypeChoiceButton>();

            button.type = choice.type;
            button.name = choice.name;
            button.sprite = choice.sprite;

            newChoiseButton.transform.SetParent(transform);
            newChoiseButton.SetActive(false);
            allChoiceButtons.Add(newChoiseButton);
        }
    }

    public void TogglePrototypeChoices()
    {
        foreach (GameObject choice in allChoiceButtons)
        {
            if (choice.activeInHierarchy) choice.SetActive(false);
            else choice.SetActive(true);
        }
    }
}
