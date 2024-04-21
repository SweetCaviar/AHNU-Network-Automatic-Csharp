using System;
using System.Net;

public class RequestSender
{
    public static string SendGetRequest(string url, string userAccount, string userPassword, string provider)
    {
        try
        {
            // 构建查询参数
            string fullUrl = $"{url}?user_account={userAccount}&user_password={userPassword}&provider={provider}";

            // 创建 Web 请求
            WebClient client = new();

            // 发送 GET 请求并获取响应
            string response = client.DownloadString(fullUrl);
            response = client.DownloadString(fullUrl);

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"发送 GET 请求发生错误：{ex.Message}");
            return null;
        }
    }
}
