using Data.Entities;
using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Http.Features;

namespace Business.Services;

public class ClientService(ClientRepository clientRepository, ClientInformationRepository clientInformationRepository, ClientAddressRepository clientAddressRepository)
{
    private readonly ClientRepository _clientRepository = clientRepository;
    private readonly ClientInformationRepository _clientInformationRepository = clientInformationRepository;
    private readonly ClientAddressRepository _clientAddressRepository = clientAddressRepository;

    public async Task<ServiceResult> CreateAsync(ClientRegistrationForm form)
    {
        // kontrollera så att form inte är tomt
        if (form == null)
            return ServiceResult.BadRequest();

        // kontrollera så att det inte finns en klient med samma namn
        if (await _clientRepository.ExistsAsync(x => x.ClientName == form.ClientName))
            return ServiceResult.AlreadyExists();

        try
        {
            // skapa kunden
            var clientEntity = new ClientEntity();
            var result = await _clientRepository.AddAsync(clientEntity);
            if (!result)
                return ServiceResult.Failed();

            // skapa kontaktinformation
            await _clientInformationRepository.AddAsync(clientEntity.ContactInformation);

            // skapa adress
            await _clientAddressRepository.AddAsync(clientEntity.Address);

            // skricak tillbaka ett true
            return ServiceResult.Created();


        }
        catch (Exception ex)
        {
            return ServiceResult.Failed(ex.Message);
        }
    }
}
