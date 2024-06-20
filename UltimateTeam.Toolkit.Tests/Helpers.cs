using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System.Reflection;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Tests.Helpers
{
    public class Auth : ITwoFactorCodeProvider
    {
        public TaskCompletionSource<string> taskResult = new TaskCompletionSource<string>();
        public Task<string> GetTwoFactorCodeAsync(AuthenticationType authenticationType)
        {
            authenticationType = AuthenticationType.Email;
            Console.WriteLine($"{DateTime.Now} Enter OTP:");
            taskResult.SetResult(Console.ReadLine());
            return taskResult.Task;
        }
    }

    public static class CookieHandler
    {
        public static void SaveCookiesToJson(CookieContainer cookieContainer, string filePath)
        {
            var cookies = GetAllCookies(cookieContainer);
            string json = JsonConvert.SerializeObject(cookies, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static CookieContainer LoadCookiesFromJson(string filePath)
        {
            CookieContainer cookieContainer = new CookieContainer();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var cookies = JsonConvert.DeserializeObject<CookieCollection>(json);
                foreach (Cookie cookie in cookies)
                {
                    cookieContainer.Add(cookie);
                }
            }

            return cookieContainer;
        }

        public static CookieCollection GetAllCookies(CookieContainer cookieContainer)
        {
            CookieCollection allCookies = new CookieCollection();
            Hashtable table = (Hashtable)cookieContainer.GetType().GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(cookieContainer);
            foreach (var key in table.Keys)
            {
                string domain = key as string;
                if (domain == null) continue;

                var domainTableValue = table[domain];
                var domainList = domainTableValue.GetType().GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(domainTableValue) as SortedList;
                if (domainList == null) continue;

                foreach (var entry in domainList)
                {
                    var dictionaryEntry = (DictionaryEntry)entry;
                    var cookieCollection = dictionaryEntry.Value as CookieCollection;
                    if (cookieCollection == null) continue;

                    foreach (Cookie cookie in cookieCollection)
                    {
                        allCookies.Add(cookie);
                    }
                }
            }
            return allCookies;
        }
    }
}