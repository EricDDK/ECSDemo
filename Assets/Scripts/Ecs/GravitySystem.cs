using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
	public class GravitySystem : ComponentSystem
	{
		struct Filter
		{
			public readonly int Length;
			public ComponentDataArray<GravityComponentData> gravity;
			public ComponentDataArray<Position> position;
		}

		[Inject] Filter data;
		public static float G = -20f;
		public static float topY = 20f;
		public static float bottomY = -100f;
		protected override void OnUpdate()
		{
			for (int i = 0; i < data.Length; ++i)
			{
				var gravityData = data.gravity[i];
				if (gravityData.delay > 0)
				{
					gravityData.delay -= Time.deltaTime;
					data.gravity[i] = gravityData;
				}
				else
				{
					Vector3 pos = data.position[i].Value;
					float v = gravityData.velocity + G * gravityData.mass * Time.deltaTime;
					pos.y += v;
					if (pos.y < bottomY)
					{
						pos.y = topY;
						gravityData.velocity = 0f;
						gravityData.delay = Random.Range(5f, 10f);
						data.gravity[i] = gravityData;
					}
					data.position[i] = new Position() { Value = pos };
				}

			}
		}
	}
}