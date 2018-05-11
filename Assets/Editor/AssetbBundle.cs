using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AssetbBundle {
	[MenuItem("Build/BuildAsset")]
	static void BuildAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("TempResource",BuildAssetBundleOptions.UncompressedAssetBundle,EditorUserBuildSettings.activeBuildTarget);
	}
}
