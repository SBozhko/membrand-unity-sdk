using UnityEngine;
using System.Collections;

public class AdOnTexture : MonoBehaviour {

	private Vector2[] uvs;

	public class InitialDataPackage {
		[System.Serializable]
		public class Bid {
			public string url;
			public int[] distance_update_level;
		}
		public string id;
		public Bid bid;
	}

	// Use this for initialization
	void Start () {
//		Debug.Log ("I'm running");
//		MeshFilter mf = GetComponent<MeshFilter> ();
//		Mesh m = mf.mesh;
//		Vector3[] vertices = m.vertices;
//		uvs = new Vector2[vertices.Length];
//		for (int i=0; i < uvs.Length; i++) {
//			uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
//		}
//		Debug.Log (uvs);
		string url = "http://localhost:3000/get_ad_url";
		WWWForm form = new WWWForm ();
		form.AddField("test", 43);
		WWW www = new WWW (url, form);
		StartCoroutine (InitialRequest (www));
	}

	IEnumerator InitialRequest(WWW www) {
		yield return www;
//		Debug.Break ();
		InitialDataPackage package = JsonUtility.FromJson<InitialDataPackage> (www.text);
		string url = package.bid.url;
		WWW adTextureWWW = new WWW (url);
		StartCoroutine (WaitForRequest (adTextureWWW));
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		Renderer r = GetComponent<Renderer> ();
		r.material.mainTexture = new Texture2D (4, 4, TextureFormat.DXT1, false);
		www.LoadImageIntoTexture (r.material.mainTexture as Texture2D);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
