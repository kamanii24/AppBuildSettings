// =================================
//
//	GlobalConfig.cs
//	Created by Takuya Himeji
//
// =================================

using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    // アプリタイトル
    public enum AppTitle
    {
        App1,
        App2,
        App3,
        App4
    }
	public static AppTitle ActiveTitle
	{
		get 
		{
			var settings = Resources.Load<AppBuildSettings>("AppBuildSettings");
			return settings.buildTargetTitle;
		}
	}
}