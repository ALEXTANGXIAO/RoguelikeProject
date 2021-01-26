using System;
using UnityEngine;

namespace UI
{
    public class UIWindow
    {
        protected Canvas m_canvas;

        private uint m_windowId = 0;

        private static uint m_nextWindowId = 0;

        public uint WindowId
        {
            get { return m_windowId; }
        }

        protected UIManager m_ownUIManager = null;

        protected UIManager UIMgr
        {
            get
            {
                return m_ownUIManager;
            }
        }

        public void AllocWindowId()
        {
            if (m_nextWindowId == 0)
            {
                m_nextWindowId++;
            }

            m_windowId = m_nextWindowId++;
        }

        public bool Create(UIManager uiMgr,GameObject uiGO)
        {
            if (m_canvas != null)
            {
                m_canvas.overrideSorting = true;
            }
            m_ownUIManager = uiMgr;
            RegisterEvent();
            OnCreate();

            return true;
        }


        protected virtual void RegisterEvent()
        {
        }

        protected virtual void OnCreate()
        {
        }

        protected virtual void OnDestroy()
        {
        }

        public virtual void Close()
        {

            var mgr = UIMgr;
            if (mgr != null)
            {
                mgr.CloseWindow(this);
            }
        }

        internal void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}