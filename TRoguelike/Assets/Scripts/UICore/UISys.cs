﻿
using System.Collections.Generic;
using UnityEngine;


namespace UI
{
    class UISys : BaseLogicSys<UISys>
    {
        public const int DesginWidth = 1330;
        public const int DesginHeight = 750;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private UIManager m_mgr;
        private List<IUiController> m_listController = new List<IUiController>();


        public static UIManager Mgr
        {
            get { return UISys.Instance.m_mgr; }
        }



        public override bool OnInit()
        {
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;

            m_mgr = new UIManager();
            m_mgr.Create();

            RegistAllController();
            return true;
        }

        public override void OnUpdate()
        {
            m_mgr.Update();
        }

        public override void OnLateUpdate()
        {
            m_mgr.LateUpdate();
        }


        #region 注册各个UI静态消息处理模块

        private void RegistAllController()
        {
            //AddController<LoginUIController>();
        }


        private void AddController<T>() where T : IUiController, new()
        {
            for (int i = 0; i < m_listController.Count; i++)
            {
                var type = m_listController[i].GetType();
                if (type == typeof(T))
                {
                    Debug.Log("repeat controller type:"+ typeof(T).Name);
                    return;
                }
            }

            var controller = new T();
            m_listController.Add(controller);

            controller.RegUIMessage();
        }

        #endregion
    }
}
