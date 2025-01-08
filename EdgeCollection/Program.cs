using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // 初始化 Edge 浏览器
        var options = new EdgeOptions();
        options.AddArgument("--user-data-dir=C:\\Users\\qctol\\AppData\\Local\\Microsoft\\Edge\\User Data"); // 指定用户数据目录
        options.AddUserProfilePreference("download.default_directory", @"E:\Project\EdgeCollection");
        // 初始化 Edge 浏览器
        IWebDriver driver = new EdgeDriver(options);


        try
        {
            // 打开 Edge 浏览器中的集锦页面
            driver.Navigate().GoToUrl("edge://collections");

            // 等待页面加载
            Thread.Sleep(5000);

            // 获取所有集锦
            var collections = driver.FindElements(By.ClassName("collection-name"));

            foreach (var collection in collections)
            {
                string collectionName = collection.Text;
                Console.WriteLine($"处理集锦: {collectionName}");

                // 点击集锦
                collection.Click();
                Thread.Sleep(2000);

                // 点击更多操作
                driver.FindElement(By.XPath("//button[@aria-label='更多操作']")).Click();
                Thread.Sleep(2000);

                // 选择导出
                driver.FindElement(By.XPath("//button[@aria-label='导出']")).Click();
                Thread.Sleep(2000);

                // 选择导出格式（例如 HTML）
                driver.FindElement(By.XPath("//button[@aria-label='HTML']")).Click();
                Thread.Sleep(2000);

                // 保存文件
                // 这里假设你已经配置了下载路径
                Thread.Sleep(5000);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"发生错误: {ex.Message}");
        }
        finally
        {
            // 关闭浏览器
            driver.Quit();
        }
    }
}
