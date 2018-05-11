using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public interface ITexture : IAsset
    {
        Texture GetTexture();
    }
}
