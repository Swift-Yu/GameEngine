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
            string strFilePath = filename.EndsWith(".lua") ? filename : string.Format("{0}.lua", filename);
            strFilePath = strFilePath.Replace("\\", "/");
            readFile = System.IO.File.ReadAllBytes(Application.dataPath + "/"+ strFilePath);
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

        string strFilePath = fileName.EndsWith(".lua")? fileName : string.Format("{0}.lua", fileName);
        readFile = System.IO.File.ReadAllBytes(Application.dataPath + "/" + strFilePath);

        if (m_luaEnv != null && readFile != null)
        {
            object[] objs = m_luaEnv.DoString(readFile, fileName);

            readFile = null;

            return objs;
        }
        Utility.Log.Trace("Error to Do lua file");
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
