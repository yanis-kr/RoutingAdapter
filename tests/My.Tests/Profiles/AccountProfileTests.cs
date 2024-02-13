using AutoMapper;
using My.AppCore.Profiles;
using My.Domain.Models.Domain;
using My.Domain.Models.Legacy;
using My.Domain.Models.Modern;

namespace My.Tests.Profiles;

public class AccountProfileTests
{
    private readonly IMapper _mapper;

    public AccountProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<AccountProfile>());
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void AccountProfile_DomainAccount_To_LegacyAccount_Mapping_Valid()
    {
        // Arrange
        var domainAccount = new DomainAccount
        {
            DomainField = "SomeValue",
            Name = "SomeName",
            Id = 1
        };

        // Act
        var legacyAccount = _mapper.Map<LegacyAccount>(domainAccount);

        // Assert
        Assert.NotNull(legacyAccount); // Check that the mapping result is not null
        Assert.Equal(domainAccount.Id, legacyAccount.Id); // Check that the Id was correctly mapped
        Assert.Equal(domainAccount.Name, legacyAccount.Name); // Check that the Name was correctly mapped
        Assert.Equal(domainAccount.DomainField, legacyAccount.LegacyField); // Check that the DomainField was correctly mapped to LegacyField
    }

    [Fact]
    public void AccountProfile_LegacyAccount_To_DomainAccount_Mapping_Valid()
    {
        var legacyAccount = new LegacyAccount
        {
            LegacyField = "SomeValue",
            Name = "SomeName",
            Id = 1
        };

        // Act
        var domainAccount = _mapper.Map<DomainAccount>(legacyAccount);

        // Assert
        Assert.Equal(legacyAccount.LegacyField, domainAccount.DomainField);
        Assert.Equal(legacyAccount.Name, domainAccount.Name);
        Assert.Equal(legacyAccount.Id, domainAccount.Id);
    }

    // Similarly, add tests for ModernAccount and DomainAccountResponse mappings

    [Fact]
    public void AccountProfile_DomainAccount_To_ModernAccount_Mapping_Valid()
    {
        // Arrange
        var domainAccount = new DomainAccount
        {
            DomainField = "SomeValue",
            Name = "SomeName",
            Id = 1
        };

        // Act
        var modernAccount = _mapper.Map<ModernAccount>(domainAccount);

        // Assert
        Assert.NotNull(modernAccount); // Check that the mapping result is not null
        Assert.Equal(domainAccount.Id, modernAccount.Id); // Check that the Id was correctly mapped
        Assert.Equal(domainAccount.Name, modernAccount.Name); // Check that the Name was correctly mapped
        Assert.Equal(domainAccount.DomainField, modernAccount.ModernField); // Check that the DomainField was correctly mapped to ModernField
        //Assert.True(domainAccount.DomainField == modernAccount.ModernField, "DomainField was not correctly mapped to ModernField");
    }

    [Fact]
    public void AccountProfile_ModernAccount_To_DomainAccount_Mapping_Valid()
    {
        var modernAccount = new ModernAccount
        {
            ModernField = "SomeValue",
            Name = "SomeName",
            Id = 1
        };

        // Act
        var domainAccount = _mapper.Map<DomainAccount>(modernAccount);

        // Assert
        Assert.NotNull(modernAccount); // Check that the mapping result is not null
        Assert.Equal(domainAccount.Id, modernAccount.Id); // Check that the Id was correctly mapped
        Assert.Equal(domainAccount.Name, modernAccount.Name); // Check that the Name was correctly mapped
        Assert.Equal(domainAccount.DomainField, modernAccount.ModernField); // Check that the DomainField was correctly mapped to ModernField

    }
}
