using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Todo.Services
{
    public class Gravatar
    {
        private readonly RestClient _client;
        private readonly IMemoryCache _memoryCache;

        public const string GravatarJsonProfileUrlTemplate = "https://www.gravatar.com/{0}.json";
        public const string GravatarPrefferedUsernameCacheKey = "GravatarProfile-PrefferedUsername-{0}";

        public Gravatar(IMemoryCache memoryCache)
        {
            _client = new RestClient();
            _memoryCache = memoryCache;
        }

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public string GetPreferredUsername(string emailAddress)
        {
            var userName = string.Empty;
            var emailHash = GetHash(emailAddress);

            var cacheKey = string.Format(GravatarPrefferedUsernameCacheKey, emailHash);
            if (_memoryCache.TryGetValue(cacheKey, out string preferredUsername))
            {
                return preferredUsername;
            }

            var userProfileJsonUrl = string.Format(GravatarJsonProfileUrlTemplate, emailHash);

            var request = new RestRequest(string.Format(userProfileJsonUrl, emailHash), Method.Get);

            var response = _client.Execute<GravatarProfileResponse>(request);
            if (response.IsSuccessful)
            {
                userName = response.Data?.Entry?.FirstOrDefault()?.PreferredUsername;

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };
                _memoryCache.Set(cacheKey, userName, cacheExpOptions);
            }

            return userName;
        }
    }

    public class GravatarProfileResponse
    {
        public List<GravatarProfile> Entry { get; set; }
    }

    public class GravatarProfile
    {
        public string Hash { get; set; }
        public string RequestHash { get; set; }
        public string ProfileUrl { get; set; }
        public string PreferredUsername { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<GravatarPhoto> Photos { get; set; }
        public string DisplayName { get; set; }
        public bool PreventContactMessage { get; set; }
    }

    public class GravatarPhoto
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}