using Eml.DataRepository.Contracts;
// using EcomEngine.Business.Common.Entities;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace EcomEngine.Tests.Unit.Stubs
{
    public class RepositoryStubs : IDisposable
    {
        private readonly List<IDisposable> disposables;
        // public readonly IDataRepositorySoftDeleteGuid<Company> CompanyRepository;
        // public readonly IDataRepositorySoftDeleteGuid<ContactPerson> ContactPersonRepository;
        // public readonly IDataRepositorySoftDeleteGuid<Contract> ContractRepository;
        // public readonly IDataRepositorySoftDeleteGuid<PositionTitle> PositionTitleRepository;

        public RepositoryStubs()
        {
            // CompanyRepository = Substitute.For<IDataRepositorySoftDeleteGuid<Company>>();
            // ContactPersonRepository = Substitute.For<IDataRepositorySoftDeleteGuid<ContactPerson>>();
            // ContractRepository = Substitute.For<IDataRepositorySoftDeleteGuid<Contract>>();
            // PositionTitleRepository = Substitute.For<IDataRepositorySoftDeleteGuid<PositionTitle>>();

            disposables = new List<IDisposable>
            {
                // CompanyRepository,
                // ContactPersonRepository,
                // ContractRepository,
                // PositionTitleRepository
            };
        }

        public void Dispose()
        {
            disposables.ForEach(r => r?.Dispose());
        }
    }
}
