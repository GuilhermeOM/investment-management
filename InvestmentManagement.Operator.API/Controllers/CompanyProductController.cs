namespace InvestmentManagement.Operator.API.Controllers;

using System.Collections.Generic;
using AutoMapper;
using InvestmentManagement.Operator.API.Dtos.CompanyProduct;
using InvestmentManagement.Operator.API.Dtos.ProductType;
using InvestmentManagement.Operator.API.Repositories;
using InvestmentManagement.Operator.API.Repositories.Helper;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("company-product")]
public class CompanyProductController(
    ICompanyRepository companyRepository,
    IProductTypeRepository productTypeRepository,
    ICompanyProductRepository companyProductRepository,
    IMapper mapper) : ControllerBase
{
    private readonly ICompanyRepository companyRepository = companyRepository;
    private readonly IProductTypeRepository productTypeRepository = productTypeRepository;
    private readonly ICompanyProductRepository companyProductRepository = companyProductRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Busca os produtos da empresa com base no id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id")]
    public async Task<ActionResult<CompanyProductRead>> GetCompanyProductById([FromQuery] Guid id)
    {
        var companyProduct = await this.companyProductRepository.ReadCompanyProductById(id);

        if (companyProduct == null)
        {
            return this.NotFound();
        }

        return this.Ok(this.mapper.Map<CompanyProductRead>(companyProduct));
    }

    /// <summary>
    /// Busca os produtos da empresa com base no email da empresa que criou
    /// </summary>
    /// <param name="email"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("get-by-email")]
    public async Task<ActionResult<PagedList<CompanyProductRead>>> GetCompanyProductsByEmail(
        [FromQuery] string email,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var companyProducts = await this.companyProductRepository.ReadCompanyProductByEmailWithPagging(email, page, pageSize);

        if (companyProducts.Items.Count == 0)
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
    /// Cria um produto
    /// </summary>
    /// <param name="companyProductCreate"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult> CreateCompanyProduct(
        [FromBody] CompanyProductCreate companyProductCreate)
    {
        if (string.IsNullOrEmpty(companyProductCreate.Product.CreatorEmail))
        {
            return this.BadRequest("Email de empresa inválido.");
        }

        var company = await this.companyRepository
            .ReadCompanyByEmail(companyProductCreate.Product.CreatorEmail);

        if (company == null)
        {
            return this.BadRequest("Empresa não existe.");
        }

        var productType = await this.productTypeRepository
            .ReadProductTypeByType(companyProductCreate.Product.TypeName);

        if (productType == null) // criação do tipo dividendo caso não exista
        {
            var productTypeCreate = new ProductTypeCreate()
            {
                Type = companyProductCreate.Product.TypeName
            };

            productType = this.mapper.Map<ProductType>(productTypeCreate);
            await this.productTypeRepository.Create(productType);
        }

        var companyProducts = await this.companyProductRepository.ReadCompanyProductByEmail(companyProductCreate.Product.CreatorEmail);

        if (companyProducts.Any(p => p.Product.Ticker == companyProductCreate.Product.Ticker))
        {
            return this.BadRequest("Empresa já possui esse produto no portifólio.");
        }

        var companyProduct = this.mapper.Map<CompanyProduct>(companyProductCreate);
        companyProduct.Product.Company = company;
        companyProduct.Product.Type = productType;

        await this.companyProductRepository.Create(companyProduct);

        await this.productTypeRepository.SaveChanges();
        await this.companyProductRepository.SaveChanges();

        var companyProductRead = this.mapper.Map<CompanyProductRead>(companyProduct);
        return this.Created($"get-by-id?id={companyProduct.Id}", companyProductRead);
    }

    /// <summary>
    /// Adiciona uma quantia de produtos ao solicitado
    /// </summary>
    /// <param name="companyProductAddAmount"></param>
    /// <returns></returns>
    [HttpPatch("add-product-amount")]
    public async Task<ActionResult> UpdateCompanyProductAmount(
        [FromBody] CompanyProductAddAmount companyProductAddAmount)
    {
        if (string.IsNullOrEmpty(companyProductAddAmount.CreatorEmail))
        {
            return this.BadRequest("Email de empresa inválido.");
        }

        var company = await this.companyRepository
            .ReadCompanyByEmail(companyProductAddAmount.CreatorEmail);

        if (company == null)
        {
            return this.BadRequest("Empresa não existe.");
        }

        var updatedRows = await this.companyProductRepository
            .SumCompanyProductAmount(companyProductAddAmount.Ticker, companyProductAddAmount.Amount);

        if (updatedRows > 0)
        {
            return this.NoContent();
        }

        return this.Problem("Não foi possível realizar a atualização.");
    }
}
