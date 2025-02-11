using AutoMapper;
using eMix.ConsultaCEP.Contracts.Repositories;
using eMix.ConsultaCEP.Models;
using eMix.ConsultaCEP.Services;
using Moq;

namespace eMix.ConsultaCEP.Tests;

public class AddressServiceTests
{
    private readonly Mock<IAddressRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IViaCepHttpService> _viaCepHttpServiceMock;
    private readonly AddressService _addressService;

    public AddressServiceTests()
    {
        _repositoryMock = new Mock<IAddressRepository>();
        _mapperMock = new Mock<IMapper>();
        _viaCepHttpServiceMock = new Mock<IViaCepHttpService>();

        _addressService = new AddressService(
            _repositoryMock.Object,
            _mapperMock.Object,
            _viaCepHttpServiceMock.Object
        );
    }

    [Fact]
    public async Task Find_ShouldReturnListOfAddresses()
    {
        // Arrange
        var addresses = new List<Address> { new Address { Cep = "12345678" } };
        _repositoryMock.Setup(repo => repo.Find()).ReturnsAsync(addresses);

        // Act
        var result = await _addressService.Find();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("12345678", result.First().Cep);
    }

    [Fact]
    public async Task FindByZipCodeAndSave_ShouldReturnExistingAddress_WhenAddressExists()
    {
        // Arrange
        var zipCode = "12345678";
        var existingAddress = new Address { Cep = zipCode };
        _repositoryMock.Setup(repo => repo.Find(zipCode)).ReturnsAsync(existingAddress);

        // Act
        var result = await _addressService.FindByZipCodeAndSave(zipCode);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(zipCode, result.Cep);
        _viaCepHttpServiceMock.Verify(viaCep => viaCep.getAddressByZipCode(It.IsAny<string>()), Times.Never);
        _repositoryMock.Verify(repo => repo.Save(It.IsAny<Address>()), Times.Never);
    }

    [Fact]
    public async Task FindByZipCodeAndSave_ShouldReturnNull_WhenViaCepReturnsError()
    {
        // Arrange
        var zipCode = "12345678";
        var viaCepResponse = new ViaCepRequestResult { erro = "true" };
        _repositoryMock.Setup(repo => repo.Find(zipCode)).ReturnsAsync((Address?)null);
        _viaCepHttpServiceMock.Setup(service => service.getAddressByZipCode(zipCode)).ReturnsAsync(viaCepResponse);

        // Act
        var result = await _addressService.FindByZipCodeAndSave(zipCode);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task FindByZipCodeAndSave_ShouldSaveAndReturnNewAddress_WhenNotFoundInRepository()
    {
        // Arrange
        var zipCode = "12345678";
        var viaCepResponse = new ViaCepRequestResult { }; // Simulando resposta válida
        var newAddress = new Address { Cep = zipCode };

        _repositoryMock.Setup(repo => repo.Find(zipCode)).ReturnsAsync((Address?)null);
        _viaCepHttpServiceMock.Setup(service => service.getAddressByZipCode(zipCode)).ReturnsAsync(viaCepResponse);
        _mapperMock.Setup(mapper => mapper.Map<Address>(viaCepResponse)).Returns(newAddress);

        // Act
        var result = await _addressService.FindByZipCodeAndSave(zipCode);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(zipCode, result.Cep);
        _repositoryMock.Verify(repo => repo.Save(newAddress), Times.Once);
    }
}
