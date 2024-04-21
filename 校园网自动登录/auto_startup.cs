using System;
using IWshRuntimeLibrary; // 添加这个命名空间

public class AutoStartupManager
{
    private const string ShortcutName = "AHNUAutostart";

    public static bool ShortcutExists()
    {
        string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        string shortcutPath = System.IO.Path.Combine(startupFolderPath, $"{ShortcutName}.lnk");
        return System.IO.File.Exists(shortcutPath);
    }

    public static void CreateShortcut(string target, string startIn = null)
    {
        try
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = System.IO.Path.Combine(startupFolderPath, $"{ShortcutName}.lnk");

            // 创建 Shell 对象
            WshShell shell = new WshShell();

            // 创建快捷方式对象
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

            // 设置目标路径
            shortcut.TargetPath = target;

            // 设置起始路径
            if (startIn != null)
            {
                shortcut.WorkingDirectory = startIn;
            }

            // 保存快捷方式
            shortcut.Save();

            Console.WriteLine("已成功将脚本添加到自启动文件夹！");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"创建自启动快捷方式发生错误：{ex.Message}");
        }
    }
}
