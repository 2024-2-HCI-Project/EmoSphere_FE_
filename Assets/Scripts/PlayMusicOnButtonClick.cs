using UnityEngine;

public class PlayMusicOnButtonClick : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource 연결

    public void PlayMusic()
    {
        if (!audioSource.isPlaying) // 이미 재생 중이 아니면 재생
        {
            audioSource.Play();
        }
    }
}
