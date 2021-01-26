using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIMono : MonoBehaviour
    {
        private List<ILogicSys> m_listLogicMgr;

        private void Awake()
        {
            UISys.Instance.OnInit();
        }
        void Start()
        {
            UISys.Instance.OnStart();
        }
        void Update()
        {
            UISys.Instance.OnUpdate();
        }
    }
}