using CompanyData.Data.DTOs;
using CompanyData.Data.Models;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public interface IGenerateDataService
    {
        void RemoveAllCompanyData();
        void GenerataData(GenerateDataDTO data);
        Task GenerataCompany(GenerateDataDTO data);
        Contact GenerataContact(Company comnay,GenerateDataDTO data);
        Order GenerataOrder(Contact contact, GenerateDataDTO data);
    }
}
