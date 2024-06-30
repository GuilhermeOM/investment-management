namespace InvestmentManagement.Client.API.Controllers;

using AutoMapper;
using InvestmentManagement.Client.API.Dtos.Client;
using InvestmentManagement.Client.API.Repositories;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("client")]
public class ClientController(
    IClientRepository clientRepository,
    IMapper mapper) : ControllerBase
{
    private readonly IClientRepository clientRepository = clientRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Busca os clientes com base no id
    /// </summary>
    /// <param name="id">Id do client</param>
    /// <returns></returns>
    [HttpGet("get-by-id")]
    public async Task<ActionResult<ClientRead>> GetClientById([FromQuery] Guid id)
    {
        var client = await this.clientRepository.ReadClientById(id);

        if (client == null)
        {
            return this.NotFound();
        }

        return this.Ok(this.mapper.Map<ClientRead>(client));
    }

    /// <summary>
    /// Busca os clientes com base no email cadastrado
    /// </summary>
    /// <param name="email">email do cliente</param>
    /// <returns></returns>
    [HttpGet("get-by-email")]
    public async Task<ActionResult<ClientRead>> GetClientByEmail([FromQuery] string email)
    {
        var client = await this.clientRepository.ReadClientByEmail(email);

        if (client == null)
        {
            return this.NotFound();
        }

        return this.Ok(this.mapper.Map<ClientRead>(client));
    }

    /// <summary>
    /// Cria um cliente
    /// </summary>
    /// <param name="clientCreate"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult> CreateClient([FromBody] ClientCreate clientCreate)
    {
        if (string.IsNullOrEmpty(clientCreate.Email))
        {
            return this.BadRequest("Email de cliente inválido.");
        }

        var client = this.mapper.Map<Client>(clientCreate);
        await this.clientRepository.Create(client);
        await this.clientRepository.SaveChanges();

        var clientRead = this.mapper.Map<ClientRead>(client);
        return this.Created($"get-by-id?id={client.Id}", clientRead);
    }

    /// <summary>
    /// Deposita um valor na conta do cliente
    /// </summary>
    /// <param name="clientDeposit"></param>
    /// <returns></returns>
    [HttpPatch("deposit")]
    public async Task<ActionResult> Deposit([FromBody] ClientDeposit clientDeposit)
    {
        if (string.IsNullOrEmpty(clientDeposit.ClientEmail))
        {
            return this.BadRequest("Email de cliente inválido.");
        }

        if (clientDeposit.Amount <= 0)
        {
            return this.BadRequest("Quantia do deposito precisa ser maior que 0.");
        }

        var client = await this.clientRepository.ReadClientByEmail(clientDeposit.ClientEmail);

        if (client == null)
        {
            return this.BadRequest("Cliente não encontrado.");
        }

        await this.clientRepository.SumWallet(client.Id, clientDeposit.Amount);
        await this.clientRepository.SaveChanges();

        return this.NoContent();
    }
}
