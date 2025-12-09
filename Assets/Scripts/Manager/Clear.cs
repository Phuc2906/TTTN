using UnityEngine;

public class Clear : MonoBehaviour
{
        void Start()
    {
        // code nay de xoa PlayerPrefs neu can test lai
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Đã xóa toàn bộ PlayerPrefs");
    }

    //cho code vào 1 game object r chạy 1 lần sau đó nhớ xóa script nàykhỏi game object
}
