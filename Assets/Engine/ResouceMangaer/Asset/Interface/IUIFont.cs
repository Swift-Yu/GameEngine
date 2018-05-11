using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public interface IUIFont : IAsset
    {
        Font GetFont();
    }
}
