namespace InvestmentManagement.Operator.API.Controllers;

using AutoMapper;
using InvestmentManagement.Operator.API.Dtos.Company;
using InvestmentManagement.Operator.API.Repositories;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("company")]
public class CompanyController(
    ICompanyRepository companyRepository,
    IMapper mapper) : ControllerBase
{
    private readonly ICompanyRepository companyRepository = companyRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Busca a empresa pelo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id")]
    public async Task<ActionResult<CompanyRead>> GetCompanyById([FromQuery] Guid id)
    {
        var company = await this.companyRepository.ReadCompanyById(id);

        if (company == null)
        {
            return this.NotFound();
        }

        return this.Ok(this.mapper.Map<CompanyRead>(company));
    }

    /// <summary>
    /// Cria uma empresa
    /// </summary>
    /// <param name="companyCreate"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult> CreateCompany([FromBody] CompanyCreate companyCreate)
    {
        if (string.IsNullOrEmpty(companyCreate.Email))
        {
            return this.BadRequest("Email de empresa inválido.");
        }

        var company = this.mapper.Map<Company>(companyCreate);
        await this.companyRepository.Create(company);
        await this.companyRepository.SaveChanges();

        var companyRead = this.mapper.Map<CompanyRead>(company);
        return this.Created($"get-by-id?id={company.Id}", companyRead);
    }
}
