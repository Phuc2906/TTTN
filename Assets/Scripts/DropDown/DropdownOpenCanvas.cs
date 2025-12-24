using UnityEngine;

public class DropdownOpenCanvas : MonoBehaviour
{
    public GameObject[] canvasList;

    public void OnDropdownChanged(int index)
    {
        for (int i = 0; i < canvasList.Length; i++)
        {
            canvasList[i].SetActive(i == index);
        }
    }
}
