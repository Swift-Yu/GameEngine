using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public interface IUIAtlas : IAsset
    {
        Sprite GetSprite(string strName);
       
    }
}
