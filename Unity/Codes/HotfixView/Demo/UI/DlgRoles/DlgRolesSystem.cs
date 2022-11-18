using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(RoleInfo))]
	[FriendClass(typeof(RoleInfosComponent))]
	[FriendClass(typeof(DlgRoles))]
	public static  class DlgRolesSystem
	{

		public static void RegisterUIEvent(this DlgRoles self)
		{
			self.View.EButton_StartGameButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
			self.View.EButton_CreateRoleButton.AddListenerAsync(() => { return self.OnCreateRoleClickHandler(); });
			self.View.EButton_DeleteRoleButton.AddListenerAsync(() => { return self.OnDeleteRoleClickHandler();});
			self.View.ELoopScrollList_RoleListLoopHorizontalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
			{
				self.OnRoleListRefreshHanlder(transform,index);
			});
		}

		public static void ShowWindow(this DlgRoles self, Entity contextData = null)
		{
			self.RefreshRoleItem();
		}

		public static void RefreshRoleItem(this DlgRoles self)
		{
			int count = self.ZoneScene().GetComponent<RoleInfosComponent>().RoleInfos.Count;
			self.AddUIScrollItems(ref self.ScrollItemSelectroles, count);
			self.View.ELoopScrollList_RoleListLoopHorizontalScrollRect.SetVisible(true,count);
		}

		public static void OnRoleListRefreshHanlder(this DlgRoles self,Transform transform , int index)
		{
			 Scroll_Item_selectrole scrollItemSelectrole = self.ScrollItemSelectroles[index].BindTrans(transform);
			 RoleInfo roleInfo =self.ZoneScene().GetComponent<RoleInfosComponent>().RoleInfos[index];
			 scrollItemSelectrole.EButton_OptionImage.color  = roleInfo.Id == self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId? Color.green : Color.gray;
			 scrollItemSelectrole.E_OptionRoleNameText.SetText(roleInfo.Name);
			 scrollItemSelectrole.EButton_OptionButton.AddListener(() =>
			 {
				 self.OnSelectRoleItemHandler(roleInfo.Id);
			 });
		}

		public static void OnSelectRoleItemHandler(this DlgRoles self, long roleId)
		{
			self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId = long.Parse(roleId.ToString());
			Log.Debug($"当前选择的角色 Id 是：{roleId}");
			self.View.ELoopScrollList_RoleListLoopHorizontalScrollRect.RefillCells();
		}
		public static async ETTask OnCreateRoleClickHandler(this DlgRoles self)
		{
			string name = self.View.EInputField_RoleNameInputField.text;

			if (string.IsNullOrEmpty(name))
			{
				Log.Error("Name is null");
				return;
			}

			try
			{
				int errorCode = await LoginHelper.CreateRole(self.ZoneScene(), name);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				self.RefreshRoleItem();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		public static async ETTask OnDeleteRoleClickHandler(this DlgRoles self)
		{
			if (self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId == 0)
			{
				Log.Error("请选择需要删除的角色");
				return;
			}

			try
			{
				int errorCode = await LoginHelper.DeleteRole(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				self.RefreshRoleItem();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		public static async ETTask OnConfirmClickHandler(this DlgRoles self)
		{
			if (self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId == 0)
			{
				Log.Error("请选择角色");
				return;
			}

			try
			{
				int errorCode = await LoginHelper.GetRelamKey(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}

				errorCode = await LoginHelper.EnterGame(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
	}
}
