//using Unity.Entities;
//using UnityEngine;

//public class InputSystem : ComponentSystem
//{
//	struct Data
//	{
//		public ComponentArray<MoveComponent> moveArray;
//	}

//	[Inject] private Data _data;
//	protected override void OnUpdate()
//	{
//		for (int i = 0; i < _data.moveArray.Length; ++i)
//		{
//			_data.moveArray[i].moveDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
//		}
//	}
//}