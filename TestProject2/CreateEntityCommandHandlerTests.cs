using AutoMapper;
using GenCo.Application.BusinessRules.Entities;
using GenCo.Application.DTOs.Entity.Requests;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Exceptions;
using GenCo.Application.Features.Entities.Commands.CreateEntity;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using Moq;

namespace TestProject2;

[TestClass]
public class CreateEntityCommandHandlerTests(
    Mock<IGenericRepository<Entity>> entityRepoMock,
    Mock<IEntityBusinessRules> rulesMock,
    Mock<IUnitOfWork> unitOfWorkMock,
    CreateEntityCommandHandler handler)
{
    private Mock<IGenericRepository<Entity>> _entityRepoMock = entityRepoMock;
    private Mock<IEntityBusinessRules> _rulesMock = rulesMock;
    private Mock<IUnitOfWork> _unitOfWorkMock = unitOfWorkMock;
    private CreateEntityCommandHandler _handler = handler;

    [TestInitialize]
    public void Setup()
    {
        _entityRepoMock = new Mock<IGenericRepository<Entity>>();
        _rulesMock = new Mock<IEntityBusinessRules>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateEntityRequestDto, Entity>();
            cfg.CreateMap<Entity, EntityResponseDto>();
        });
        var mapper = config.CreateMapper();

        _handler = new CreateEntityCommandHandler(
            _entityRepoMock.Object,
            _rulesMock.Object,
            _unitOfWorkMock.Object,
            mapper
        );
    }

    [TestMethod]
    public async Task Handle_Should_CreateEntity_When_ValidRequest()
    {
        // Arrange
        var request = new CreateEntityRequestDto
        {
            ProjectId = Guid.NewGuid(),
            Name = "Customer",
            Label = "Khách hàng"
        };

        _rulesMock.Setup(r => r.EnsureProjectExistsAsync(request.ProjectId, It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        _rulesMock.Setup(r => r.EnsureEntityNameUniqueOnCreateAsync(request.ProjectId, request.Name, It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        _entityRepoMock.Setup(r => r.AddAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Entity e, CancellationToken _) => e);

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(1);

        var command = new CreateEntityCommand(request);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Entity created successfully", result.Message);
        Assert.AreEqual(request.Name, result.Data!.Name);

        _rulesMock.Verify(r => r.EnsureProjectExistsAsync(request.ProjectId, It.IsAny<CancellationToken>()), Times.Once);
        _rulesMock.Verify(r => r.EnsureEntityNameUniqueOnCreateAsync(request.ProjectId, request.Name, It.IsAny<CancellationToken>()), Times.Once);
        _entityRepoMock.Verify(r => r.AddAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_Should_Fail_When_ProjectNotExist()
    {
        // Arrange
        var request = new CreateEntityRequestDto
        {
            ProjectId = Guid.NewGuid(),
            Name = "Customer"
        };

        _rulesMock.Setup(r => r.EnsureProjectExistsAsync(request.ProjectId, It.IsAny<CancellationToken>()))
                  .ThrowsAsync(new BusinessRuleValidationException("Project not found", "PROJECT_NOT_FOUND"));

        var command = new CreateEntityCommand(request);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<BusinessRuleValidationException>(async () =>
        {
            await _handler.Handle(command, CancellationToken.None);
        });

        _rulesMock.Verify(r => r.EnsureProjectExistsAsync(request.ProjectId, It.IsAny<CancellationToken>()), Times.Once);
        _entityRepoMock.Verify(r => r.AddAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task Handle_Should_Fail_When_EntityNameNotUnique()
    {
        // Arrange
        var request = new CreateEntityRequestDto
        {
            ProjectId = Guid.NewGuid(),
            Name = "Customer"
        };

        _rulesMock.Setup(r => r.EnsureProjectExistsAsync(request.ProjectId, It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        _rulesMock.Setup(r => r.EnsureEntityNameUniqueOnCreateAsync(request.ProjectId, request.Name, It.IsAny<CancellationToken>()))
                  .ThrowsAsync(new BusinessRuleValidationException("Entity name already exists", "ENTITY_NAME_EXISTS"));

        var command = new CreateEntityCommand(request);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<BusinessRuleValidationException>(async () =>
        {
            await _handler.Handle(command, CancellationToken.None);
        });

        _rulesMock.Verify(r => r.EnsureProjectExistsAsync(request.ProjectId, It.IsAny<CancellationToken>()), Times.Once);
        _rulesMock.Verify(r => r.EnsureEntityNameUniqueOnCreateAsync(request.ProjectId, request.Name, It.IsAny<CancellationToken>()), Times.Once);
        _entityRepoMock.Verify(r => r.AddAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task Handle_Should_Fail_When_SaveChangesReturnsZero()
    {
        // Arrange
        var request = new CreateEntityRequestDto
        {
            ProjectId = Guid.NewGuid(),
            Name = "Customer",
            Label = "Khách hàng"
        };

        _rulesMock.Setup(r => r.EnsureProjectExistsAsync(request.ProjectId, It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        _rulesMock.Setup(r => r.EnsureEntityNameUniqueOnCreateAsync(request.ProjectId, request.Name, It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

        _entityRepoMock.Setup(r => r.AddAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Entity e, CancellationToken _) => e);

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(0); // giả lập commit fail

        var command = new CreateEntityCommand(request);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<BusinessRuleValidationException>(async () =>
        {
            await _handler.Handle(command, CancellationToken.None);
        });

        _entityRepoMock.Verify(r => r.AddAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
