using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace CP.Client.Providers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.').Length > 1 ? jwt.Split('.')[1] : string.Empty;
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = new List<Claim>();
            if (keyValuePairs == null)
                return claims;

            foreach (var kvp in keyValuePairs)
            {
                var value = kvp.Value?.ToString() ?? string.Empty;

                // Map common JWT claim names to ClaimTypes so Identity.Name and roles work correctly
                if (kvp.Key == "unique_name" || kvp.Key == "name" || kvp.Key == ClaimTypes.Name)
                {
                    claims.Add(new Claim(ClaimTypes.Name, value));
                    continue;
                }

                if (kvp.Key == "role" || kvp.Key == ClaimTypes.Role)
                {
                    // Roles can be serialized as array or single value
                    if (value.StartsWith("["))
                    {
                        try
                        {
                            var roles = JsonSerializer.Deserialize<string[]>(value);
                            if (roles != null)
                            {
                                foreach (var r in roles)
                                    claims.Add(new Claim(ClaimTypes.Role, r));
                            }
                            else
                            {
                                claims.Add(new Claim(ClaimTypes.Role, value));
                            }
                        }
                        catch
                        {
                            // Fallback if parsing fails
                            var cleaned = value.Trim('[', ']', '"');
                            foreach (var r in cleaned.Split(','))
                                claims.Add(new Claim(ClaimTypes.Role, r.Trim()));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, value));
                    }

                    continue;
                }

                // Default: keep original claim name
                claims.Add(new Claim(kvp.Key, value));
            }

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
