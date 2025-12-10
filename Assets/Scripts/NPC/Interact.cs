using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("UI")]
    public GameObject text;       
    public GameObject paperCanvas;   

    [Header("Interact")]
    public float showRange = 5f;

    private Transform player;
    private bool isInteracting = false; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (text != null)
            text.SetActive(false);
        if (paperCanvas != null)
            paperCanvas.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (!isInteracting)
        {
            if (dist <= showRange)
            {
                if (!text.activeSelf)
                    text.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                    StartInteract();
            }
            else
            {
                if (text.activeSelf)
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

    if (player != null)
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= showRange && text != null)
            text.SetActive(true);
    }
}

}
