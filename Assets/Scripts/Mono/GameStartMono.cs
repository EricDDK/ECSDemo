using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartMono : MonoBehaviour
{
	public Mesh mesh;
	public Material mat;

	public GameObject ball;

	public int spawnCount;

	private void Awake()
	{
		StartLagacyMethod();
	}

	void StartLagacyMethod()
	{
		var rootGo = new GameObject("Balls");
		rootGo.transform.position = Vector3.zero;

		for (int i = 0; i < spawnCount; ++i)
		{
			var go = new GameObject();
			var meshFilter = go.AddComponent<MeshFilter>();
			meshFilter.sharedMesh = mesh;

			var meshRd = go.AddComponent<MeshRenderer>();
			meshRd.sharedMaterial = mat;

			var dropComponent = go.AddComponent<LagacyDrop>();
			dropComponent.delay = 0.02f * i;
			dropComponent.mass = Random.Range(0.5f, 3f);

			Vector3 pos = UnityEngine.Random.insideUnitSphere * 40;
			go.transform.parent = rootGo.transform;
			pos.y = 1;
			go.transform.position = pos;
		}
	}

}
