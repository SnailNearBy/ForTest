
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgServerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_BackgroundImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackgroundImage == null )
     			{
		    		this.m_E_BackgroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Background");
     			}
     			return this.m_E_BackgroundImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_ServerListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_ServerListLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_ServerListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_ServerList");
     			}
     			return this.m_ELoopScrollList_ServerListLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button EButton_EnterButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterButton == null )
     			{
		    		this.m_EButton_EnterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_Enter");
     			}
     			return this.m_EButton_EnterButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_EnterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterImage == null )
     			{
		    		this.m_EButton_EnterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_Enter");
     			}
     			return this.m_EButton_EnterImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackgroundImage = null;
			this.m_ELoopScrollList_ServerListLoopVerticalScrollRect = null;
			this.m_EButton_EnterButton = null;
			this.m_EButton_EnterImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_BackgroundImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_ServerListLoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_EnterButton = null;
		private UnityEngine.UI.Image m_EButton_EnterImage = null;
		public Transform uiTransform = null;
	}
}
