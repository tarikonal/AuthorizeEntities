
using Business.Handlers.Rols.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Rols.Queries.GetRolQuery;
using Entities.Concrete;
using static Business.Handlers.Rols.Queries.GetRolsQuery;
using static Business.Handlers.Rols.Commands.CreateRolCommand;
using Business.Handlers.Rols.Commands;
using Business.Constants;
using static Business.Handlers.Rols.Commands.UpdateRolCommand;
using static Business.Handlers.Rols.Commands.DeleteRolCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RolHandlerTests
    {
        Mock<IRolRepository> _rolRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _rolRepository = new Mock<IRolRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Rol_GetQuery_Success()
        {
            //Arrange
            var query = new GetRolQuery();

            _rolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Rol, bool>>>())).ReturnsAsync(new Rol()
//propertyler buraya yazılacak
//{																		
//RolId = 1,
//RolName = "Test"
//}
);

            var handler = new GetRolQueryHandler(_rolRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RolId.Should().Be(1);

        }

        [Test]
        public async Task Rol_GetQueries_Success()
        {
            //Arrange
            var query = new GetRolsQuery();

            _rolRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Rol, bool>>>()))
                        .ReturnsAsync(new List<Rol> { new Rol() { /*TODO:propertyler buraya yazılacak RolId = 1, RolName = "test"*/ } });

            var handler = new GetRolsQueryHandler(_rolRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Rol>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Rol_CreateCommand_Success()
        {
            Rol rt = null;
            //Arrange
            var command = new CreateRolCommand();
            //propertyler buraya yazılacak
            //command.RolName = "deneme";

            _rolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Rol, bool>>>()))
                        .ReturnsAsync(rt);

            _rolRepository.Setup(x => x.Add(It.IsAny<Rol>())).Returns(new Rol());

            var handler = new CreateRolCommandHandler(_rolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Rol_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRolCommand();
            //propertyler buraya yazılacak 
            //command.RolName = "test";

            _rolRepository.Setup(x => x.Query())
                                           .Returns(new List<Rol> { new Rol() { /*TODO:propertyler buraya yazılacak RolId = 1, RolName = "test"*/ } }.AsQueryable());

            _rolRepository.Setup(x => x.Add(It.IsAny<Rol>())).Returns(new Rol());

            var handler = new CreateRolCommandHandler(_rolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Rol_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRolCommand();
            //command.RolName = "test";

            _rolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Rol, bool>>>()))
                        .ReturnsAsync(new Rol() { /*TODO:propertyler buraya yazılacak RolId = 1, RolName = "deneme"*/ });

            _rolRepository.Setup(x => x.Update(It.IsAny<Rol>())).Returns(new Rol());

            var handler = new UpdateRolCommandHandler(_rolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Rol_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRolCommand();

            _rolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Rol, bool>>>()))
                        .ReturnsAsync(new Rol() { /*TODO:propertyler buraya yazılacak RolId = 1, RolName = "deneme"*/});

            _rolRepository.Setup(x => x.Delete(It.IsAny<Rol>()));

            var handler = new DeleteRolCommandHandler(_rolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

