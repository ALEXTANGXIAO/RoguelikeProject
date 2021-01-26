using UnityEngine;

namespace UI
{
    /// <summary>
    /// 定义通用的逻辑接口，统一生命期调用
    /// </summary>
    public interface ILogicSys
    {
        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <returns></returns>
        bool OnInit();

        /// <summary>
        /// 销毁系统
        /// </summary>
        void OnDestroy();

        /// <summary>
        /// 初始化后，第一帧统一调用
        /// </summary>
        void OnStart();

        /// <summary>
        /// 更新接口
        /// </summary>
        /// <returns></returns>
        void OnUpdate();

        /// <summary>
        /// 渲染后调用
        /// </summary>
        void OnLateUpdate();

        /// 清理数据接口，切换账号/角色时调用
        /// </summary>
        void OnRoleLogout();

        void OnPause();
        void OnResume();
    }
}
