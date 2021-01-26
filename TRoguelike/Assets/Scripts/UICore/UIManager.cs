using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    internal enum WindowType
    {
        UINormal = 0,
        UITop = 1,
        UIMax = 2
    };

    internal class UIWindowStack
    {
        public WindowType m_windowtype;
        public int m_baseOrder = 0;
        public List<uint> m_windowList = new List<uint>();
        public Transform m_parentTrans;
        

        public int FindIndex(uint windowId)
        {
            for (int i = 0; i < m_windowList.Count; i++)
            {
                if (m_windowList[i] == windowId)
                {
                    return i;
                }
            }

            return -1;
        }
    }

    public class UIManager
    {
        private float m_zOffset; //z轴的修正值，用于保证模型显示的时候不会出现模型和窗口重叠的问题

        private Transform m_canvasTrans;
        public Canvas m_canvas;
        private Dictionary<uint, UIWindow> m_allWindow = new Dictionary<uint, UIWindow>();

        /// <summary>
        /// 不同显示顺序的的窗口列表
        /// </summary>
        private UIWindowStack[] m_listWindowStack = new UIWindowStack[((int)WindowType.UIMax)];

        /// <summary>
        /// 类型到实例的索引
        /// </summary>
        private static Dictionary<string, UIWindow> m_typeToInst = new Dictionary<string, UIWindow>();
        private GameObject m_uiManagerGo;
        private Transform m_uiManagerTransform;

        public void Create()
        {
            m_uiManagerGo = TGame.TResources.AllocGameObject("UI");
            GameObject.DontDestroyOnLoad(m_uiManagerGo);

            m_uiManagerTransform = m_uiManagerGo.transform;

            //m_uiCamera = GameObject.FindChildComponent<Camera>(m_uiManagerTransform, "Camera");
            Canvas canvas = m_uiManagerGo.GetComponentInChildren<Canvas>();
            if (canvas != null)
            {
                m_canvas = canvas;
                m_canvasTrans = canvas.transform;
            }

            var notchingTrans = m_canvasTrans.Find("UI");
            var windowRoot = m_canvasTrans;
            windowRoot = notchingTrans.Find("UI");

            int baseOrder = 1000;
            for (int i = 0; i < (int)WindowType.UIMax; i++)
            {
                m_listWindowStack[i] = new UIWindowStack();
                m_listWindowStack[i].m_windowtype = (WindowType)i;
                m_listWindowStack[i].m_baseOrder = baseOrder;
                m_listWindowStack[i].m_parentTrans = windowRoot;
                baseOrder += 1000;
            }
        }

        public void Update()
        {
            //throw new NotImplementedException();
        }

        public void LateUpdate()
        {
            //throw new NotImplementedException();
        }



        #region oper function

        public string GetWindowTypeName<T>()
        {
            string typeName = typeof(T).Name;
            return typeName;
        }

        public string GetWindowTypeName(UIWindow window)
        {
            string typeName = window.GetType().Name;
            return typeName;
        }

        public T ShowWindow<T>() where T : UIWindow, new()
        {
            string typeName = GetWindowTypeName<T>();


            T window = GetUIWindowByType(typeName) as T;
            if (window == null)
            {
                window = new T();
                if (!CreateWindowByType(window, typeName))
                {
                    return null;
                }
            }

            ShowWindow(window, -1);
            return window;
        }

        public void CloseWindow<T>() where T : UIWindow
        {
            string typeName = GetWindowTypeName<T>();
            CloseWindow(typeName);
        }

        public void CloseWindow(UIWindow window)
        {
            window.Destroy();
        }

            private bool CreateWindowByType(UIWindow window, string typeName)
        {
            GameObject uiGo = null;

            uiGo.name = typeName;

            window.AllocWindowId();

            RectTransform rectTrans = uiGo.transform as RectTransform;

            rectTrans.localRotation = Quaternion.identity;
            rectTrans.localScale = Vector3.one;


            m_typeToInst[typeName] = window;
            m_allWindow[window.WindowId] = window;
            return true;
        }
        public UIWindow GetUIWindowByType(string typeName)
        {
            UIWindow window;
            if (m_typeToInst.TryGetValue(typeName, out window))
            {
                return window;
            }

            return null;
        }

        #endregion
        private void ShowWindow(UIWindow window, int showIndex)
        {

        }

        public void CloseWindow(string typeName)
        {

        }
    }
}