using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
	public class BallDropMain : MonoBehaviour
	{
		public Material mat;
		public Mesh mesh;
		public GameObject ballPrefab;

		public int spawnCount;

		void Start()
		{
			StartECSMethod();
		}

		void StartECSMethod()
		{
			var entityMgr = World.Active.GetOrCreateManager<EntityManager>();
			var entities = new NativeArray<Entity>(spawnCount, Allocator.Temp);

			//Two ways of creating entities
			var archeType = entityMgr.CreateArchetype(typeof(GravityComponentData), typeof(Position), typeof(MeshInstanceRenderer));
			entityMgr.CreateEntity(archeType, entities);

			var meshRenderer = new MeshInstanceRenderer()
			{
				mesh = mesh,
				material = mat,
			};
			//Add Components
			for (int i = 0; i < entities.Length; ++i)
			{
				Vector3 pos = UnityEngine.Random.insideUnitSphere * 40;
				pos.y = GravitySystem.topY;
				var entity = entities[i];
				entityMgr.SetComponentData(entity, new Position { Value = pos });
				entityMgr.SetComponentData(entity, new GravityComponentData { mass = Random.Range(0.5f, 3f), delay = 0.02f * i });
				entityMgr.SetSharedComponentData(entity, meshRenderer);
			}

			entities.Dispose();
		}
	}
}