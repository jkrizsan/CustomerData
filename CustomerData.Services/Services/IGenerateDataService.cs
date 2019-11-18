using CompanyData.Data.DTOs;
using CompanyData.Data.Models;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public interface IGenerateDataService
    {
        Task RemoveAllCompanyData();
        Task GenerataData(GenerateDataDto data);
        void GenerataCompanies(GenerateDataDto data);
        Contact GenerataContact(Company comnay,GenerateDataDto data);
        Order GenerataOrder(Contact contact, GenerateDataDto data);
    }
}
