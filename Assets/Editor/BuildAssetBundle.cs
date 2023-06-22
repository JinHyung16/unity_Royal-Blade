using UnityEditor;
using System.IO;

public class BuildAssetBundle
{
    [MenuItem("Assets/Build AssetBundles")]
    private static void BuildAssetBundles()
    {
        //string assetBundleDirectory = "Assets/AssetBundles";
        
        string assetBundleDirectory = "Assets/StreamingAssets"; //StreamingAsset�� �ξ�� android ���� �� ���� �����ͷ� ��� ����
        if ( !Directory.Exists(assetBundleDirectory) )
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles (assetBundleDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        EditorUtility.DisplayDialog("���� ���� ����", "���� ���� ���带 �Ϸ��߽��ϴ�", "���� �Ϸ�");
    }
}
