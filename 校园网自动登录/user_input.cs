using System;

public class UserInputManager
{
    public static (string userAccount, string userPassword, string provider) GetUserInput()
    {
        try
        {
            string userAccount, userPassword, provider;

            // 显示欢迎信息
            DisplayWelcomeMessage();
            WaitForEnter();

            // 清空控制台并打印使用手册
            Console.Clear();
            DisplayHandbook();
            WaitForEnter();

            // 清空控制台并打印注意事项
            Console.Clear();
            DisplayNote();
            WaitForEnter();

            Console.Clear();

            // 循环直到输入有效的账户（数字）
            do
            {
                Console.WriteLine("请输入您的账户（必须11位为数字）：");
                userAccount = Console.ReadLine().Trim(); // 去除首尾空格
            } while (!IsValidAccount(userAccount));

            // 循环直到输入有效的密码
            do
            {
                Console.WriteLine("请输入您的密码：");
                userPassword = Console.ReadLine(); // 不进行额外验证
            } while (string.IsNullOrWhiteSpace(userPassword));

            // 循环直到输入有效的运营商
            do
            {
                Console.WriteLine("请选择您的网络运营商（1. 中国电信，2. 中国联通，3. 中国移动）：");
                string providerChoice = Console.ReadLine();

                switch (providerChoice)
                {
                    case "1":
                        provider = "telecom";
                        break;
                    case "2":
                        provider = "unicom";
                        break;
                    case "3":
                        provider = "cmcc";
                        break;
                    default:
                        Console.WriteLine("请选择有效的运营商");
                        provider = null; // 重新循环选择
                        break;
                }
            } while (string.IsNullOrWhiteSpace(provider));

            return (userAccount, userPassword, provider);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"获取用户输入发生错误：{ex.Message}");
            return (null, null, null);
        }
    }

    // 判断字符串是否为 11 位数字
    private static bool IsValidAccount(string input)
    {
        return input.Length == 11 && IsNumeric(input);
    }

    // 判断字符串是否由数字构成
    private static bool IsNumeric(string input)
    {
        foreach (char c in input)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }

    // 显示欢迎信息
    private static void DisplayWelcomeMessage()
    {
        Console.WriteLine("欢迎使用安徽师范大学校园网自动登录程序！\n");
        Console.WriteLine("最后一次更新日期：2024-04-23.\n");
        Console.WriteLine("当前版本：3.0.0\n");
        Console.WriteLine("作者：[SweetCaviar]");
    }

    // 显示使用手册
    private static void DisplayHandbook()
    {
        Console.WriteLine("使用手册：\n\n" +
            "请您务必在使用前阅读使用手册！！！\n\n" +
            "1.请您将程序安装在显眼的位置，并记住其所在文件夹。\n\n" +
            "2.请确保正确输入账户、密码和运营商信息，不要输入其他字符。\n\n" +
            "3.第一次运行时，程序会尝试将自身添加进自启动文件夹。如果提示添加失败，请使用管理员权限运行程序。\n\n" +
            "4.本程序会在其所在文件夹内生成两个文件：" +
            "\n    - AHNUcredentials.txt：保存用户信息。" +
            "\n    - app.log：保存程序日志，请注意查看。\n\n" +
            "5.程序会在开机时自启动一次。如果后续断网需要重新连接，请再次运行本程序即可。");
    }

    // 显示注意事项
    private static void DisplayNote()
    {
        Console.WriteLine("注意事项：\n\n" +
            "1.本程序完全免费！如果您通过付费途径获得本程序，与作者无关，请立即维权。\n\n" +
            "2.请务必输入正确的账户、密码和运营商信息，否则将无法成功登录。\n\n" +
            "3.如果在使用过程中遇到任何错误或有更好的建议，请欢迎与我联系：" +
            "\n    联系邮箱：sweettcaviar@outlook.com\n\n" +
            "4.如果本程序对您有所帮助，请访问 GitHub 给予一个 Star，这对我很有帮助！" +
            "当然，您也可以选择在 GitHub 上提出 issue。" +
            "\n    网址：https://github.com/SweetCaviar/AHNU-Campus-Network-Automatic-Login-Csharp\n\n" +
            "5.如果您想再次弹出此信息，只需删除程序文件夹中的 AHNUcredentials.txt 文件。\n\n" +
            "6.本程序暂时不会联网更新。\n\n" +
            "7.如果您想卸载本程序，\n" +
            "    1) 请打开\"开始菜单\"，选择\"卸载\"，等待程序完成。\n" +
            "    2）删除安装文件夹，卸载完毕。");
    }

    // 等待用户按下回车键
    private static void WaitForEnter()
    {
        Console.WriteLine("\n按回车键继续...");
        Console.ReadLine();
    }
}
