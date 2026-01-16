using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButtonBase : MonoBehaviour, ICharacterButton
{
    [Header("Character ID (0 → 3)")]
    public int characterID;

    [Header("UI")]
    public Button equipButton;
    public TMP_Text buttonText;

    [Header("Price")]
    public int price;
    public bool isFree;

    [Header("Notice")]
    public GameObject noticeCanvas;

    protected bool isPurchased;
    protected bool isEquipped;

    protected virtual void Start()
    {
        CharacterManager.Instance.Register(this);
        equipButton.onClick.AddListener(OnClick);

        LoadState();

        if (isEquipped)
            CharacterManager.Instance.OnCharacterEquipped(this);

        UpdateUI();
    }

    void LoadState()
    {
        isPurchased = isFree || PlayerPrefs.GetInt($"Char_{characterID}_Buy", 0) == 1;
        isEquipped  = PlayerPrefs.GetInt("SelectedPlayerID", 0) == characterID;
    }

    void OnClick()
    {
        if (!isPurchased)
            Buy();
        else
            Equip();
    }

    void Buy()
    {
        if (isFree)
        {
            isPurchased = true;
            PlayerPrefs.SetInt($"Char_{characterID}_Buy", 1);
            Equip();
            return;
        }

        if (CoinManager.Instance.SpendCoin(price))
        {
            isPurchased = true;
            PlayerPrefs.SetInt($"Char_{characterID}_Buy", 1);
            Equip();
        }
        else
        {
            ShowNotice();
        }
    }

    void Equip()
    {
        isEquipped = true;

        PlayerPrefs.SetInt("SelectedPlayerID", characterID);
        PlayerPrefs.Save();

        CharacterManager.Instance.OnCharacterEquipped(this);
        UpdateUI();
    }

    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
        UpdateUI();
    }

    void ShowNotice()
    {
        if (noticeCanvas != null)
            noticeCanvas.SetActive(true);
    }

    void UpdateUI()
    {
        if (isEquipped)
        {
            buttonText.text = "Đã mặc";
            equipButton.image.color = Color.yellow;
        }
        else if (!isPurchased)
        {
            buttonText.text = isFree ? "Free" : price.ToString();
            equipButton.image.color = Color.red;
        }
        else
        {
            buttonText.text = "Mặc";
            equipButton.image.color = Color.blue;
        }
    }
}
