using Business.Dtos;
using Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
namespace Business.Services;

public interface IClientService
{
    Task<Client?> GetClientByIdAsync(int clientId);
    Task<Client?> GetClientByNameAsync(string clientName);
    Task<IEnumerable<Client>?> GetClientsAsync();
}

public class ClientService(ClientRepository clientRepository, IMemoryCache cache) : IClientService
{
    private readonly ClientRepository _clientRepository = clientRepository;
    private readonly IMemoryCache _cache = cache;
    private const string _cacheKey_All = "Client_All";

    public async Task<IEnumerable<Client>?> GetClientsAsync()
    {
        if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Client>? cachedItems))
            return cachedItems;

        var clients = await SetCache();
        return clients;
    }

    public async Task<Client?> GetClientByNameAsync(string clientName)
    {
        var client = new Client();

        if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Client>? cachedItems))
        {
            client = cachedItems?.FirstOrDefault(x => x.ClientName == clientName);
            if (client != null)
                return client;
        }

        var entity = await _clientRepository.GetAsync(x => x.ClientName == clientName);
        if (entity == null)
            return null;

        await SetCache();

        client = new Client { Id = entity.Id, ClientName = entity.ClientName };
        return client;
    }

    public async Task<Client?> GetClientByIdAsync(int clientId)
    {
        var client = new Client();

        if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Client>? cachedItems))
        {
            client = cachedItems?.FirstOrDefault(x => x.Id == clientId);
            if (client != null)
                return client;
        }

        var entity = await _clientRepository.GetAsync(x => x.Id == clientId);
        if (entity == null)
            return null;

        await SetCache();

        client = new Client { Id = entity.Id, ClientName = entity.ClientName };
        return client;
    }


    public async Task<IEnumerable<Client>> SetCache()
    {
        _cache.Remove(_cacheKey_All);
        var entities = await _clientRepository.GetAllAsync();
        var clients = entities.Select(x => new Client { Id = x.Id, ClientName = x.ClientName });
        clients = clients.OrderBy(x => x.Id);
        _cache.Set(_cacheKey_All, clients, TimeSpan.FromMinutes(10));

        return clients;
    }
}
