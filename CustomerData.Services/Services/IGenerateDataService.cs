using CompanyData.Data.DTOs;
using CompanyData.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Services.Services
{
    public interface IGenerateDataService
    {
        void RemoveAllCompanyData();
        void GenerataCompaniesData(GenerateDataDTO data);
    }
}
