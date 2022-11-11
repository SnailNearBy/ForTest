
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESCommonUITestAwakeSystem : AwakeSystem<ESCommonUITest,Transform> 
	{
		public override void Awake(ESCommonUITest self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESCommonUITestDestroySystem : DestroySystem<ESCommonUITest> 
	{
		public override void Destroy(ESCommonUITest self)
		{
			self.DestroyWidget();
		}
	}
}
