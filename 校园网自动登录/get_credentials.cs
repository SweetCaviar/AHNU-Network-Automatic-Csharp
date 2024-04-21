using System;
using System.IO;

public class CredentialsManager
{
    public static void SaveCredentials(string userAccount, string userPassword, string provider, string sourceFolderPath)
    {
        // 确保凭据不为空
        if (userAccount == null || userPassword == null || provider == null)
        {
            throw new ArgumentNullException("凭据信息不能为空");
        }

        try
        {
            // 构建用户数据文件路径
            string credentialsFilePath = Path.Combine(sourceFolderPath, "AHNUcredentials.txt");

            // 将凭据信息写入文件
            using (StreamWriter writer = new StreamWriter(credentialsFilePath))
            {
                writer.WriteLine($"账户: {userAccount}");
                writer.WriteLine($"密码: {userPassword}");
                writer.WriteLine($"运营商: {provider}");
            }

            Console.WriteLine("凭据信息保存成功！");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"保存凭据信息时发生错误：{ex.Message}");
        }
    }

    public static (string, string, string) LoadCredentials()
    {
        // 默认情况下，返回空凭据信息
        string userAccount = null;
        string userPassword = null;
        string provider = null;

        try
        {
            // 获取源程序所在文件夹的路径
            string sourceFolderPath = AppDomain.CurrentDomain.BaseDirectory;

            // 构建用户数据文件路径
            string credentialsFilePath = Path.Combine(sourceFolderPath, "AHNUcredentials.txt");

            // 检查用户数据文件是否存在
            if (File.Exists(credentialsFilePath))
            {
                // 从文件中读取凭据信息
                string[] lines = File.ReadAllLines(credentialsFilePath);
                if (lines.Length >= 3)
                {
                    userAccount = lines[0].Split(':')[1].Trim();
                    userPassword = lines[1].Split(':')[1].Trim();
                    provider = lines[2].Split(':')[1].Trim();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"加载凭据信息时发生错误：{ex.Message}");
        }

        // 返回凭据信息
        return (userAccount, userPassword, provider);
    }
}
