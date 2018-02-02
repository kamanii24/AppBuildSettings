# AppBuildSettings
#### 概要
ひとつのプロジェクトで複数のアプリとして管理するときに便利なエディタ拡張です。<br>
現在の対象プラットフォームはiOS、Androidのみです。<br>

# 使い方
**AppBuildSettings**の対象のプラットフォーム用の情報を入力し、ビルドするだけです。<br>
各情報はビルド前処理**OnPreprocessBuild**で各プラットフォームのPlayerSettingsの内容を上書きます。<br>
<br>
![Imgur](https://i.imgur.com/pk7hpHM.png)
<br>

## ビルド環境
Unity 2017.3.0p3<br>
macOS High Sierra 10.13.3
