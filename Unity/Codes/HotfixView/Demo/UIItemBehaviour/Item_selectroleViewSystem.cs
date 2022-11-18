
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_selectroleDestroySystem : DestroySystem<Scroll_Item_selectrole> 
	{
		public override void Destroy( Scroll_Item_selectrole self )
		{
			self.DestroyWidget();
		}
	}
}
