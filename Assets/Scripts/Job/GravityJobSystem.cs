using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using UnityEngine;

namespace Job
{
	public class GravityJobSystem : JobComponentSystem
	{
		public static float G = -20;
		public static float topY = 20f;
		public static float bottomY = -100f;

		struct GravityJob : IJobProcessComponentData<GravityComponentData, Position>
		{
			//运行在其它线程中，需要拷贝一份GravityJobSystem的数据
			public float G;
			public float topY;
			public float bottomY;
			//非主线程不能使用Time.deltaTime
			public float deltaTime;
			//非主线程不能使用UnityEngine.Random
			public Unity.Mathematics.Random random;

			//物理运算
			public void Execute(ref GravityComponentData gravityData, ref Position positionData)
			{
				if (gravityData.delay > 0)
				{
					gravityData.delay -= deltaTime;
				}
				else
				{
					Vector3 pos = positionData.Value;
					float v = gravityData.velocity + G * gravityData.mass * deltaTime;
					pos.y += v;
					if (pos.y < bottomY)
					{
						pos.y = topY;
						gravityData.velocity = 0f;
						gravityData.delay = random.NextFloat(0, 10);
					}
					positionData.Value = pos;
				}
			}
		}

		//每帧生成任务丢给Job System
		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			GravityJob job = new GravityJob()
			{
				G = G,
				topY = topY,
				bottomY = bottomY,
				deltaTime = Time.deltaTime,
				random = new Unity.Mathematics.Random((uint)(Time.time * 1000 + 1)),
			};


			return job.Schedule(this, inputDeps);
		}
	}
}