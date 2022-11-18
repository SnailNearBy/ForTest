using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(ServerInfo))]
	[FriendClass(typeof(ServerInfosComponent))]
	[FriendClass(typeof(DlgServer))]
	public static  class DlgServerSystem
	{

		public static void RegisterUIEvent(this DlgServer self)
		{
			self.View.EButton_EnterButton.AddListenerAsync(() => { return self.OnConfirmClickHandler();});
			self.View.ELoopScrollList_ServerListLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
			{
				self.OnScrollItemRefreshHandler(transform,index);
			});
		}

		public static void ShowWindow(this DlgServer self, Entity contextData = null)
		{
			int count = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList.Count;
			self.AddUIScrollItems(ref self.ScrollItemServeroptions,count);
			self.View.ELoopScrollList_ServerListLoopVerticalScrollRect.SetVisible(true,count);
		}

		public static void HideWindow(this DlgServer self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemServeroptions);
		}
		
		public static void OnScrollItemRefreshHandler(this DlgServer self ,Transform transform ,int index)
		{
			Scroll_Item_serveroption serveroption = self.ScrollItemServeroptions[index].BindTrans(transform);
			ServerInfo info = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList[index];
			serveroption.EButton_OptionImage.color =
					info.Id == self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId? Color.red : Color.cyan;
			serveroption.E_OptionTextText.SetText(info.ServerName);
			serveroption.EButton_OptionButton.AddListener(() =>
			{
				self.OnSelectServerItemHandler(info.Id);
			});
		}

		public static void OnSelectServerItemHandler(this DlgServer self, long serverId)
		{
			self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId = int.Parse(serverId.ToString());
			Log.Debug($"当前选择的服务器 Id 是：{serverId}");
			self.View.ELoopScrollList_ServerListLoopVerticalScrollRect.RefillCells();
		}
		
		public static async ETTask OnConfirmClickHandler(this DlgServer self)
		{
			bool isSelect = self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId != 0;
			if (!isSelect)
			{
				Log.Error("请选择区服");
				return;
			}

			try
			{
				int errorCode = await LoginHelper.GetRoles(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Roles);
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Server);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
	}
}
