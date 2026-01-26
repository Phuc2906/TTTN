using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManagerList : MonoBehaviour
{
    [Header("Card")]
    public List<CardUI> cards = new List<CardUI>();

    [Header("UI")]
    public GameObject continueButton;
    public GameObject gameCanvas;
    public GameObject warningCanvas;
    public TextMeshProUGUI playCountText;

    [Header("Play Count")]
    public int maxPlays = 3;
    private int currentPlays;

    private CardUI firstCard;
    private CardUI secondCard;
    private bool canClick = true;

    void Start()
    {
        if (continueButton != null)
            continueButton.SetActive(false);

        if (warningCanvas != null)
            warningCanvas.SetActive(false);

        currentPlays = maxPlays;
        UpdatePlayText();

        foreach (CardUI card in cards)
        {
            card.onCardClicked.AddListener(OnCardClicked);
        }
    }

    void OnCardClicked(CardUI card)
    {
        if (!canClick) return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        canClick = false;
        yield return new WaitForSeconds(0.5f);

        if (firstCard.frontImage.name == secondCard.frontImage.name)
        {
            firstCard.Hide();
            secondCard.Hide();
            CheckAllMatched();
        }
        else
        {
            firstCard.Flip();
            secondCard.Flip();

            currentPlays--;
            UpdatePlayText();

            if (currentPlays <= 0)
            {
                EndGame();
            }
        }

        firstCard = null;
        secondCard = null;
        canClick = true;
    }

    void UpdatePlayText()
    {
        if (playCountText != null)
            playCountText.text = "Số lần chơi: " + currentPlays;
    }

    void EndGame()
    {
        if (gameCanvas != null)
            gameCanvas.SetActive(false);

        if (warningCanvas != null)
            warningCanvas.SetActive(true);
    }

    void CheckAllMatched()
    {
        foreach (CardUI c in cards)
        {
            if (!c.isMatched)
                return;
        }

        if (continueButton != null)
            continueButton.SetActive(true);
    }
}
