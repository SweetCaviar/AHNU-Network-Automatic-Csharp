using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            // 获取源程序所在文件夹的路径
            string sourceFolderPath = AppDomain.CurrentDomain.BaseDirectory;

            // 设置日志文件路径
            string logFilePath = Path.Combine(sourceFolderPath, "app.log");

            // 创建或追加日志文件
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                // 尝试从本地文件中加载凭据
                (string userAccount, string userPassword, string provider) = CredentialsManager.LoadCredentials();

                if (userAccount == null || userPassword == null)
                {
                    // 如果本地文件中没有凭据，则获取用户输入
                    (userAccount, userPassword, provider) = UserInputManager.GetUserInput();

                    // 保存凭据到本地文件
                    CredentialsManager.SaveCredentials(userAccount, userPassword, provider, sourceFolderPath);
                }

                // 检查并创建自启动快捷方式
                try
                {
                    if (!AutoStartupManager.ShortcutExists())
                    {
                        AutoStartupManager.CreateShortcut(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"创建自启动快捷方式发生错误：{ex.Message}");
                    writer.WriteLine($"创建自启动快捷方式发生错误：{ex.Message}");
                }

                // 示例 URL
                string baseUrl = "http://rz.ahnu.edu.cn:801/eportal/portal/login";

                // 发送 GET 请求并获取响应
                string response = null;

                // 发送 GET 请求并获取响应，最多尝试三次
                for (int i = 0; i < 3; i++)
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    Console.WriteLine($"{timestamp} 正在发送第 {i + 1} 次请求...");

                    response = RequestSender.SendGetRequest(baseUrl, userAccount, userPassword, provider);

                    if (response != null && (response.Contains("Portal协议认证成功") || response.Contains("已经在线！")))
                    {
                        break;
                    }

                    Task.Delay(1000); // 等待1秒
                }

                string finalTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine($"{finalTimestamp} - 响应内容：{response}");
                writer.WriteLine($"{finalTimestamp} - 响应内容：{response}");

                // 关闭日志文件写入器
                writer.Close();
            }

            // 提示用户按任意键退出
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Console.WriteLine($"{timestamp} - 发生错误：{ex.Message}");
        }
    }
}
