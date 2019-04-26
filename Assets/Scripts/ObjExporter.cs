using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ObjExporter : MonoBehaviour{

    public Texture2D currentTexture;
    public Text CompleteTXT;
    public GameObject CompletePanel;

    public string MeshToString(SkinnedMeshRenderer mf) {
		Mesh m = mf.sharedMesh;
		Material[] mats = mf.materials;

		StringBuilder sb = new StringBuilder();

		sb.Append("g ").Append(mf.name).Append("\n");
		foreach(Vector3 v in m.vertices) {
			sb.Append(string.Format("v {0} {1} {2}\n",v.x,v.y,v.z));
		}
		sb.Append("\n");
		foreach(Vector3 v in m.normals) {
			sb.Append(string.Format("vn {0} {1} {2}\n",v.x,v.y,v.z));
		}
		sb.Append("\n");
		foreach(Vector3 v in m.uv) {
			sb.Append(string.Format("vt {0} {1}\n",v.x,v.y));
		}
		for (int material=0; material < mats.Length; material ++) 
		{
			string materialName = mats [material].name;
			sb.Append("\n");
			sb.Append("usemtl ").Append(mats[material].name).Append("\n");
			sb.Append("usemap ").Append(mats[material].name).Append("\n");


			int[] triangles = m.GetTriangles(material);
			for (int i=0;i<triangles.Length;i+=3) 
			{
				sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", 
					triangles[i]+1, triangles[i+1]+1, triangles[i+2]+1));
			}
		}
		return sb.ToString();
	}
    public void GatherTextures(int currentTextureId, SkinnedMeshRenderer renderer, string category, string groupName, string subgroupName)
    {
        if (currentTextureId != renderer.sharedMaterials.Length)
        {
            if (renderer.materials[currentTextureId].mainTexture != null)
            {
                currentTexture = renderer.sharedMaterials[currentTextureId].mainTexture as Texture2D;

                GameObject.Find("Manager").GetComponent<ObjExporter>().SaveTextureToFile(currentTexture, category, groupName, subgroupName, currentTexture.name + ".png");

                GatherTextures(currentTextureId + 1, renderer, category, groupName, subgroupName);
            }
            else
            {
                GatherTextures(currentTextureId + 1, renderer, category, groupName, subgroupName);
            }
        }
    }
    public void MeshToFile(SkinnedMeshRenderer mf, string filename) {
		string path = Application.persistentDataPath + "/Unity/Body/";
		(new FileInfo(path)).Directory.Create();
		using (StreamWriter sw = new StreamWriter (Application.persistentDataPath + "/Unity/Body/" + filename))
		{
            GatherTextures(0, mf, "Body", null, null);
			sw.Write(MeshToString(mf));
			Debug.Log ("Exported to: " + path);
			sw.Close ();
		}
        CompletePanel.SetActive(true);
        CompleteTXT.text = "Your model has been exported to: " + '\n' + path;
	}
	public void SaveTextureToFile(Texture2D Texture, string category, string groupName, string subgroupName, string filename)
	{
		string path = Application.persistentDataPath + "/Unity/" + category + "/";
		if (!String.IsNullOrEmpty (groupName)) 
		{
			path += groupName + "/";
		}
		if (!String.IsNullOrEmpty (subgroupName)) 
		{
			path += subgroupName + "/";
		}
		path += "Textures/";
		Debug.Log ("Texture path: " + path);
		(new FileInfo (path)).Directory.Create ();
		var bytes = Texture.EncodeToPNG ();
		var file = new FileStream(path + filename, FileMode.Create);
		var binary = new BinaryWriter (file);
		binary.Write (bytes);
		file.Close ();
		Debug.Log (filename + " texture saved");
	}
}