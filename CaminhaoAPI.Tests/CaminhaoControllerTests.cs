using CaminhaoApi.Controllers;
using CaminhaoAPI.Models.Enums;
using CaminhaoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaminhaoApi.Data;

public class CaminhaoControllerTests
{
    private readonly CaminhaoController _controller;
    private readonly DbContextOptions<CaminhaoContext> _options;

    public CaminhaoControllerTests()
    {
        // Configurando o InMemoryDatabase
        _options = new DbContextOptionsBuilder<CaminhaoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new CaminhaoContext(_options))
        {
            if (context.Caminhoes.FirstOrDefault(x => x.Id == 1) == null)
                context.Caminhoes.Add(new Caminhao { Id = 1, Modelo = ModeloEnum.FM, AnoFabricacao = 2020, CodigoChassi = "12345678901234567", Cor = "Azul", Planta = PlantaEnum.Brasil });

            if (context.Caminhoes.FirstOrDefault(x => x.Id == 2) == null)
                context.Caminhoes.Add(new Caminhao { Id = 2, Modelo = ModeloEnum.VM, AnoFabricacao = 2021, CodigoChassi = "23456789012345678", Cor = "Verde", Planta = PlantaEnum.Suecia });

            context.SaveChanges();
        }

        // Criando uma nova instância do contexto para o controlador
        var newContext = new CaminhaoContext(_options);
        _controller = new CaminhaoController(newContext);
    }

    [Fact]
    public async Task Get_ReturnsListOfCaminhoes()
    {
        // Act
        var result = await _controller.Get();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Caminhao>>>(result);
        var okResult = Assert.IsType<List<Caminhao>>(actionResult.Value);
        Assert.True(okResult.Any());
    }

    [Fact]
    public async Task Get_ReturnsCaminhao_WhenExists()
    {
        // Act
        var result = await _controller.Get(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Caminhao>>(result);
        var okResult = Assert.IsType<Caminhao>(actionResult.Value);
        Assert.Equal(1, okResult.Id);
        Assert.Equal("Azul", okResult.Cor);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenDoesNotExist()
    {
        // Act
        var result = await _controller.Get(999);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Caminhao>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task Post_CreatesCaminhao_WhenValid()
    {
        // Arrange
        var newCaminhao = new Caminhao
        {
            Modelo = ModeloEnum.FH,
            AnoFabricacao = 2022,
            CodigoChassi = "12345678901234567",
            Cor = "Azul",
            Planta = PlantaEnum.Brasil
        };

        // Act
        var result = await _controller.Post(newCaminhao);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Caminhao>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var createdCaminhao = Assert.IsType<Caminhao>(createdAtActionResult.Value);

        Assert.Equal(newCaminhao.Modelo, createdCaminhao.Modelo);
        Assert.Equal(newCaminhao.AnoFabricacao, createdCaminhao.AnoFabricacao);
        Assert.Equal(newCaminhao.CodigoChassi, createdCaminhao.CodigoChassi);
        Assert.Equal(newCaminhao.Cor, createdCaminhao.Cor);
    }

    [Fact]
    public async Task Put_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var caminhaoToUpdate = new Caminhao
        {
            Id = 3,
            Modelo = ModeloEnum.FM,
            AnoFabricacao = 2023,
            CodigoChassi = "23456789012345678",
            Cor = "Verde"
        };

        // Act
        var result = await _controller.Put(1, caminhaoToUpdate);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var caminhaoToUpdate = new Caminhao
        {
            Id = 1,
            Modelo = ModeloEnum.VM,
            AnoFabricacao = 2023,
            CodigoChassi = "12345678901234567",
            Cor = "Vermelho"
        };

        // Act
        var result = await _controller.Put(1, caminhaoToUpdate);

        // Assert
        Assert.IsType<NoContentResult>(result);

        using (var context = new CaminhaoContext(_options))
        {
            var updatedCaminhao = await context.Caminhoes.FindAsync(1);
            Assert.Equal("Vermelho", updatedCaminhao?.Cor);
            Assert.Equal(2023, updatedCaminhao?.AnoFabricacao);
        }
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenCaminhaoDoesNotExist()
    {
        // Act
        var result = await _controller.Delete(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenCaminhaoDeletedSuccessfully()
    {
        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);

        using (var context = new CaminhaoContext(_options))
        {
            var caminhao = await context.Caminhoes.FindAsync(1);
            Assert.Null(caminhao);
        }
    }
}
