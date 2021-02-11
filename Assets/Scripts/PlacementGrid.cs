using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacementGrid : MonoBehaviour
{
    public int gridSize = 20;
    public Building buildingPattern;
    public GameObject buildingTemplate;
    public Material buildMaterial;

    private Mesh gridMesh;
    private GameObject placementModel;
    private bool buildPossible;
    private MeshRenderer meshRenderer;

    private void OnEnable()
    {
        buildPossible = true;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(buildingPattern.gridSize.x, 1, buildingPattern.gridSize.y);
        placementModel = Instantiate(buildingPattern.prefab, transform.position, transform.rotation, transform);
        var children = placementModel.GetComponentsInChildren<Renderer>();
        foreach (var child in children) {
            foreach (var mat in child.materials)
            {
                child.material = buildMaterial;
            }
        }
    }

    private void OnDisable()
    {
        Destroy(placementModel);
    }

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.SetInt("_GridSize", gridSize);
        meshRenderer.material.SetFloat("_BuildingSizeX", buildingPattern.gridSize.x);
        meshRenderer.material.SetFloat("_BuildingSizeZ", buildingPattern.gridSize.y);

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        gridMesh = new Mesh();
        meshFilter.mesh = gridMesh;

        gridMesh.Clear();

        Vector3[] vertices = new Vector3[gridSize * gridSize];
        List<int> lines = new List<int>(gridSize * gridSize * 4 - gridSize * 4);

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                var index = (y * gridSize) + x;
                var centeredLength = gridSize / 2;
                vertices[index] = (new Vector3(x - centeredLength, 0.0f, y - centeredLength));

                if (x > 0)
                {
                    lines.Add(index - 1);
                    lines.Add(index);
                }
                if (y > 0)
                {
                    lines.Add(index - gridSize);
                    lines.Add(index);
                }
            }
        }

        gridMesh.vertices = vertices;
        gridMesh.SetIndices(lines.ToArray(), MeshTopology.Lines, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("World")))
        {
            var xPos = Mathf.Round(hit.point.x);
            var zPos = Mathf.Round(hit.point.z);
            var pos = new Vector3(xPos, hit.point.y, zPos);

            transform.position = pos;

            if (buildPossible && Input.GetMouseButtonDown(0))
            {
                var newBuilding = Instantiate(buildingTemplate, pos, transform.rotation);
                
                var collider = newBuilding.GetComponent<BoxCollider>();
                collider.size = new Vector3(buildingPattern.gridSize.x, 1, buildingPattern.gridSize.y);

                var construction = newBuilding.AddComponent<Construction>();
                construction.toConstruct = buildingPattern;

                Instantiate(buildingPattern.prefab, pos, transform.rotation, newBuilding.transform);
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                transform.Rotate(0, -90.0f,0);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                transform.Rotate(0, 90.0f,0);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log(other.tag);
        if (other.tag == "Obstacle")
        {
            buildMaterial.SetColor("_Color", Color.red);
            buildPossible = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Obstacle")
        {
            buildMaterial.SetColor("_Color", Color.green);
            buildPossible = true;
        }
    }
}
