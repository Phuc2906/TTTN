using UnityEngine;

public class Converse : MonoBehaviour
{
    [Header("Save")]
    public string rentID = "Rent_NPC_01";

    [Header("UI")]
    public GameObject text;
    public GameObject paperCanvas;

    [Header("Interact")]
    public float showRange = 5f;

    private Transform player;
    private bool isInteracting = false;
    private bool rented;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        rented = PlayerPrefs.GetInt(rentID, 0) == 1;

        if (rented)
        {
            if (text != null)
                Destroy(text);

            return;
        }

        if (text != null)
            text.SetActive(false);

        if (paperCanvas != null)
            paperCanvas.SetActive(false);
    }

    void Update()
    {
        if (player == null || rented) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (!isInteracting)
        {
            if (dist <= showRange)
            {
                if (text != null && !text.activeSelf)
                    text.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                    StartInteract();
            }
            else
            {
                if (text != null && text.activeSelf)
                    text.SetActive(false);
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

        if (text != null)
            text.SetActive(false);

        if (paperCanvas != null)
            paperCanvas.SetActive(true);
    }

    public void StopInteract()
    {
        isInteracting = false;

        if (paperCanvas != null)
            paperCanvas.SetActive(false);
    }
}