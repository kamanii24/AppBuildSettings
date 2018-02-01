// =================================
//
//	AppBuildSettingsEditor.cs
//	Created by Takuya Himeji
//
// ==============================

using UnityEngine;
using UnityEditor.Build;
using UnityEditor;

[CustomEditor(typeof(AppBuildSettings))]
public class AppBuildSettingsEditor : Editor, IPreprocessBuild
{
	// Inspector更新
    public override void OnInspectorGUI()
    {
        var buildSettings = target as AppBuildSettings;

		// ビルド対象のタイトルを更新
        buildSettings.buildTargetTitle = (GlobalConfig.AppTitle)EditorGUILayout.EnumPopup("Build Target Title", buildSettings.buildTargetTitle);
        EditorGUILayout.Space();

        for(int i=0; i<buildSettings.settings.Length; i++)
        {
        	AppBuildSettings.Settings stg = buildSettings.settings[i]; // AppSetting取得
			stg.isEditorOpen = EditorGUILayout.Foldout(stg.isEditorOpen, stg.productName); // タイトル名表示

            if (stg.isEditorOpen)
            {
                // ProductName, BundleIDの設定
                EditorGUI.indentLevel++;  // インデント
                stg.productName = (string)EditorGUILayout.TextField("Product Name", stg.productName);
                stg.bundleId = (string)EditorGUILayout.TextField("Bundle ID", stg.bundleId);

                EditorGUILayout.Space();
                foreach(AppBuildSettings.PlatformItem item in stg.items)
                {
                    EditorGUILayout.LabelField(item.platform.ToString());
                    EditorGUI.indentLevel++;  // インデント
                    item.version = (string)EditorGUILayout.TextField("Version", item.version);
                    item.buildVersion = (string)EditorGUILayout.TextField("Build Version", item.buildVersion);
                    item.icon = (Texture2D)EditorGUILayout.ObjectField("App Icon", item.icon, typeof(Texture2D), false);
                    EditorGUI.indentLevel--;  // インデント戻し
                }
                EditorGUI.indentLevel--;  // インデント戻し
            }
        }

        this.serializedObject.ApplyModifiedProperties();
	}


	// ------------
    // ビルド前処理
	// PlayerSettingsの更新
    public void OnPreprocessBuild(UnityEditor.BuildTarget buildTarget, string path)
    {
        var buildSettings = Resources.Load<AppBuildSettings>("AppBuildSettings");
        
        for (int i = 0; i < buildSettings.settings.Length; i++)
        {
            AppBuildSettings.Settings settings = buildSettings.settings[i];
            GlobalConfig.AppTitle appTitle = settings.appTitle;
            PlayerSettings.productName = buildSettings.settings[i].productName;

            if (appTitle == buildSettings.buildTargetTitle)
            {
                foreach(AppBuildSettings.PlatformItem item in settings.items)
                {
                    switch(item.platform)
                    {
                        case AppBuildSettings.Platform.iOS:
                            // BundleID設定
                            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, buildSettings.settings[i].bundleId);
                            // アプリアイコンの設定
                            SetIcons(BuildTargetGroup.iOS, item.icon);
                        break;
                        case AppBuildSettings.Platform.Android:
                            // BundleID設定
                            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, buildSettings.settings[i].bundleId);
                            // アプリアイコンの設定
                            SetIcons(BuildTargetGroup.Android, item.icon);
                            break;
                    }
                }
                // アセット保存
                AssetDatabase.SaveAssets();
                break;
            }
        }
    }

	// アプリアイコンの設定
	private void SetIcons(BuildTargetGroup buildTarget, Texture2D icon)
	{
		// アイコンの反映
		if (icon != null)
		{
			// プラットフォームのアイコンを取得
            Texture2D[] icons = PlayerSettings.GetIconsForTargetGroup(buildTarget);
            // アイコン配列にTexuterを格納
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i] = icon;
            }
            // 取得したアイコン画像をPlayerSettingsに反映
            PlayerSettings.SetIconsForTargetGroup(buildTarget, icons);
		}
	}

    // 実行順
    public int callbackOrder { get { return 0; } }


    // AppBuildSettingsアセットピックアップ
    [MenuItem("Tools/AppBuildSettings")]
    static void SelectionAsset()
    {
        var guids = AssetDatabase.FindAssets("t:AppBuildSettings");
        if (guids.Length == 0)
        {
            throw new System.IO.FileNotFoundException("AppBuildSettings does not found");
        }

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        var obj = AssetDatabase.LoadAssetAtPath<AppBuildSettings>(path);
        EditorGUIUtility.PingObject(obj);
        Selection.activeObject = obj;
    }
}