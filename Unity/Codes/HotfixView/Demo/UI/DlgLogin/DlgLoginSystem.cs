using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgLoginSystem
	{ 

		public static void RegisterUIEvent(this DlgLogin self)
		{
			self.View.E_LoginButton.AddListenerClickAsync(() => { return self.OnLoginClickHandler();});
		}

		public static void ShowWindow(this DlgLogin self, Entity contextData = null)
		{
			self.View.ESCommonUITest.SetLabelContent("登陆公共UI");
		}
		
		public static async ETTask OnLoginClickHandler(this DlgLogin self)
		{

			try
			{
				 int errorCode =await LoginHelper.Login(
					self.DomainScene(), 
					ConstValue.LoginAddress, 
					self.View.E_AccountInputField.GetComponent<InputField>().text, 
					self.View.E_PasswordInputField.GetComponent<InputField>().text);
				 if (errorCode != ErrorCode.ERR_Success)
				 {
					 Log.Error(errorCode.ToString());
					 return;
				 }
				 //TODO 显示登陆之后的页面逻辑
				 Log.Debug("登陆成功！");
				 //获取服务器列表
				 errorCode = await LoginHelper.GetServerInfo(self.ZoneScene());
				 if (errorCode != ErrorCode.ERR_Success)
				 {
					 Log.Error(errorCode.ToString());
					 return;
				 }
				 // LoginHelper.LoginTest(self.DomainScene(),ConstValue.LoginAddress).Coroutine();
				  
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Server);
			}
			catch (Exception e)   
			{
				Log.Error(e.ToString());
			}
		
			
			//self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Test);
			//self.ZoneScene().GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_Login);

			await ETTask.CompletedTask;
		}
		
		public static void HideWindow(this DlgLogin self)
		{

		}
		
	}
}
