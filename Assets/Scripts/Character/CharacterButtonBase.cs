using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButtonBase : MonoBehaviour, ICharacterButton
{
    [Header("UI Components")]
    public Button Equipped;
    public TMP_Text TextPro;

    [Header("NoticeCanvas")]
    public GameObject noticeCanvas;

    [HideInInspector]
    public int price = 0; 
    public bool isFree = false;

    protected bool isEquipped = false;
    protected bool isPurchased = false;

   protected virtual void Start()
{
    CharacterManager.Instance.Register(this);
    Equipped.onClick.AddListener(OnClick);

    if (isFree)
    {
        price = 0; 
        isPurchased = true;

        if (!PlayerPrefs.HasKey($"{gameObject.name}_Equipped"))
        {
            isEquipped = true; 
        }
        else
        {
            isEquipped = PlayerPrefs.GetInt($"{gameObject.name}_Equipped", 0) == 1;
        }
    }
    else
    {
        isPurchased = PlayerPrefs.GetInt($"{gameObject.name}_Purchased", 0) == 1;
        isEquipped = PlayerPrefs.GetInt($"{gameObject.name}_Equipped", 0) == 1;
    }

    if (isEquipped)
        CharacterManager.Instance.OnCharacterEquipped(this);

    UpdateButton();
}


    protected void OnClick()
    {
        if (!isPurchased && price > 0)
        {
            BuyCharacter();
        }
        else
        {
            ToggleEquip();
        }
    }

    protected void BuyCharacter()
    {
        if (isFree || CoinManager.Instance.SpendCoin(price))
        {
            isPurchased = true;
            EquipCharacter();
        }
        else
        {
            if (noticeCanvas != null)
                noticeCanvas.SetActive(true);
        }
    }

    protected void ToggleEquip()
    {
        if (!isPurchased) return;

        if (!isEquipped)
            EquipCharacter();
        else
            SetEquipped(false);
    }

    public void EquipCharacter()
    {
        if (!isPurchased) return;

        isEquipped = true;
        CharacterManager.Instance.OnCharacterEquipped(this);
        SaveState();
        UpdateButton();
    }

    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
        SaveState();
        UpdateButton();
    }

    protected void UpdateButton()
    {
        Image img = Equipped.GetComponent<Image>();

        if (isEquipped)
        {
            img.color = Color.yellow;
            TextPro.text = "Equipped";
        }
        else if (!isPurchased)
        {
            img.color = Color.red;
            TextPro.text = price <= 0 ? "Free" : price.ToString();
        }
        else
        {
            img.color = Color.blue;
            TextPro.text = "Equip";
        }
    }

    protected void SaveState()
    {
        PlayerPrefs.SetInt($"{gameObject.name}_Purchased", isPurchased ? 1 : 0);
        PlayerPrefs.SetInt($"{gameObject.name}_Equipped", isEquipped ? 1 : 0);
        PlayerPrefs.Save();
    }
}
