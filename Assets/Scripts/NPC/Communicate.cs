using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Communicate : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textHintTMP; 
    public GameObject canvas_A;
    public GameObject canvas_B;
    public Toggle targetToggle;

    [Header("Interact")]
    public float showRange = 5f;

    private Transform player;
    private bool isInteracting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (textHintTMP != null)
            textHintTMP.gameObject.SetActive(false);   

        canvas_A.SetActive(false);
        canvas_B.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (!isInteracting)
        {
            if (dist <= showRange)
            {
                if (!textHintTMP.gameObject.activeSelf)
                    textHintTMP.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                    StartInteract();
            }
            else
            {
                if (textHintTMP.gameObject.activeSelf)
                    textHintTMP.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                StopInteract();
        }
    }

    void StartInteract()
    {
        isInteracting = true;
        textHintTMP?.gameObject.SetActive(false);

        if (!targetToggle.isOn) 
        {
            canvas_A.SetActive(true);
            canvas_B.SetActive(false);
        }
        else // toggle ON
        {
            canvas_B.SetActive(true);
            canvas_A.SetActive(false);
        }
    }

    public void StopInteract()
    {
        isInteracting = false;

        canvas_A.SetActive(false);
        canvas_B.SetActive(false);

        if (Vector3.Distance(transform.position, player.position) <= showRange)
            textHintTMP?.gameObject.SetActive(true);
    }
}
