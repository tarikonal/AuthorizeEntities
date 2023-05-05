
using Business.Handlers.RolYetkiIslevObjes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.RolYetkiIslevObjes.Queries.GetRolYetkiIslevObjeQuery;
using Entities.Concrete;
using static Business.Handlers.RolYetkiIslevObjes.Queries.GetRolYetkiIslevObjesQuery;
using static Business.Handlers.RolYetkiIslevObjes.Commands.CreateRolYetkiIslevObjeCommand;
using Business.Handlers.RolYetkiIslevObjes.Commands;
using Business.Constants;
using static Business.Handlers.RolYetkiIslevObjes.Commands.UpdateRolYetkiIslevObjeCommand;
using static Business.Handlers.RolYetkiIslevObjes.Commands.DeleteRolYetkiIslevObjeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RolYetkiIslevObjeHandlerTests
    {
        Mock<IRolYetkiIslevObjeRepository> _rolYetkiIslevObjeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _rolYetkiIslevObjeRepository = new Mock<IRolYetkiIslevObjeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task RolYetkiIslevObje_GetQuery_Success()
        {
            //Arrange
            var query = new GetRolYetkiIslevObjeQuery();

            _rolYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RolYetkiIslevObje, bool>>>())).ReturnsAsync(new RolYetkiIslevObje()
//propertyler buraya yazılacak
//{																		
//RolYetkiIslevObjeId = 1,
//RolYetkiIslevObjeName = "Test"
//}
);

            var handler = new GetRolYetkiIslevObjeQueryHandler(_rolYetkiIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RolYetkiIslevObjeId.Should().Be(1);

        }

        [Test]
        public async Task RolYetkiIslevObje_GetQueries_Success()
        {
            //Arrange
            var query = new GetRolYetkiIslevObjesQuery();

            _rolYetkiIslevObjeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<RolYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new List<RolYetkiIslevObje> { new RolYetkiIslevObje() { /*TODO:propertyler buraya yazılacak RolYetkiIslevObjeId = 1, RolYetkiIslevObjeName = "test"*/ } });

            var handler = new GetRolYetkiIslevObjesQueryHandler(_rolYetkiIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<RolYetkiIslevObje>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task RolYetkiIslevObje_CreateCommand_Success()
        {
            RolYetkiIslevObje rt = null;
            //Arrange
            var command = new CreateRolYetkiIslevObjeCommand();
            //propertyler buraya yazılacak
            //command.RolYetkiIslevObjeName = "deneme";

            _rolYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RolYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(rt);

            _rolYetkiIslevObjeRepository.Setup(x => x.Add(It.IsAny<RolYetkiIslevObje>())).Returns(new RolYetkiIslevObje());

            var handler = new CreateRolYetkiIslevObjeCommandHandler(_rolYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task RolYetkiIslevObje_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRolYetkiIslevObjeCommand();
            //propertyler buraya yazılacak 
            //command.RolYetkiIslevObjeName = "test";

            _rolYetkiIslevObjeRepository.Setup(x => x.Query())
                                           .Returns(new List<RolYetkiIslevObje> { new RolYetkiIslevObje() { /*TODO:propertyler buraya yazılacak RolYetkiIslevObjeId = 1, RolYetkiIslevObjeName = "test"*/ } }.AsQueryable());

            _rolYetkiIslevObjeRepository.Setup(x => x.Add(It.IsAny<RolYetkiIslevObje>())).Returns(new RolYetkiIslevObje());

            var handler = new CreateRolYetkiIslevObjeCommandHandler(_rolYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task RolYetkiIslevObje_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRolYetkiIslevObjeCommand();
            //command.RolYetkiIslevObjeName = "test";

            _rolYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RolYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new RolYetkiIslevObje() { /*TODO:propertyler buraya yazılacak RolYetkiIslevObjeId = 1, RolYetkiIslevObjeName = "deneme"*/ });

            _rolYetkiIslevObjeRepository.Setup(x => x.Update(It.IsAny<RolYetkiIslevObje>())).Returns(new RolYetkiIslevObje());

            var handler = new UpdateRolYetkiIslevObjeCommandHandler(_rolYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task RolYetkiIslevObje_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRolYetkiIslevObjeCommand();

            _rolYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RolYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new RolYetkiIslevObje() { /*TODO:propertyler buraya yazılacak RolYetkiIslevObjeId = 1, RolYetkiIslevObjeName = "deneme"*/});

            _rolYetkiIslevObjeRepository.Setup(x => x.Delete(It.IsAny<RolYetkiIslevObje>()));

            var handler = new DeleteRolYetkiIslevObjeCommandHandler(_rolYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _rolYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

