# Unity 客户端

### 架构

### GameLauncher

```c#
public class GameLaunch : UnitySingleton<GameLaunch>
{
    void Start()
    {
        //初始化游戏框架代码
        this.gameObject.AddComponent<ResMgr>();
        this.gameOjbect.AddComponent<EventMgr>();
        this.gameOjbect.AddComponent<UIMgr>();
        // Other Managers
        
        //初始化游戏逻辑模块代码
        this.gameObject.AddComponent<GameApp>();
        
        //检查更新资源
        
        //进入游戏逻辑入口
        GameApp.Instance.GameStart();
    }
}
```



### 单例

```c#
public class UnitySingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
        	if (_intance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null) 
                {
                    GameObject obj = new GameObject();
                    _instance = (T)obj.AddComponent(typeof(T));
                    //_instance = obj.AddComponent<T>();
                    obj.hideFlags = HideFlags.DontSave; // 该对象不保存到场景。**加载新场景时，也不会销毁它**
                    obj.name = typeof(T).name;
                }
            }    
          	return _instance;
        }
    }
    
    public virtual void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null) 
        {
            _instance = this as T;
        }
        else 
        {
            GameObject.Destory(this.gameObject);
        }
    }
}
```

