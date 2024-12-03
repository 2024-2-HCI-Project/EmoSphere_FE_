using UnityEngine;

public class StartEffect : MonoBehaviour
{
    public ParticleSystem fireEffect; // 불꽃 파티클 시스템 연결

    public void StartFire()
    {
        fireEffect.Play(); // 불꽃 효과 실행
    }
}



