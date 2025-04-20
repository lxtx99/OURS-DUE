using UnityEngine;
using UnityEngine.UI;


public class BGMToggle : MonoBehaviour
{
    public AudioSource bgm; // �������AudioSource

    public void ToggleBGM()
    {
        bgm.mute = !bgm.mute; // �л�����״̬
    }
}