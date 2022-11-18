
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_serveroption : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_serveroption BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EButton_OptionButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_EButton_OptionButton == null )
     				{
		    			this.m_EButton_OptionButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_Option");
     				}
     				return this.m_EButton_OptionButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_Option");
     			}
     		}
     	}

		public UnityEngine.UI.Image EButton_OptionImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_EButton_OptionImage == null )
     				{
		    			this.m_EButton_OptionImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_Option");
     				}
     				return this.m_EButton_OptionImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_Option");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_OptionTextText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_OptionTextText == null )
     				{
		    			this.m_E_OptionTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EButton_Option/E_OptionText");
     				}
     				return this.m_E_OptionTextText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EButton_Option/E_OptionText");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_OptionButton = null;
			this.m_EButton_OptionImage = null;
			this.m_E_OptionTextText = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_EButton_OptionButton = null;
		private UnityEngine.UI.Image m_EButton_OptionImage = null;
		private UnityEngine.UI.Text m_E_OptionTextText = null;
		public Transform uiTransform = null;
	}
}
