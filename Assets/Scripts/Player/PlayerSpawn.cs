using UnityEngine;
using Unity.Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public GameObject[] players;
    public CinemachineCamera cam;

    public WeaponController CurrentWeaponController { get; private set; }

    void Awake()
    {
        Instance = this;

        int id = PlayerPrefs.GetInt("SelectedPlayerID", 0);

        for (int i = 0; i < players.Length; i++)
        {
            bool active = (i == id);
            players[i].SetActive(active);

            if (active)
            {
                cam.Follow = players[i].transform;
                CurrentWeaponController =
                    players[i].GetComponent<WeaponController>();
            }
        }
    }
}
