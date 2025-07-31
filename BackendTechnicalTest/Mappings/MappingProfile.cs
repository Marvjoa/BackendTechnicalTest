using AutoMapper;
using BackendTechnicalTest.API.Dto.Clients;
using BackendTechnicalTest.VIewModels.Accounts;
using BackendTechnicalTest.API.Dto.Accounts;
using BackendTechnicalTest.VIewModels.Transactions;
using BackendTechnicalTest.API.Dto.Transactions;
using BackendTechnicalTest.VIewModels.Clients;

namespace BackendTechnicalTest.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateClientViewModel, ClientDto>();
        CreateMap<CreateAccountViewModel, AccountDto>();
        CreateMap<DepositViewModel, TransactionDto>();
        CreateMap<WithdrawViewModel, TransactionDto>();
    }
}