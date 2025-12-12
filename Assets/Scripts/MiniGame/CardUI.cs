using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class CardUI : MonoBehaviour
{
    public Sprite frontImage; 
    public Sprite backImage;  

    private Image image;

    [HideInInspector] public bool isFlipped = false;
    [HideInInspector] public bool isMatched = false;

    public UnityEvent<CardUI> onCardClicked;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        isFlipped = true;
        image.sprite = frontImage;
        StartCoroutine(ShowFrontThenBack(1f));
    }

    private IEnumerator ShowFrontThenBack(float wait)
    {
        yield return new WaitForSeconds(wait);
        Flip();
    }

    public void OnClick()
    {
        if (isMatched) return;   
        if (isFlipped) return;   

        Flip();
        onCardClicked?.Invoke(this);
    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        image.sprite = isFlipped ? frontImage : backImage;
    }

    public void Hide()
    {
        isMatched = true;
        gameObject.SetActive(false);
    }
}
