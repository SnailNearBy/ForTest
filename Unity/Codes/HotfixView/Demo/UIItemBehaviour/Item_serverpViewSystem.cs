
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_serverpDestroySystem : DestroySystem<Scroll_Item_serverp> 
	{
		public override void Destroy( Scroll_Item_serverp self )
		{
			self.DestroyWidget();
		}
	}
}
