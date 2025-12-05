// Services/Collaborate/Infrastructure/Services/ProfilesClient.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AidManager.Collaborate.Infrastructure.Services;

public class ProfilesClient : IProfilesClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProfilesClient> _logger;

    public ProfilesClient(HttpClient httpClient, ILogger<ProfilesClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ProfilesUserDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/api/Profiles/{userId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "No se pudo obtener el usuario {UserId} desde Profiles. StatusCode: {StatusCode}",
                userId, response.StatusCode);
            return null;
        }

        var user = await response.Content.ReadFromJsonAsync<ProfilesUserDto>(cancellationToken: cancellationToken);
        return user;
    }
}