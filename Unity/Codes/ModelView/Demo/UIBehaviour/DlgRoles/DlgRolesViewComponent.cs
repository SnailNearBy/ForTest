
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgRolesViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.InputField EInputField_RoleNameInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EInputField_RoleNameInputField == null )
     			{
		    		this.m_EInputField_RoleNameInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"EInputField_RoleName");
     			}
     			return this.m_EInputField_RoleNameInputField;
     		}
     	}

		public UnityEngine.UI.Image EInputField_RoleNameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EInputField_RoleNameImage == null )
     			{
		    		this.m_EInputField_RoleNameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EInputField_RoleName");
     			}
     			return this.m_EInputField_RoleNameImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_RoleListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_RoleListLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_RoleListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_RoleList");
     			}
     			return this.m_ELoopScrollList_RoleListLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button EButton_DeleteRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DeleteRoleButton == null )
     			{
		    		this.m_EButton_DeleteRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_DeleteRole");
     			}
     			return this.m_EButton_DeleteRoleButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_DeleteRoleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DeleteRoleImage == null )
     			{
		    		this.m_EButton_DeleteRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_DeleteRole");
     			}
     			return this.m_EButton_DeleteRoleImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_CreateRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateRoleButton == null )
     			{
		    		this.m_EButton_CreateRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_CreateRole");
     			}
     			return this.m_EButton_CreateRoleButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_CreateRoleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateRoleImage == null )
     			{
		    		this.m_EButton_CreateRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_CreateRole");
     			}
     			return this.m_EButton_CreateRoleImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_StartGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_StartGameButton == null )
     			{
		    		this.m_EButton_StartGameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_StartGame");
     			}
     			return this.m_EButton_StartGameButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_StartGameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_StartGameImage == null )
     			{
		    		this.m_EButton_StartGameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_StartGame");
     			}
     			return this.m_EButton_StartGameImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackgroundImage = null;
			this.m_EInputField_RoleNameInputField = null;
			this.m_EInputField_RoleNameImage = null;
			this.m_ELoopScrollList_RoleListLoopHorizontalScrollRect = null;
			this.m_EButton_DeleteRoleButton = null;
			this.m_EButton_DeleteRoleImage = null;
			this.m_EButton_CreateRoleButton = null;
			this.m_EButton_CreateRoleImage = null;
			this.m_EButton_StartGameButton = null;
			this.m_EButton_StartGameImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_BackgroundImage = null;
		private UnityEngine.UI.InputField m_EInputField_RoleNameInputField = null;
		private UnityEngine.UI.Image m_EInputField_RoleNameImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RoleListLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_DeleteRoleButton = null;
		private UnityEngine.UI.Image m_EButton_DeleteRoleImage = null;
		private UnityEngine.UI.Button m_EButton_CreateRoleButton = null;
		private UnityEngine.UI.Image m_EButton_CreateRoleImage = null;
		private UnityEngine.UI.Button m_EButton_StartGameButton = null;
		private UnityEngine.UI.Image m_EButton_StartGameImage = null;
		public Transform uiTransform = null;
	}
}
