
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class ESCommonUITest : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public UnityEngine.UI.Image EImage_imgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EImage_imgImage == null )
     			{
		    		this.m_EImage_imgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EImage_img");
     			}
     			return this.m_EImage_imgImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_labText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_labText == null )
     			{
		    		this.m_ELabel_labText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ELabel_lab");
     			}
     			return this.m_ELabel_labText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EImage_imgImage = null;
			this.m_ELabel_labText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_EImage_imgImage = null;
		private UnityEngine.UI.Text m_ELabel_labText = null;
		public Transform uiTransform = null;
	}
}
