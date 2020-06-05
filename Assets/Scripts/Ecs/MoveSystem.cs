using Unity.Entities;
using UnityEngine;

public class MoveSystem : ComponentSystem
{
	public struct Filter
	{
		public Transform tf;
		public MoveComponent moveComponent;
	}

	protected override void OnUpdate()
	{
		foreach (var entity in GetEntities<Filter>())
		{
			Vector3 pos = entity.tf.position + entity.moveComponent.moveDir * Time.deltaTime * 3;
			entity.tf.position = pos;
		}
	}
}