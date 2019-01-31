namespace CrossSessionCookies
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    public class CookiesControl
    {
        private IWebDriver Instance;

        public CookiesControl(IWebDriver instance, IfrBrowser parent)
        {
            Instance = instance;
        }

        public void Add(string name, string domain, string value, string path, DateTime expDate)
        {
            OpenQA.Selenium.Cookie myCookie = new Cookie(name, value, domain, path, expDate);

            try
            {
                Instance.Manage().Cookies.DeleteCookieNamed(name);
            }
            catch
            {
                Log.WarnMsg("Failed to delete Cookie from browser. Looks like its absent cookie with name '{0}'", name);
            }

            try
            {
                Instance.Manage().Cookies.AddCookie(myCookie);
            }
            catch
            {
                Log.ErrorMsg("Failed to add cookie. Most possible is Domain from Cookie and current domain in browser is not the same.");
            }

        }

        public List<Cookie> GetAll()
        {
            List<Cookie> cookies = new List<Cookie>();

            cookies.AddRange(Instance.Manage().Cookies.AllCookies);

            return cookies;
        }

        public void DeleteAll()
        {
            Instance.Manage().Cookies.DeleteAllCookies();
        }

        public void Delete(string name)
        {
            Instance.Manage().Cookies.DeleteCookieNamed(name);
        }

        public void Delete(Cookie cookie)
        {
            Instance.Manage().Cookies.DeleteCookie(cookie);
        }

        public Cookie Get(string name)
        {
            Cookie result;

            result = Instance.Manage().Cookies.GetCookieNamed(name);

            return result;
        }
    }
}