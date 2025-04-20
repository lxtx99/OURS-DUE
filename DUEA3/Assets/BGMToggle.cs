using UnityEngine;
using UnityEngine.UI;


public class BGMToggle : MonoBehaviour
{
    public AudioSource bgm; // ÍÏÈëÄãµÄAudioSource

    public void ToggleBGM()
    {
        bgm.mute = !bgm.mute; // ÇĞ»»¾²Òô×´Ì¬
    }
}