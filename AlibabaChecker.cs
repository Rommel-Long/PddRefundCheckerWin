using DocumentFormat.OpenXml.Bibliography;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Threading;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

public class AlibabaChecker
{
    private IWebDriver _driver;

    public AlibabaChecker()
    {
  
        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.EdgeConfig());

        var options = new EdgeOptions();
       
        options.AddArgument("--start-maximized");
        options.AddArgument("--remote-debugging-port=9222");
        //options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/114 Safari/537.36");

        options.AddArgument(@"user-data-dir=C:\Users\72179\AppData\Local\Microsoft\Edge\User Data");
        options.AddArgument("profile-directory=Default");


        _driver = new EdgeDriver(options);
        
    }

    public void EnsureLogin()
    {
       
        _driver.Navigate().GoToUrl("https://www.1688.com");

        LoginHelper.LoadCookies(_driver, "https://www.1688.com");

        _driver.Navigate().Refresh();

        Thread.Sleep(2000);

        if (!IsLoggedIn())
        {
            MessageBox.Show("⚠ 当前未登录，请手动登录 1688。完成后点击“确定”继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Console.ReadKey();
            if (IsLoggedIn())
            {
                if (MessageBox.Show("是否保存本次登录的 Cookie？", "保存 Cookie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    LoginHelper.SaveCookies(_driver);
                }
            }
            else
            {
                throw new Exception("登录失败，请重试。");
            }
        }
    }

    private bool IsLoggedIn()
    {
        try
        {
            // 根据是否出现“我的阿里”等元素判断是否已登录
            return _driver.PageSource.Contains("我的阿里") || !_driver.PageSource.Contains("登录");
        }
        catch
        {
            return false;
        }
    }

    public string CheckStatus(string trackingNumber)
    {
        try
        {
            _driver.Navigate().GoToUrl("https://trade.1688.com/order/order_list.htm");
            Thread.Sleep(3000);

            var searchBox = _driver.FindElement(By.Name("orderId"));
            searchBox.Clear();
            searchBox.SendKeys(trackingNumber);
            searchBox.SendKeys(Keys.Enter);

            Thread.Sleep(4000); // ⏳ 等搜索结果加载

            // 检查滑块或验证码（如果需要也可识别）
            if (_driver.PageSource.Contains("nc-container"))
            {
                MessageBox.Show("检测到滑块，请手动完成后点击“确定”继续", "验证码验证", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var html = _driver.PageSource;
            Thread.Sleep(new Random().Next(2000, 4000)); // ⏱️ 防风控延迟
            return html.Contains("申请退货") ? "✅ 已申请" : "❌ 未申请";
        }
        catch (Exception ex)
        {
            return $"❗ 查询失败：{ex.Message}";
        }
    }

    public void Quit() => _driver?.Quit();
}