using Blazored.LocalStorage;
using CP.Client.Providers;
using CP.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace CP.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<AuthResponse?> Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<AuthResponse>();
                await _localStorage.SetItemAsync("authToken", response!.Token);
                
                ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(response.Token);
                
                return response;
            }
            return null;
        }

        public async Task<AuthResponse?> Register(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
             if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<AuthResponse>();
                await _localStorage.SetItemAsync("authToken", response!.Token);
                
                ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(response.Token);

                return response;
            }
            return null;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
        }
    }
}
