using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//required compenents for this script to prevent errors
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]


public class Node
{
    public float nx { get; set; }
    public float ny { get; set; }
    public float nz { get; set; }
}


public class MeshGeneration : MonoBehaviour
{
    #region "Vars"
    public Mesh mesh;
    public Mesh mesh2;
    public GameObject circleHolder;
    public int radius;
    public List<Vector3> points;
    public List<Node> correctedPoints = new List<Node>();
    public Vector3[] vertexList = new Vector3[24];
    public GameManager gameManager;
    public int j = 0;
    public int temp;
    public MeshCollider meshCollider;
    #endregion


    private void OnEnable()
    {
        mesh = new Mesh()
        {
            name = "Procedural Mesh"  
            
        };
        mesh2 = new Mesh()
        {
            name = "Attack Mesh"
        };
    }


    //Do not mess with this unless you understand what this code is doing. In summary, it is creating a mesh object vertex by vertex
    //and connecting triagles together via the vertecies.
    public void GenerateMovementMesh()
    {
        mesh.name = "Movement Mesh";
        // calculate movement radius
        radius = 2;
        
        //To allow mesh collision of the new mesh that was created
        meshCollider = this.gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        // pick most vital points to create polygon


        // first quadrant
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(circleHolder.transform.position.x + radius*2), ny = 0, nz = 0 });
        //2
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(circleHolder.transform.position.x + radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 6) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / 6) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI /6) * radius * 2), nz = 0 });
        //4
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 3) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / 3) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 3) * radius * 2), nz = 0 });
        //6
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI / 2)) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = 0, ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 2) * radius * 2), nz = 0 });


        //8 second quadrant
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * 2) / 3) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * 2) / 3) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * 2) / 3) * radius * 2), nz = 0 });
        //10
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * 5) / 6) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * 5) / 6) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * 5) / 6) * radius * 2), nz = 0 });
        //12
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI) * radius * 2), ny = 0, nz = 0 }); //


        //14 third quadrant
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * -5) / 6) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * -5) / 6) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * -5) / 6) * radius * 2), nz = 0 });
        //16
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * -2) / 3) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * -2) / 3) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI *- 2) / 3) * radius * 2), nz = 0 });
        //18
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin(-Mathf.PI / 2) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = 0, ny = -radius * 2, nz = 0 });


        //20 fourth quadrant
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / -3) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / -3) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / -3) * radius * 2), nz = 0 });
        //22
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / -6) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / -6) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / -6) * radius * 2), nz = 0 });

        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(circleHolder.transform.position.x + radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(circleHolder.transform.position.x + radius * 2), ny = 0, nz = 0 });

        Debug.Log(correctedPoints.Count);
        // convert circle to polygon
        for (int i = 0; i < 25; i++)
        {
            vertexList[i] = new Vector3(correctedPoints[i].nx, correctedPoints[i].ny, 0);
        }
        // generate polygon from perimeter to center
        mesh.vertices = vertexList;
        mesh.triangles = new int[]
        {
            0,1,2,2,3,4,4,5,6,6,7,8,8,9,10,10,11,12,12,13,14,14,15,16,16,17,18,18,19,20,20,21,22,22,23,24,3,15,21,3,9,15,4,6,8,10,12,14,16,18,20,22,24,2
        };
        // display
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void GenerateAttackMesh()
    {
        // calculate movement radius
        radius = 2;
        meshCollider = this.gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        //position circle
        Debug.Log("Positioning circle");
        // pick most vital points to create polygon
        // first quadrant
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(circleHolder.transform.position.x + radius * 2), ny = 0, nz = 0 });
        //2
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(circleHolder.transform.position.x + radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 6) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / 6) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 6) * radius * 2), nz = 0 });
        //4
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 3) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / 3) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 3) * radius * 2), nz = 0 });
        //6
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI / 2)) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = 0, ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / 2) * radius * 2), nz = 0 });

        //8 second quadrant
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * 2) / 3) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * 2) / 3) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * 2) / 3) * radius * 2), nz = 0 });
        //10
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * 5) / 6) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * 5) / 6) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * 5) / 6) * radius * 2), nz = 0 });
        //12
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI) * radius * 2), ny = 0, nz = 0 }); //

        //14 third quadrant
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * -5) / 6) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * -5) / 6) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * -5) / 6) * radius * 2), nz = 0 });
        //16
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * -2) / 3) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos((Mathf.PI * -2) / 3) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin((Mathf.PI * -2) / 3) * radius * 2), nz = 0 });
        //18
        correctedPoints.Add(new Node() { nx = correctedPoints[correctedPoints.Count - 1].nx, ny = Mathf.RoundToInt(Mathf.Sin(-Mathf.PI / 2) * radius * 2), nz = 0 });
        correctedPoints.Add(new Node() { nx = 0, ny = -radius * 2, nz = 0 });

        //20 fourth quadrant
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / -3) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / -3) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / -3) * radius * 2), nz = 0 });
        //22
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / -6) * radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(Mathf.Cos(Mathf.PI / -6) * radius * 2), ny = Mathf.RoundToInt(Mathf.Sin(Mathf.PI / -6) * radius * 2), nz = 0 });

        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(circleHolder.transform.position.x + radius * 2), ny = correctedPoints[correctedPoints.Count - 1].ny, nz = 0 });
        correctedPoints.Add(new Node() { nx = Mathf.RoundToInt(circleHolder.transform.position.x + radius * 2), ny = 0, nz = 0 });

        Debug.Log(correctedPoints.Count);
        // convert circle to polygon
        for (int i = 0; i < 25; i++)
        {
            vertexList[i] = new Vector3(correctedPoints[i].nx, correctedPoints[i].ny, 0);
        }
        // generate polygon from perimeter to center
        mesh2.vertices = vertexList;
        mesh2.triangles = new int[]
        {
            0,1,2,2,3,4,4,5,6,6,7,8,8,9,10,10,11,12,12,13,14,14,15,16,16,17,18,18,19,20,20,21,22,22,23,24,3,15,21,3,9,15,4,6,8,10,12,14,16,18,20,22,24,2
        };
        // display
        GetComponent<MeshFilter>().mesh = mesh2;
    }
}
