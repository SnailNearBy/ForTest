using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgTest))]
	public static  class  DlgTestSystem
	{

		public static void RegisterUIEvent(this DlgTest self)
		{
		   self.View.E_LoginButton.AddListener(self.OnEnterClickHandler);
		   self.View.EButton_TestButton.AddListener(self.OnChangeSelfLabelHandler);
		   self.View.ELoopScrollList_TestLoopHorizontalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
		   {
			   self.OnLoopListItemRefreshHandler(transform, index);
		   });
		}

		public static void ShowWindow(this DlgTest self, Entity contextData = null)
		{
			self.View.ESCommonUITest.SetLabelContent("测试界面");

			int count = 10;
			self.AddUIScrollItems(ref self.ScrollItemServerpsDic ,count);
			self.View.ELoopScrollList_TestLoopHorizontalScrollRect.SetVisible(true , count);
		}

		public static void HideWindows(this DlgTest self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemServerpsDic);	
		}
		
		public static void OnChangeSelfLabelHandler(this DlgTest self)
		{
			self.View.ELabel_TextText.text = "成功！";
		}
		public static void OnEnterClickHandler(this DlgTest self)
		{
			Log.Debug(self.ToString());
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Login);
			self.ZoneScene().GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_Test);
		}

		public static void OnLoopListItemRefreshHandler(this DlgTest self , Transform transform , int index)
		{
			// if(self.ScrollItemServerpsDic.TryGetValue(index , out Scroll_Item_serverp item))
			// {
			// 	item.uiTransform = transform;
			// 	item.ELabel_setverpText.text = index.ToString();
			// }
 
			 Scroll_Item_serverp scrollItemServerp =self.ScrollItemServerpsDic[index].BindTrans(transform);
			 scrollItemServerp.ELabel_setverpText.text = index.ToString();
		}
	}
}
