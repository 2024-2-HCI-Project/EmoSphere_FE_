using UnityEngine;

public class RandomizeSpikes : MonoBehaviour
{
    public float spikeHeight = 0.3f; // 뾰족뾰족한 정도
    private Mesh mesh; // 수정할 메쉬를 저장

    void Start()
    {
        // MeshFilter 컴포넌트가 있는지 확인
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter가 이 오브젝트에 없습니다!");
            return;
        }

        // MeshFilter의 sharedMesh를 복사하여 새로운 메쉬 인스턴스를 만듦
        mesh = Instantiate(meshFilter.sharedMesh); // sharedMesh를 복사하여 새로운 메쉬 생성
        mesh.name = "CustomMesh"; // 새로 만든 메쉬 이름 지정
        meshFilter.mesh = mesh; // MeshFilter의 메쉬를 수정한 메쉬로 설정

        // 메쉬의 정점(vertices)을 가져오기
        Vector3[] vertices = mesh.vertices;

        // 각 정점을 랜덤하게 변형하여 뾰족하게 만들기
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += vertices[i].normalized * Random.Range(0, spikeHeight); // 정점을 변형
        }

        // 수정된 정점 데이터를 다시 메쉬에 반영
        mesh.vertices = vertices;

        // 메쉬의 법선 벡터(normals)와 경계(boundaries)를 재계산
        mesh.RecalculateNormals();  // 법선 벡터 재계산
        mesh.RecalculateBounds();   // 경계 재계산
    }
}
