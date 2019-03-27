using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IliasSync.Ilias
{
    public class IliasClient
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private HttpClientEx _httpClient;

        public IliasClient(string username, string password)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            Username = username;
            Password = password;

            _httpClient = new HttpClientEx();
            _httpClient.AddCommonHeaders();
        }

        public async Task<bool> LoginAsync()
        {
            var loggedIn = await _httpClient.GetAsync("https://www.ilias.fh-dortmund.de/");

            if (loggedIn.Contains("Abmelden"))
            {
                return true;
            }

            var postData = $"username={Username}&password={Password}&cmd%5BdoStandardAuthentication%5D=Anmelden";


            _httpClient.AddRequestHeader("Cache-Control", "max-age=0");
            _httpClient.SetReferrerUri("https://www.ilias.fh-dortmund.de/ilias/login.php?target=&soap_pw=&ext_uid=&cookies=nocookies&client_id=ilias-fhdo&lang=de");

            loggedIn = await _httpClient.PostAsync("https://www.ilias.fh-dortmund.de/ilias/ilias.php?lang=de&cmd=post&cmdClass=ilstartupgui&cmdNode=w6&baseClass=ilStartUpGUI&rtoken=", new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded"));

            return loggedIn.Contains("Abmelden");
        }

        public async Task<PersonalDesktop> GetPersonalDesktopAsync()
        {
            var webData =
                await _httpClient.GetAsync(
                    "https://www.ilias.fh-dortmund.de/ilias/ilias.php?baseClass=ilPersonalDesktopGUI&cmd=jumpToSelectedItems");


            var doc = new HtmlDocument();
            doc.LoadHtml(webData);


            var ret = new List<Course>();
            foreach (var row in doc.DocumentNode.SelectNodes("//div").Where(x => x.HasClass("ilObjListRow")))
            {
                var result = Regex.Match(row.InnerHtml, "<h4 class=\"il_ContainerItemTitle\"><a href=\"https://www.ilias.fh-dortmund.de/ilias/goto_ilias-fhdo_crs_(.*?).html\" class=\"il_ContainerItemTitle\" target=\"_top\">(.*?)</a></h4>");
                if (result.Groups[1].Value == "")
                    continue;

                var courseId = int.Parse(result.Groups[1].Value);

                ret.Add(new Course(courseId, result.Groups[2].Value));


            }

            return new PersonalDesktop(ret);
        }

        public async Task<List<Container>> GetContainersFromContainerAsync(int containerId)
        {
            var ret = new List<Container>();
            var containerStr = await _httpClient.GetAsync($"https://www.ilias.fh-dortmund.de/ilias/ilias.php?ref_id={containerId}&baseClass=ilRepositoryGUI");

            var hie = Regex.Matches(containerStr,
                "alt=\"Ordner\" border=\"0\" /> --><li><a href=\"(.*?)\"  target=\"_top\" >(.*?)</a></li>");

            var folders =
                Regex.Matches(containerStr, "<a href=\"https://www.ilias.fh-dortmund.de/ilias/goto_ilias-fhdo_fold_(.*?).html\" class=\"il_ContainerItemTitle\"  target=\"_top\" >(.*?)</a>");

            foreach (Match folder in folders)
            {
                ret.Add(new Container(int.Parse(folder.Groups[1].Value), folder.Groups[2].Value));
            }

            return ret;
        }


        public async Task<List<File>> GetFilesFromContainerAsync(int containerId)
        {
            var ret = new List<File>();
            var containerStr = await _httpClient.GetAsync($"https://www.ilias.fh-dortmund.de/ilias/ilias.php?ref_id={containerId}&baseClass=ilRepositoryGUI");

            var hie = Regex.Matches(containerStr,
                "alt=\"Ordner\" border=\"0\" /> --><li><a href=\"(.*?)\"  target=\"_top\" >(.*?)</a></li>");
            var hiePath = hie.Cast<Match>().Aggregate("", (current, match) => Path.Combine(current, match.Groups[2].Value.Trim().RemoveIlegalPathCharacters()));

            var doc = new HtmlDocument();
            doc.LoadHtml(containerStr);
            foreach (var row in doc.DocumentNode.SelectNodes("//div").Where(x => x.HasClass("ilCLI") && x.HasClass("ilObjListRow") && x.HasClass("row")))
            {
                if (row.InnerHtml.Contains("goto_ilias-fhdo_file_"))
                {
                    var match = Regex.Match(row.InnerHtml, "<a href=\"https://www.ilias.fh-dortmund.de/ilias/goto_ilias-fhdo_file_(.*?)_download.html\" class=\"il_ContainerItemTitle\">(.*?)</a>");

                    var fileId = int.Parse(match.Groups[1].Value);
                    var fileName = match.Groups[2].Value;
                    var fileExt = "";
                    var res = Regex.Matches(row.InnerHtml, "<span class=\"il_ItemProperty\">(.*?)</span>", RegexOptions.Singleline);

                    foreach (Match re in res)
                    {
                        if (WebUtility.HtmlDecode(re.Groups[1].ToString()).Trim() != string.Empty)
                        {
                            fileExt = WebUtility.HtmlDecode(re.Groups[1].ToString()).Trim();
                            break;
                        }
                    }

                    var file = new File(fileId, fileName.RemoveIlegalPathCharacters(), hiePath, fileExt.RemoveIlegalPathCharacters());
                    ret.Add(file);
                }

            }

            return ret;
        }

        public async Task<byte[]> DownloadFileAsync(int fileId)
        {
            var data = await _httpClient.GetByteArrayAsync($"https://www.ilias.fh-dortmund.de/ilias/goto_ilias-fhdo_file_{fileId}_download.html");

            return data;
        }
    }
}
