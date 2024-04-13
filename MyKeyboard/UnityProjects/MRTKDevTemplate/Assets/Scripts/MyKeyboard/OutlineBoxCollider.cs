using UnityEngine;

public class OutlineBoxCollider : MonoBehaviour
{
    public Color outlineColor = Color.yellow;
    public float outlineWidth = 0.01f;

    private BoxCollider boxCollider;
    private GameObject outlineObject;
    private LineRenderer[] outlineLines;

    void Start()
    {
        // 获取 Box Collider 组件
        boxCollider = GetComponent<BoxCollider>();

        // 创建描边 GameObject
        outlineObject = new GameObject("Outline");
        outlineObject.transform.parent = transform;

        // 创建 Line Renderer 组件
        outlineLines = new LineRenderer[12];
        for (int i = 0; i < 12; i++)
        {
            GameObject lineObject = new GameObject("OutlineLine" + i);
            lineObject.transform.parent = outlineObject.transform;
            outlineLines[i] = lineObject.AddComponent<LineRenderer>();
            outlineLines[i].positionCount = 2;
            outlineLines[i].startWidth = outlineWidth;
            outlineLines[i].endWidth = outlineWidth;
            outlineLines[i].material = new Material(Shader.Find("Sprites/Default"));
            outlineLines[i].material.color = outlineColor;
        }

        // 更新描边
        UpdateOutline();
    }

    void UpdateOutline()
    {
        // 获取 Box Collider 的边界信息
        Bounds bounds = boxCollider.bounds;
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        // 计算边界顶点
        Vector3[] vertices = new Vector3[8];
        vertices[0] = center + new Vector3(extents.x, extents.y, extents.z);
        vertices[1] = center + new Vector3(-extents.x, extents.y, extents.z);
        vertices[2] = center + new Vector3(-extents.x, -extents.y, extents.z);
        vertices[3] = center + new Vector3(extents.x, -extents.y, extents.z);
        vertices[4] = center + new Vector3(extents.x, extents.y, -extents.z);
        vertices[5] = center + new Vector3(-extents.x, extents.y, -extents.z);
        vertices[6] = center + new Vector3(-extents.x, -extents.y, -extents.z);
        vertices[7] = center + new Vector3(extents.x, -extents.y, -extents.z);

        // 更新描边位置
        // 12 条边由 8 个顶点组成
        int[] lines = new int[]
        {
            0, 1, 1, 2, 2, 3, 3, 0,
            4, 5, 5, 6, 6, 7, 7, 4,
            0, 4, 1, 5, 2, 6, 3, 7
        };

        for (int i = 0; i < 12; i++)
        {
            outlineLines[i].SetPosition(0, vertices[lines[i * 2]]);
            outlineLines[i].SetPosition(1, vertices[lines[i * 2 + 1]]);
        }
    }

    void Update()
    {
        // 每帧更新描边位置（可选）
        UpdateOutline();
    }
}