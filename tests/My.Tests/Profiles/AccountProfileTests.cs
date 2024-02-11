using AutoMapper;
using My.AppCore.Profiles;
using My.Domain.Models.Domain;
using My.Domain.Models.MySys1;
using My.Domain.Models.MySys2;

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
    public void AccountProfile_DomainAccount_To_MySys1Account_Mapping_Valid()
    {
        // Arrange
        var domainAccount = new DomainAccount
        {
            DomainField = "SomeValue",
            Name = "SomeName",
            Id = 1
        };

        // Act
        var mySys1Account = _mapper.Map<MySys1Account>(domainAccount);

        // Assert
        Assert.NotNull(mySys1Account); // Check that the mapping result is not null
        Assert.Equal(domainAccount.Id, mySys1Account.Id); // Check that the Id was correctly mapped
        Assert.Equal(domainAccount.Name, mySys1Account.Name); // Check that the Name was correctly mapped
        Assert.Equal(domainAccount.DomainField, mySys1Account.MySys1Field); // Check that the DomainField was correctly mapped to MySys1Field
    }

    [Fact]
    public void AccountProfile_MySys1Account_To_DomainAccount_Mapping_Valid()
    {
        var mySys1Account = new MySys1Account
        {
            MySys1Field = "SomeValue",
            Name = "SomeName",
            Id = 1
        };

        // Act
        var domainAccount = _mapper.Map<DomainAccount>(mySys1Account);

        // Assert
        Assert.Equal(mySys1Account.MySys1Field, domainAccount.DomainField);
        Assert.Equal(mySys1Account.Name, domainAccount.Name);
        Assert.Equal(mySys1Account.Id, domainAccount.Id);
    }

    // Similarly, add tests for MySys2Account and DomainAccountResponse mappings

    [Fact]
    public void AccountProfile_DomainAccount_To_MySys2Account_Mapping_Valid()
    {
        // Arrange
        var domainAccount = new DomainAccount
        {
            DomainField = "SomeValue",
            Name = "SomeName",
            Id = 1
        };

        // Act
        var mySys2Account = _mapper.Map<MySys2Account>(domainAccount);

        // Assert
        Assert.NotNull(mySys2Account); // Check that the mapping result is not null
        Assert.Equal(domainAccount.Id, mySys2Account.Id); // Check that the Id was correctly mapped
        Assert.Equal(domainAccount.Name, mySys2Account.Name); // Check that the Name was correctly mapped
        Assert.Equal(domainAccount.DomainField, mySys2Account.MySys2Field); // Check that the DomainField was correctly mapped to MySys2Field
        //Assert.True(domainAccount.DomainField == mySys2Account.MySys2Field, "DomainField was not correctly mapped to MySys2Field");
    }

    [Fact]
    public void AccountProfile_MySys2Account_To_DomainAccount_Mapping_Valid()
    {
        var mySys2Account = new MySys2Account
        {
            MySys2Field = "SomeValue",
            Name = "SomeName",
            Id = 1
        };

        // Act
        var domainAccount = _mapper.Map<DomainAccount>(mySys2Account);

        // Assert
        Assert.NotNull(mySys2Account); // Check that the mapping result is not null
        Assert.Equal(domainAccount.Id, mySys2Account.Id); // Check that the Id was correctly mapped
        Assert.Equal(domainAccount.Name, mySys2Account.Name); // Check that the Name was correctly mapped
        Assert.Equal(domainAccount.DomainField, mySys2Account.MySys2Field); // Check that the DomainField was correctly mapped to MySys2Field

    }
}
