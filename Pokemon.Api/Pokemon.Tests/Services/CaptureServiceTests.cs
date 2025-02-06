using Moq;
using Pokemon.Api.Services;
using Pokemon.Api.Data;
using Pokemon.Api.Models;
using Pokemon.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.Tests
{
    public class CaptureServiceTests
    {
        private readonly CaptureService _captureService;
        private readonly Mock<PokemonService> _mockPokemonService;
        private readonly AppDbContext _dbContext;

        public CaptureServiceTests()
        {
            // Configuração do banco de dados em memória para testes
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Banco de dados em memória
                .Options;

            _dbContext = new AppDbContext(options);

            // Criando a instância do PokemonService
            _mockPokemonService = new Mock<PokemonService>(_dbContext);

            // Instanciando o serviço CaptureService com o mock do PokemonService
            _captureService = new CaptureService(_dbContext, _mockPokemonService.Object);
        }

        [Fact]
        public async Task CapturePokemonAsync_ShouldReturnCapture_WhenValidTrainerAndPokemon()
        {
            // Arrange
            var trainer = new Trainer { Id = 1, Name = "Ash" };
            var pokemonDto = new DetailsDto { Id = 1, Name = "Pikachu" };
            var captureDto = new CaptureDto { TrainerId = 1, PokemonId = 1 };

            _dbContext.Trainers.Add(trainer);
            _dbContext.SaveChanges();

            // Mockando o comportamento do PokemonService para retornar o Pokémon correto
            _mockPokemonService.Setup(p => p.GetPokemonByIdAsync(captureDto.PokemonId))
                               .ReturnsAsync(pokemonDto);

            // Act
            var capture = await _captureService.CapturePokemonAsync(captureDto);

            // Assert
            Assert.NotNull(capture);
            Assert.Equal(trainer.Id, capture?.TrainerId);
            Assert.Equal(pokemonDto.Id, capture?.PokemonId);
            Assert.Equal(pokemonDto.Name, capture?.PokemonName);
            Assert.True(capture?.CreatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public async Task CapturePokemonAsync_ShouldReturnNull_WhenTrainerNotFound()
        {
            // Arrange
            var captureDto = new CaptureDto { TrainerId = 999, PokemonId = 1 };

            // Act
            var capture = await _captureService.CapturePokemonAsync(captureDto);

            // Assert
            Assert.Null(capture);
        }

        [Fact]
        public async Task CapturePokemonAsync_ShouldReturnNull_WhenPokemonNotFound()
        {
            // Arrange
            var trainer = new Trainer { Id = 1, Name = "Ash" };
            var captureDto = new CaptureDto { TrainerId = 1, PokemonId = 999 };

            _dbContext.Trainers.Add(trainer);
            _dbContext.SaveChanges();

            // Act
            var capture = await _captureService.CapturePokemonAsync(captureDto);

            // Assert
            Assert.Null(capture);
        }

        [Fact]
        public async Task GetCapturedPokemonsAsync_ShouldReturnList_WhenCapturesExist()
        {
            // Arrange
            var trainer = new Trainer { Id = 1, Name = "Ash" };
            var pokemon = new DetailsDto { Id = 1, Name = "Pikachu" };

            var captureDto = new CaptureDto { TrainerId = 1, PokemonId = 1 };
            _dbContext.Trainers.Add(trainer);
            _dbContext.SaveChanges();

            // Mockando o comportamento do PokemonService para retornar o Pokémon correto
            _mockPokemonService.Setup(p => p.GetPokemonByIdAsync(captureDto.PokemonId))
                               .ReturnsAsync(pokemon);

            // Act
            await _captureService.CapturePokemonAsync(captureDto);
            var capturedPokemons = await _captureService.GetCapturedPokemonsAsync();

            // Assert
            Assert.NotEmpty(capturedPokemons);
            Assert.Single(capturedPokemons);
            Assert.Equal("Pikachu", capturedPokemons[0].PokemonName);
        }

        [Fact]
        public async Task GetCapturedPokemonsAsync_ShouldReturnEmptyList_WhenNoCapturesExist()
        {
            // Act
            var capturedPokemons = await _captureService.GetCapturedPokemonsAsync();

            // Assert
            Assert.Empty(capturedPokemons);
        }
    }
}
