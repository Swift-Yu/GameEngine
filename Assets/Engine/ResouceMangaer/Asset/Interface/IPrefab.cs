using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public interface IPrefab : IAsset
    {
        UnityEngine.Object GetObj();
    }

}