using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PlacementGrid : MonoBehaviour
{
    public int gridSize = 20;

    private GameObject placementModel;
    private Building _buildingPattern;
    public Building buildingPattern
    {
        set
        {
            _buildingPattern = value;
            UpdatePlacementModel(value);
        }
        get {
            return _buildingPattern;
        }
    }

    public Material buildMaterial;

    private Mesh gridMesh;
    private bool buildPossible;
    private MeshRenderer meshRenderer;

    private void OnDisable()
    {
        Destroy(placementModel);
    }

    void Start()
    {
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

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
                CreateNewBuilding(_buildingPattern);
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                transform.Rotate(0, -90.0f, 0);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                transform.Rotate(0, 90.0f, 0);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == Tag.OBSTACLE)
        {
            buildMaterial.SetColor("_Color", Color.red);
            buildPossible = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tag.OBSTACLE)
        {
            buildMaterial.SetColor("_Color", Color.green);
            buildPossible = true;
        }
    }

    private void CreateNewBuilding(Building building)
    {
        var newBuilding = Instantiate(building.prefab, transform.position, transform.rotation);
        newBuilding.AddComponent<BoxCollider>().size = new Vector3(building.gridSize.x, building.calculatedHeight, building.gridSize.y);

        var construction = newBuilding.AddComponent<Construction>();
        construction.toConstruct = building;
    }

    private void UpdatePlacementModel(Building building)
    {
        Destroy(placementModel);
        placementModel = Instantiate(building.prefab, transform.position, transform.rotation, transform);

        gameObject.GetComponent<BoxCollider>().size = new Vector3(building.gridSize.x, building.calculatedHeight, building.gridSize.y);

        var children = placementModel.GetComponentsInChildren<Renderer>();
        foreach (var child in children)
        {
            foreach (var mat in child.materials)
            {
                child.material = buildMaterial;
            }
        }

        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.SetInt("_GridSize", gridSize);
        meshRenderer.material.SetFloat("_BuildingSizeX", _buildingPattern.gridSize.x);
        meshRenderer.material.SetFloat("_BuildingSizeZ", _buildingPattern.gridSize.y);
    }
}
