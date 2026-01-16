using UnityEngine;

public class Character_01 : CharacterButtonBase
{
    protected override void Start()
    {
        characterID = 0;
        isFree = true;
        price = 0;
        base.Start();
    }
}

