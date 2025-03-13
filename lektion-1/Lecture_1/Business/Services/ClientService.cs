using Business.Factories;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;


public class ClientService(IClientRepository clientRepository)
{
    private readonly IClientRepository _clientRepository = clientRepository;


    public async Task<ResponseResult<Client>> CreateClientAsync(ClientRegistrationForm form)
    {
        var result = await ValidateModelAsync(form);
        if (!result.Success)
            return result;

        var clientEntity = ClientFactory.Map(form);
        if (clientEntity == null)
            return new ResponseResult<Client> { Success = false, StatusCode = 500, Message = "Unable to create client entity." };

        var response = await _clientRepository.AddAsync(clientEntity);
        return response
            ? new ResponseResult<Client> { Success = true, StatusCode = 201 }
            : new ResponseResult<Client> { Success = false, StatusCode = 500, Message = "Unexpected error occured." };
    }

    public async Task<ResponseResult<IEnumerable<Client>>> GetClientsAsync()
    {
        var entities = await _clientRepository.GetAllAsync();
        var clients = entities.Select(ClientFactory.Map);

        return new ResponseResult<IEnumerable<Client>> { Success = true, StatusCode = 200, Result = clients };
    }

    public async Task<ResponseResult<Client>> GetClientByIdAsync(int id)
    {
        var entity = await _clientRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return new ResponseResult<Client> { Success = false, StatusCode = 404, Message = "Client not found." };

        var client = ClientFactory.Map(entity);
        return new ResponseResult<Client> { Success = true, StatusCode = 200, Result = client };
    }

    public async Task<ResponseResult<Client>> GetClientByClientNameAsync(string clientName)
    {
        var entity = await _clientRepository.GetAsync(x => x.ClientName == clientName);
        if (entity == null)
            return new ResponseResult<Client> { Success = false, StatusCode = 404, Message = "Client not found." };

        var client = ClientFactory.Map(entity);
        return new ResponseResult<Client> { Success = true, StatusCode = 200, Result = client };
    }

    public async Task<ResponseResult<Client>> ValidateModelAsync(ClientRegistrationForm form)
    {
        if (form == null || string.IsNullOrEmpty(form.ClientName) || string.IsNullOrEmpty(form.Email))
            return new ResponseResult<Client> { Success = false, StatusCode = 400, Message = "Invalid fields." };

        if (!await _clientRepository.ExistsAsync(x => x.ClientName == form.ClientName))
            return new ResponseResult<Client> { Success = false, StatusCode = 409, Message = "Client already exists." };

        return new ResponseResult<Client> { Success = true, StatusCode = 200 };
    }
}
