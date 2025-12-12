using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerList : MonoBehaviour
{
    public List<CardUI> cards = new List<CardUI>();
    public GameObject continueButton;
    public PlayerHealth playerHealth;  

    private CardUI firstCard;
    private CardUI secondCard;

    private bool canClick = true;

    void Start()
    {
        if (continueButton != null)
            continueButton.SetActive(false);

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

            
        if (playerHealth != null)
            playerHealth.TakeDamage((int)(playerHealth.maxHealth * 0.25f));

        }

        firstCard = null;
        secondCard = null;

        canClick = true;
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
