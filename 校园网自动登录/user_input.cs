using System;

public class UserInputManager
{
    public static (string userAccount, string userPassword, string provider) GetUserInput()
    {
        try
        {
            Console.WriteLine("请输入您的账户：");
            string userAccount = Console.ReadLine();

            Console.WriteLine("请输入您的密码：");
            string userPassword = Console.ReadLine();

            Console.WriteLine("请选择您的网络运营商（1. 中国电信，2. 中国联通，3. 中国移动）：");
            int providerChoice = int.Parse(Console.ReadLine());
            string provider;
            switch (providerChoice)
            {
                case 1:
                    provider = "telecom";
                    break;
                case 2:
                    provider = "unicom";
                    break;
                case 3:
                    provider = "cmcc";
                    break;
                default:
                    provider = "telecom";
                    break;
            }

            return (userAccount, userPassword, provider);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"获取用户输入发生错误：{ex.Message}");
            return (null, null, null);
        }
    }
}
