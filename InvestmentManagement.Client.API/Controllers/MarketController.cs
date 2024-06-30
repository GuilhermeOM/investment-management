namespace InvestmentManagement.Client.API.Controllers;

using System.Collections.Generic;
using AutoMapper;
using InvestmentManagement.Client.API.Dtos.Client;
using InvestmentManagement.Client.API.Dtos.ClientProduct;
using InvestmentManagement.Client.API.Dtos.CompanyProduct;
using InvestmentManagement.Client.API.Repositories;
using InvestmentManagement.Client.API.Repositories.Helper;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.AspNetCore.Mvc;

public class MarketController(
    ICompanyProductRepository companyProductRepository,
    IClientProductRepository clientProductRepository,
    IClientRepository clientRepository,
    IMapper mapper) : ControllerBase
{
    private readonly ICompanyProductRepository companyProductRepository = companyProductRepository;
    private readonly IClientProductRepository clientProductRepository = clientProductRepository;
    private readonly IClientRepository clientRepository = clientRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Busca os produtos (no momento apenas dividendos) no mercado primário
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("primary-market")]
    public async Task<ActionResult<PagedList<CompanyProductRead>>> GetPrimaryMarket(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var companyProducts = await this.companyProductRepository.ReadCompanyProductWithPagging(page, pageSize);

        if (companyProducts == null)
        {
            return this.NotFound();
        }

        return this.Ok(new
        {
            Items = this.mapper.Map<IEnumerable<CompanyProductRead>>(companyProducts.Items),
            companyProducts.Page,
            companyProducts.PageSize,
            companyProducts.TotalCount,
            companyProducts.HasNextPage,
            companyProducts.HasPreviousPage,
        });
    }

    /// <summary>
    /// Busca os produtos (no momento apenas dividendos) com base no email da empresa no mercado primário
    /// </summary>
    /// <param name="email">email da empresa</param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("primary-market-by-email")]
    public async Task<ActionResult<PagedList<CompanyProductRead>>> GetPrimaryMarketByEmail(
        [FromQuery] string email,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var companyProducts = await this.companyProductRepository.ReadCompanyProductByEmailWithPagging(email, page, pageSize);

        if (companyProducts == null)
        {
            return this.NotFound();
        }

        return this.Ok(new
        {
            Items = this.mapper.Map<IEnumerable<CompanyProductRead>>(companyProducts.Items),
            companyProducts.Page,
            companyProducts.PageSize,
            companyProducts.TotalCount,
            companyProducts.HasNextPage,
            companyProducts.HasPreviousPage,
        });
    }

    /// <summary>
    /// Busca os produtos (no momento apenas dividendos) com base no Ticker da ação no mercado primário
    /// </summary>
    /// <param name="ticker"></param>
    /// <returns></returns>
    [HttpGet("primary-market-by-ticker")]
    public async Task<ActionResult<CompanyProductRead>> GetPrimaryMarketByTicker(
        [FromQuery] string ticker)
    {
        var companyProducts = await this.companyProductRepository.ReadCompanyProductByTicker(ticker);

        if (companyProducts == null)
        {
            return this.NotFound();
        }

        return this.Ok(this.mapper.Map<CompanyProductRead>(companyProducts));
    }

    /// <summary>
    /// Realiza compra de uma ação no mercado primário
    /// </summary>
    /// <param name="companyProductBuy"></param>
    /// <returns></returns>
    [HttpPatch("primary-market/buy")]
    public async Task<ActionResult> PatchPrimaryMarketBuy(
        [FromBody] CompanyProductBuy companyProductBuy)
    {
        if (companyProductBuy.Amount < 1)
        {
            return this.BadRequest("Quantia da compra precisa ser maior que 1.");
        }

        var client = await this.clientRepository.ReadClientByEmail(companyProductBuy.ClientEmail);

        if (client == null)
        {
            return this.BadRequest("Cliente não encontrado.");
        }

        var companyProduct = await this.companyProductRepository.ReadCompanyProductByTicker(companyProductBuy.Ticker);

        if (companyProduct == null)
        {
            return this.BadRequest("Produto não encontrado no mercado primário.");
        }

        if (companyProduct.Amount < companyProductBuy.Amount)
        {
            return this.BadRequest($"Não é possível comprar {companyProductBuy.Amount} itens desse produto, pois só há {companyProduct.Amount} disponíveis no mercado.");
        }

        if (client.Wallet < companyProduct.Product.InitialPrice * companyProductBuy.Amount)
        {
            return this.BadRequest("Saldo insuficiente.");
        }

        await this.companyProductRepository.BuyCompanyProduct(companyProduct.ProductId, companyProductBuy.Amount);

        if (companyProduct.Amount == 0)
        {
            this.companyProductRepository.Delete(companyProduct);
        }

        var clientProduct = await this.clientProductRepository.ReadByTicker(companyProductBuy.Ticker);

        if (clientProduct == null)
        {
            var clientProductCreate = new ClientProductCreate
            {
                Product = companyProduct.Product,
                Amount = companyProductBuy.Amount
            };
            var newClientProduct = this.mapper.Map<ClientProduct>(clientProductCreate);

            await this.clientProductRepository.Create(newClientProduct);

            await this.clientProductRepository.SaveChanges();

            clientProduct = await this.clientProductRepository.ReadByTicker(companyProductBuy.Ticker);
        }

        var clientHasProduct = client.Products
            .Exists(product => product.Product.Ticker.Equals(companyProduct.Product.Ticker, StringComparison.OrdinalIgnoreCase));

        if (clientHasProduct
            && client.Products.Any(p =>
                    p.Product.Ticker.Equals(companyProduct.Product.Ticker, StringComparison.OrdinalIgnoreCase) && !p.Purchasable))
        {
            await this.clientRepository.AddClientProductAmount(clientProduct!.Id, companyProductBuy.Amount);
        }
        else
        {
            await this.clientRepository.AddClientProduct(client.Email, clientProduct!);
        }

        await this.clientRepository.SubtractWallet(client.Id, companyProduct.Product.InitialPrice * companyProductBuy.Amount);

        await this.companyProductRepository.SaveChanges();
        await this.clientRepository.SaveChanges();

        return this.NoContent();
    }

    /// <summary>
    /// Busca os produtos (no momento apenas dividendos) no mercado secundário
    /// </summary>
    /// <returns></returns>
    [HttpGet("secondary-market")]
    public async Task<ActionResult<IEnumerable<ClientMarketRead>>> GetSecondaryMarket()
    {
        var clients = await this.clientRepository.ReadPurshasableClient();

        if (!clients.Any())
        {
            return this.NotFound();
        }

        return this.Ok(this.mapper.Map<IEnumerable<ClientMarketRead>>(clients));
    }

    /// <summary>
    /// Busca os produtos (no momento apenas dividendos) com base no email do cliente que está vendendo no mercado secundário
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("secondary-market-by-email")]
    public async Task<ActionResult<ClientMarketRead>> GetSecondaryMarketByEmail(
        [FromQuery] string email)
    {
        var client = await this.clientRepository.ReadPurshasableClientByEmail(email);
        if (client == null)
        {
            return this.NotFound();
        }

        return this.Ok(this.mapper.Map<ClientMarketRead>(client));
    }

    /// <summary>
    /// Busca os produtos (no momento apenas dividendos) com base no Ticker do dividendo no mercado secundário
    /// </summary>
    /// <param name="ticker"></param>
    /// <returns></returns>
    [HttpGet("secondary-market-by-ticker")]
    public async Task<ActionResult<IEnumerable<ClientMarketRead>>> GetSecondaryMarketByTicker(
        [FromQuery] string ticker)
    {
        var clients = await this.clientRepository.ReadPurshasableClientsByTicker(ticker);

        if (!clients.Any())
        {
            return this.NotFound();
        }

        return this.Ok(this.mapper.Map<IEnumerable<ClientMarketRead>>(clients));
    }
}
