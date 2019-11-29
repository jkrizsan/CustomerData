using CompanyData.Data.DTOs;
using CompanyData.Data.Models;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public interface IGenerateDataService
    {
        Task RemoveAllCompanyData();
        Task GenerataData(GenerateDataDto data);
        Task GenerataCompanies(GenerateDataDto data);
        Task<Contact> GenerataContact(Company comnay,GenerateDataDto data);
    }
}
