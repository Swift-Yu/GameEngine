using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;

public static class XluaGenConfig
{

    #region dotween

    /// <summary>
    /// dotween的扩展方法在lua中调用
    /// </summary>
    [LuaCallCSharp]
    [ReflectionUse]
    public static List<Type> dotween_lua_call_cs_list = new List<Type>()
    {
        typeof(DG.Tweening.AutoPlay),
        typeof(DG.Tweening.AxisConstraint),
        typeof(DG.Tweening.Ease),
        typeof(DG.Tweening.LogBehaviour),
        typeof(DG.Tweening.LoopType),
        typeof(DG.Tweening.PathMode),
        typeof(DG.Tweening.PathType),
        typeof(DG.Tweening.RotateMode),
        typeof(DG.Tweening.ScrambleMode),
        typeof(DG.Tweening.TweenType),
        typeof(DG.Tweening.UpdateType),

        typeof(DG.Tweening.DOTween),
        typeof(DG.Tweening.DOVirtual),
        typeof(DG.Tweening.EaseFactory),
        typeof(DG.Tweening.Tweener),
        typeof(DG.Tweening.Tween),
        typeof(DG.Tweening.Sequence),
        typeof(DG.Tweening.TweenParams),
        typeof(DG.Tweening.Core.ABSSequentiable),


        typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),
        typeof(DG.Tweening.Core.DOSetter<float>),

        typeof(DG.Tweening.TweenCallback),
        typeof(DG.Tweening.TweenExtensions),
        typeof(DG.Tweening.TweenSettingsExtensions),
        typeof(DG.Tweening.ShortcutExtensions),
        typeof(DG.Tweening.ShortcutExtensions43),
        typeof(DG.Tweening.ShortcutExtensions46),
        typeof(DG.Tweening.ShortcutExtensions50),
       
        //dotween pro 的功能
        //typeof(DG.Tweening.DOTweenPath),
        //typeof(DG.Tweening.DOTweenVisualManager),
    };
    #endregion

    #region UGUI
    [LuaCallCSharp]
    public static List<Type> ugui_lua_call_cs_list = new List<Type>()
    {
        typeof(UnityEngine.RectTransform),
        typeof(UnityEngine.UI.Image),
        typeof(UnityEngine.UI.Button),
        typeof(UnityEngine.UI.Toggle),
        typeof(UnityEngine.UI.Text),
        typeof(UnityEngine.Font),
        typeof(UnityEngine.Sprite),
        typeof(UnityEngine.UI.RawImage),
        typeof(UnityEngine.UI.Slider),
        typeof(UnityEngine.UI.Scrollbar),
        typeof(UnityEngine.UI.ScrollRect),
        typeof(UnityEngine.UI.ToggleGroup),
        typeof(UnityEngine.UI.GridLayoutGroup),
        typeof(UnityEngine.UI.LayoutElement),
        typeof(UnityEngine.UI.InputField),
        typeof(UnityEngine.UI.GraphicRaycaster),
        typeof(UnityEngine.TextAnchor),
        typeof(UnityEngine.SpriteAlignment),
        typeof(UnityEngine.TextMesh),

    };

    #endregion
    #region ENGINE
    [LuaCallCSharp]
    [ReflectionUse]
    public static List<Type> engine_lua_call_cs_list = new List<Type>()
    {
        typeof(Engine.IAsset),
        typeof(Engine.IGameObject),
        typeof(Engine.IUIAtlas),
        typeof(Engine.ISprite),
        typeof(Engine.IUIFont),
        typeof(Engine.ITexture),
        typeof(Engine.IEffect),
        typeof(Engine.IPrefab),
        typeof(Utility.Log),
        typeof(Engine.AssetManager),
        typeof(Engine.NetService),
        typeof(Engine.AudioManager),
        typeof(Engine.NetWorkError),
        typeof(Engine.TaskPriority),

    };

    #endregion
    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
                typeof(System.Object),
                typeof(UnityEngine.Object),
                typeof(Vector2),
                typeof(Vector3),
                typeof(Vector4),
                typeof(Quaternion),
                typeof(Color),
                typeof(Color32),
                typeof(Time),
                typeof(GameObject),
                typeof(Component),
                typeof(Behaviour),
                typeof(Transform),
                typeof(Animation),
                typeof(Renderer),
                typeof(MeshRenderer),
                typeof(Material),
                typeof(Texture),
                typeof(Rect),
                typeof(AnimationCurve),
                typeof(AnimationClip),
                typeof(MonoBehaviour),
                typeof(UnityEngine.Random),
                typeof(UnityEngine.PlayerPrefs),
                typeof(UnityEngine.Space),
                typeof(ShadowQuality),
                typeof(RenderTexture),
                typeof(RenderTextureFormat),
                typeof(Camera),
            };

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
                typeof(Action),
                
                typeof(Action<LuaTable,uint>),
                typeof(Action<LuaTable, GameObject,Vector2>),            

                typeof(Action<uint>),
                typeof(Action<string>),
                typeof(Action<double>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(System.Collections.IEnumerator),
                typeof(Engine.ConnectCallback),
                typeof(Engine.DisconnectCallback),
                typeof(Engine.CreateGameObjectEvent),
                typeof(Engine.ReceiveMsgCallback),
                typeof(Engine.SendHttpsCallback),
                typeof(DG.Tweening.TweenCallback),
                typeof(DG.Tweening.Core.DOSetter<float>),
                typeof(Engine.CreateAtlasEvent),
                typeof(Engine.CreateGameObjectEvent),
                typeof(Engine.CreateTextureEvent),
                typeof(Engine.CreateFontEvent),
                typeof(Engine.CreateSpriteEvent),
            };

    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"UnityEngine.WWW", "movie"},
                new List<string>(){"UnityEngine.UI.Text", "OnRebuildRequested"},
                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
                new List<string>(){"UnityEngine.Light", "areaSize"},
                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
    #if !UNITY_WEBPLAYER
                new List<string>(){"UnityEngine.Application", "ExternalEval"},
    #endif
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
            };
}
