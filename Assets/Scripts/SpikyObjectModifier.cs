using UnityEngine;

public class SpikyObjectModifier : MonoBehaviour
{
    public float minSpikeIntensity = 0.0f; // 최소 뾰족함 (완전한 구체)
    public float maxSpikeIntensity = 2.0f; // 최대 뾰족함
    private Mesh originalMesh;
    private Mesh modifiedMesh;
    private Vector3[] originalVertices;

    void Start()
    {
        // 원본 Mesh 데이터를 가져옴
        originalMesh = GetComponent<MeshFilter>().mesh;
        modifiedMesh = Instantiate(originalMesh);
        GetComponent<MeshFilter>().mesh = modifiedMesh;
        originalVertices = originalMesh.vertices;

        // 씬 로드 시 한 번 모양을 랜덤으로 변경
        ModifyMesh();
    }

    void ModifyMesh()
    {
        Vector3[] vertices = originalVertices.Clone() as Vector3[];
        float spikeIntensity = Random.Range(minSpikeIntensity, maxSpikeIntensity); // 뾰족함 정도 랜덤 결정

        for (int i = 0; i < vertices.Length; i++)
        {
            // 각 정점을 중심에서 바깥으로 이동
            vertices[i] += vertices[i].normalized * Random.Range(0, spikeIntensity); // 정점마다 개별적으로 랜덤
        }

        modifiedMesh.vertices = vertices;
        modifiedMesh.RecalculateNormals(); // Normal 다시 계산
        modifiedMesh.RecalculateBounds(); // Bounds 업데이트
    }
}
