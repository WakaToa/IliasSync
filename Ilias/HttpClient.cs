using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IliasSync.Ilias
{
    public class HttpClientEx
    {
        public const string UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36"
            ;
        private readonly HttpClient _httpClient;

        public HttpClientHandler ClientHandler { get; set; }
        public HttpClientEx()
        {
            ClientHandler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };
            ClientHandler.AllowAutoRedirect = true;
            ClientHandler.ClientCertificateOptions = ClientCertificateOption.Automatic;
            ClientHandler.CookieContainer = new CookieContainer();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.MaxServicePoints = int.MaxValue;
            ServicePointManager.SetTcpKeepAlive(true, 6000000, 100000);
            _httpClient = new HttpClient(ClientHandler);
            SetExpectContinueHeaderToFalse();
        }

        private void SetExpectContinueHeaderToFalse()
        {
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        public void ClearRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public void AddConnectionKeepAliveHeader()
        {
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
        }

        public void AddRequestHeader(string name, string value)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
            }
            catch
            {

            }

        }

        public void RemoveRequestHeader(string name)
        {
            _httpClient.DefaultRequestHeaders.Remove(name);
        }

        public void SetReferrerUri(string value)
        {
            _httpClient.DefaultRequestHeaders.Referrer = new Uri(value);
        }

        public async Task<string> GetAsync(string requestUri)
        {
            var d = await _httpClient.GetAsync(requestUri).ConfigureAwait(false);

            return await d.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string requestUri, HttpContent httpContent)
        {
            var d = await _httpClient.PostAsync(requestUri, httpContent).ConfigureAwait(false);

            return await d.Content.ReadAsStringAsync();
        }

        public async Task<byte[]> GetByteArrayAsync(string requestUri)
        {
            return await _httpClient.GetByteArrayAsync(requestUri).ConfigureAwait(false);
        }


        private bool _addedCommonHeaders = false;

        public void AddCommonHeaders()
        {
            if (!_addedCommonHeaders)
            {
                AddRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                AddRequestHeader("Accept-Language", "de-DE,de;q=0.9,en-US;q=0.8,en;q=0.7");
                AddRequestHeader("User-Agent", UserAgent);
                AddConnectionKeepAliveHeader();
                _addedCommonHeaders = true;
            }

        }
    }
}
