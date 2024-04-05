using UnityEngine;

namespace Core
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;
		private static bool _onceInit = false;

		public static T Instance
		{
			get
			{
				if (!_instance)
				{
					_instance = (T)FindObjectOfType(typeof(T));
					
					if (!_instance)
					{
						GameObject obj = new GameObject(typeof(T).Name, typeof(T));
						_instance = obj.GetComponent<T>();
					}
				}
				return _instance;
			}
		}
		public void Awake()
		{
			if (_onceInit)
				return;
			DontDestroyOnLoad(gameObject);
			AwakeInit();
			_onceInit = true;
		}

		protected virtual void AwakeInit()
		{
		}
	}
}