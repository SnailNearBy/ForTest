
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_serveroptionDestroySystem : DestroySystem<Scroll_Item_serveroption> 
	{
		public override void Destroy( Scroll_Item_serveroption self )
		{
			self.DestroyWidget();
		}
	}
}
