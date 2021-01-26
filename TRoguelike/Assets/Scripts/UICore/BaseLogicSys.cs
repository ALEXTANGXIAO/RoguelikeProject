

namespace UI
{
    /**
     * 封装统一的基类，用于实现即使Behavir又是单例的对象
     */
    class BaseLogicSys<T> : ILogicSys where T : new()
    {
        private static T sInstance;

        public static bool HasInstance
        {
            get { return sInstance != null; }
        }
        public static T Instance
        {
            get
            {
                if (null == sInstance)
                {
                    sInstance = new T();
                }

                return sInstance;
            }
        }

        /// <summary>
        /// 单独放在基类里，避免每个logicsystem要实现一堆接口函数
        /// </summary>
        /// <returns></returns>

        public virtual void OnRoleLogout()
        {

        }

        public virtual bool OnInit()
        {
            return true;
        }

        public virtual void OnStart()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnLateUpdate()
        {
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }

        public virtual void OnDrawGizmos()
        {
        }
    }
}
