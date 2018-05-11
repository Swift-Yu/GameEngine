using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public interface ISprite : IAsset
    {
        Sprite GetSprite();
    }
}