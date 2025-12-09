using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    private List<ICharacterButton> characterButtons = new List<ICharacterButton>();

    void Awake()
    {
        Instance = this; 
    }

    public void Register(ICharacterButton button)
    {
        if (!characterButtons.Contains(button))
            characterButtons.Add(button);
    }

    public void OnCharacterEquipped(ICharacterButton equippedButton)
    {
        foreach (var btn in characterButtons)
        {
            if (btn != equippedButton)
                btn.SetEquipped(false);
        }
    }
}
