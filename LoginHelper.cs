using OpenQA.Selenium;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

public static class LoginHelper
{
    private const string CookiePath = "cookies.json";
    private const string Cx = "crshi";

    public static void SaveCookies(IWebDriver driver)
    {
        var cookies = driver.Manage().Cookies.AllCookies;
        var serializableCookies = new List<SerializableCookie>();

        foreach (var cookie in cookies)
        {
            serializableCookies.Add(new SerializableCookie
            {
                Name = cookie.Name,
                Value = cookie.Value,
                Domain = cookie.Domain,
                Path = cookie.Path,
                Expiry = cookie.Expiry,
                Secure = cookie.Secure
            });
        }

        File.WriteAllText(CookiePath, JsonConvert.SerializeObject(serializableCookies, Formatting.Indented));
        MessageBox.Show("✅ Cookie 已保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void LoadCookies(IWebDriver driver, string baseUrl)
    {
        if (!File.Exists(CookiePath))
        {
            MessageBox.Show("未找到 cookies.json，请先手动登录保存一次。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var cookiesJson = File.ReadAllText(CookiePath);
        var cookies = JsonConvert.DeserializeObject<List<SerializableCookie>>(cookiesJson);

        driver.Navigate().GoToUrl(baseUrl);

        foreach (var c in cookies)
        {
            try
            {
                var cookie = new Cookie(c.Name, c.Value, c.Domain, c.Path, c.Expiry);
                driver.Manage().Cookies.AddCookie(cookie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ 添加 Cookie 失败：{ex.Message}");
            }
        }

        driver.Navigate().Refresh();
        Console.WriteLine("✅ Cookie 已加载");
    }

    private class SerializableCookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTime? Expiry { get; set; }
        public bool Secure { get; set; }
    }
}
