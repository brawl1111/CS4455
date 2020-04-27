using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b")) 
        StartCoroutine(SplitMesh(true));
    }

    public IEnumerator SplitMesh (bool destroy)
    {
    	// if (GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null) {
    	// 	Debug.Log("no mesh filter");
    	// 	yield return null;
    	// }

    	if (GetComponent<Collider>())
    	{
    		GetComponent<Collider>().enabled = false;
    	}

    	Mesh m = new Mesh();
    	if (GetComponent<MeshFilter>())
    	{
    		m = GetComponent<MeshFilter>().mesh;
    	} else if (GetComponent<SkinnedMeshRenderer>())
    	{
    		m = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    	}

    	Material[] materials = new Material[0];
    	if (GetComponent<MeshRenderer>())
    	{
    		materials = GetComponent<MeshRenderer>().materials;
    	} else if (GetComponent<SkinnedMeshRenderer>())
    	{
    		materials = GetComponent<SkinnedMeshRenderer>().materials;
    	}

    	Vector3[] verts = m.vertices;
    	Vector3[] normals = m.normals;
    	Vector2[] uvs = m.uv;
    	for (int submesh = 0; submesh < m.subMeshCount; submesh++)
    	{
    		int[] indices = m.GetTriangles(submesh);

    		for (int i = 0; i < indices.Length; i += 3)
    		{
    			Vector3[] newVerts = new Vector3[3];
    			Vector3[] newNormals = new Vector3[3];
    			Vector2[] newUvs = new Vector2[3];
    			for (int n = 0; n < 3; n++)
    			{
    				int index = indices[i + n];
    				newVerts[n] = verts[index];
    				newUvs[n] = uvs[index];
    				newNormals[n] = normals[index];
    			}

    			Mesh mesh = new Mesh();
    			mesh.vertices = newVerts;
    			mesh.normals = newNormals;
    			mesh.uv = newUvs;

    			mesh.triangles = new int[] {0, 1, 2, 2, 1, 0};

    			GameObject go = new GameObject("Triange" + (i/3));
    			go.layer = LayerMask.NameToLayer("Particles");
    			go.transform.position = transform.position;
    			go.transform.rotation = transform.rotation;
    			go.AddComponent<MeshRenderer>().material = materials[submesh];
    			go.AddComponent<MeshFilter>().mesh = mesh;
    			go.AddComponent<BoxCollider>();
    			Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f),
    				transform.position.y + Random.Range(0f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
    			go.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(300,500), explosionPos, 5);
    			Destroy(go, 5 + Random.Range(0.0f, 5.0f));
    		}
    	}

    	GetComponent<Renderer>().enabled = false;

    	yield return new WaitForSeconds(1.0f);
    	if (destroy == true)
    	{
    		Destroy(gameObject);
    	}
    }
}
