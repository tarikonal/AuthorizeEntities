
using Business.Handlers.RolMenuIslevObjes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.RolMenuIslevObjes.Queries.GetRolMenuIslevObjeQuery;
using Entities.Concrete;
using static Business.Handlers.RolMenuIslevObjes.Queries.GetRolMenuIslevObjesQuery;
using static Business.Handlers.RolMenuIslevObjes.Commands.CreateRolMenuIslevObjeCommand;
using Business.Handlers.RolMenuIslevObjes.Commands;
using Business.Constants;
using static Business.Handlers.RolMenuIslevObjes.Commands.UpdateRolMenuIslevObjeCommand;
using static Business.Handlers.RolMenuIslevObjes.Commands.DeleteRolMenuIslevObjeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RolMenuIslevObjeHandlerTests
    {
        Mock<IRolMenuIslevObjeRepository> _rolMenuIslevObjeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _rolMenuIslevObjeRepository = new Mock<IRolMenuIslevObjeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task RolMenuIslevObje_GetQuery_Success()
        {
            //Arrange
            var query = new GetRolMenuIslevObjeQuery();

            _rolMenuIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RolMenuIslevObje, bool>>>())).ReturnsAsync(new RolMenuIslevObje()
//propertyler buraya yazılacak
//{																		
//RolMenuIslevObjeId = 1,
//RolMenuIslevObjeName = "Test"
//}
);

            var handler = new GetRolMenuIslevObjeQueryHandler(_rolMenuIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RolMenuIslevObjeId.Should().Be(1);

        }

        [Test]
        public async Task RolMenuIslevObje_GetQueries_Success()
        {
            //Arrange
            var query = new GetRolMenuIslevObjesQuery();

            _rolMenuIslevObjeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<RolMenuIslevObje, bool>>>()))
                        .ReturnsAsync(new List<RolMenuIslevObje> { new RolMenuIslevObje() { /*TODO:propertyler buraya yazılacak RolMenuIslevObjeId = 1, RolMenuIslevObjeName = "test"*/ } });

            var handler = new GetRolMenuIslevObjesQueryHandler(_rolMenuIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<RolMenuIslevObje>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task RolMenuIslevObje_CreateCommand_Success()
        {
            RolMenuIslevObje rt = null;
            //Arrange
            var command = new CreateRolMenuIslevObjeCommand();
            //propertyler buraya yazılacak
            //command.RolMenuIslevObjeName = "deneme";

            _rolMenuIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RolMenuIslevObje, bool>>>()))
                        .ReturnsAsync(rt);

            _rolMenuIslevObjeRepository.Setup(x => x.Add(It.IsAny<RolMenuIslevObje>())).Returns(new RolMenuIslevObje());

            var handler = new CreateRolMenuIslevObjeCommandHandler(_rolMenuIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolMenuIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task RolMenuIslevObje_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRolMenuIslevObjeCommand();
            //propertyler buraya yazılacak 
            //command.RolMenuIslevObjeName = "test";

            _rolMenuIslevObjeRepository.Setup(x => x.Query())
                                           .Returns(new List<RolMenuIslevObje> { new RolMenuIslevObje() { /*TODO:propertyler buraya yazılacak RolMenuIslevObjeId = 1, RolMenuIslevObjeName = "test"*/ } }.AsQueryable());

            _rolMenuIslevObjeRepository.Setup(x => x.Add(It.IsAny<RolMenuIslevObje>())).Returns(new RolMenuIslevObje());

            var handler = new CreateRolMenuIslevObjeCommandHandler(_rolMenuIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task RolMenuIslevObje_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRolMenuIslevObjeCommand();
            //command.RolMenuIslevObjeName = "test";

            _rolMenuIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RolMenuIslevObje, bool>>>()))
                        .ReturnsAsync(new RolMenuIslevObje() { /*TODO:propertyler buraya yazılacak RolMenuIslevObjeId = 1, RolMenuIslevObjeName = "deneme"*/ });

            _rolMenuIslevObjeRepository.Setup(x => x.Update(It.IsAny<RolMenuIslevObje>())).Returns(new RolMenuIslevObje());

            var handler = new UpdateRolMenuIslevObjeCommandHandler(_rolMenuIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolMenuIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task RolMenuIslevObje_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRolMenuIslevObjeCommand();

            _rolMenuIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RolMenuIslevObje, bool>>>()))
                        .ReturnsAsync(new RolMenuIslevObje() { /*TODO:propertyler buraya yazılacak RolMenuIslevObjeId = 1, RolMenuIslevObjeName = "deneme"*/});

            _rolMenuIslevObjeRepository.Setup(x => x.Delete(It.IsAny<RolMenuIslevObje>()));

            var handler = new DeleteRolMenuIslevObjeCommandHandler(_rolMenuIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolMenuIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

