using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using XLua;

public class LuaManager : Singleton<LuaManager> {

    private LuaEnv m_luaEnv;

    private float m_lastGCTime = 0;

    private const float GC_INTERVAL = 1;

    public LuaEnv luaEnv
    {
        get { return m_luaEnv; }
    }

    //private LuaFunction m_getMemAction = null;

    LuaFunction m_getMemAction = null;

    System.Action<uint> m_applicationFocus = null;
    System.Action<uint> m_applicationQuit = null;

    //
    public double LuaMem
    {
        get;
        private set;
    }

    public bool bReadFromAssetBundle = true;

    private Dictionary<string, bool> m_dicAssetBundle = new Dictionary<string, bool>();
    private Dictionary<string, byte[]> m_dicLuaBytes = new Dictionary<string, byte[]>();

    public override void Initialize()
    {
        base.Initialize();
        //LoadLuaFile("lua/framework.lua");
        m_luaEnv = new LuaEnv();
        InitLuaPath();
        OpenLibs();
        LoadLuaMain();
    }

    public void InitLuaPath()
    {
        m_luaEnv.AddLoader((ref string filename) =>
        {
            //检查后缀是否为.lua
            byte[] readFile = null;
            if (!Application.isEditor)
            {
                //执行一个打包策略，将LuaScripts文件夹下的子目录统一编辑到lua文件名里面，使用“_”链接
                filename = filename.Replace("\\", "_").Replace("/", "_").Replace(".lua", "").ToLower();
                if (!m_dicLuaBytes.TryGetValue(filename, out readFile))
                {
                    Utility.Log.Error("load lua File Failed {0}", filename);
                }
            }
            else
            {
                string strFilePath = filename.EndsWith(".lua") ? filename : string.Format("{0}.lua", filename);
                strFilePath = strFilePath.Replace("\\", "/");
                readFile = System.IO.File.ReadAllBytes(Application.dataPath + "/" + strFilePath);
            }
            return readFile;
        });
    }

    void LoadLuaMain()
    {
        DoFile("LuaScripts/FrameWork/Main");

        System.Action func = m_luaEnv.Global.Get<System.Action>("Main");
        if (func != null)
        {
            func();
            func = null;
        }

        m_applicationFocus = m_luaEnv.Global.Get<System.Action<uint>>("OnApplicationFocus");
        m_applicationQuit = m_luaEnv.Global.Get<System.Action<uint>>("OnApplicationQuit");
        m_getMemAction = m_luaEnv.Global.Get<LuaFunction>("GetMem");
    }

    //移动端加载lua文件
    public void LoadLuaFile(string strFilePath)
    {
        if (!Application.isEditor)
        {
            strFilePath = strFilePath.ToLower();
            if (m_dicAssetBundle.ContainsKey(strFilePath))
            {
                return;
            }
            string strResPath = FileUtils.Instance().FullPathFileName(ref strFilePath, FileUtils.UnityPathType.UnityPath_CustomPath);
            AssetBundle ab = AssetBundle.LoadFromFile(strResPath);
            //如果发现外部环境没有打包资源，则直接从包内加载资源
            if (ab == null)
            {
                strResPath = FileUtils.Instance().FullPathFileName(ref strFilePath, FileUtils.UnityPathType.UnityPath_StreamAsset);
                ab = AssetBundle.LoadFromFile(strResPath);
            }
            if (ab == null)
            {
                Utility.Log.Error("Load lua failed {0}", strFilePath);
                return;
            }
            Utility.Log.Trace("Load lua success {0}", strFilePath);
            m_dicAssetBundle.Add(strFilePath, true);
            //多个lua文件会按照模块进行打包，而不是单个打包，加载的时候一起加载,使用textAsset进行储存
            TextAsset[] ScirptTexts = ab.LoadAllAssets<TextAsset>();
            for (int i = 0; i < ScirptTexts.Length; i++)
            {
                m_dicLuaBytes.Add(ScirptTexts[i].name, ScirptTexts[i].bytes);
            }
            //ab包使用完要及时卸载,包括内存里面已经加载出来的资源块
            ab.Unload(true);
        }
    }

    void OpenLibs()
    {
        m_luaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
    }

    public void Update()
    {
        if (m_luaEnv == null)
        {
            return;
        }
        if (Time.time - m_lastGCTime > GC_INTERVAL)
        {
            luaEnv.Tick();
            m_lastGCTime = Time.time;
        }
    }

    public object[] DoFile(string fileName)
    {
        byte[] readFile = null;

        if (!Application.isEditor)
        {
            fileName = fileName.Replace("\\", "_").Replace("/", "_").Replace(".lua", "").ToLower();

            if (!m_dicLuaBytes.TryGetValue(fileName, out readFile))
            {
                Utility.Log.Error("Load Lua File Failed {0}", fileName);
            }
        }
        else
        {
            string strFilePath = fileName.EndsWith(".lua") ? fileName : string.Format("{0}.lua", fileName);
            readFile = System.IO.File.ReadAllBytes(Application.dataPath + "/" + strFilePath);
            Utility.Log.Trace("Error to Do lua file");
        }
        if (m_luaEnv != null && readFile != null)
        {
            object[] objs = m_luaEnv.DoString(readFile, fileName);

            readFile = null;

            return objs;
        }
        return null;
    }

    public object[] DoString(string str, string tag = "")
    {
        return m_luaEnv.DoString(str, tag);
    }

    public T GetLuaValue<T>(string funcName)
    {
        return m_luaEnv.Global.Get<T>(funcName);
    }

    public void Close()
    {
        m_applicationFocus = null;
        m_applicationQuit = null;
        m_getMemAction = null;
        m_luaEnv.Dispose();
        m_luaEnv = null;
    }

}
