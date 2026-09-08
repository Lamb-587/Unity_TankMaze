using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHelper
{
    /// <summary>
    /// 判断场景是否已添加到 Build Settings 中
    /// </summary>
    public static bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            // 从路径中提取场景名（不含 .unity 后缀）
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return true;
        }
        return false;
    }
}