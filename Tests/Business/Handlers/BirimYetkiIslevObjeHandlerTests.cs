
using Business.Handlers.BirimYetkiIslevObjes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.BirimYetkiIslevObjes.Queries.GetBirimYetkiIslevObjeQuery;
using Entities.Concrete;
using static Business.Handlers.BirimYetkiIslevObjes.Queries.GetBirimYetkiIslevObjesQuery;
using static Business.Handlers.BirimYetkiIslevObjes.Commands.CreateBirimYetkiIslevObjeCommand;
using Business.Handlers.BirimYetkiIslevObjes.Commands;
using Business.Constants;
using static Business.Handlers.BirimYetkiIslevObjes.Commands.UpdateBirimYetkiIslevObjeCommand;
using static Business.Handlers.BirimYetkiIslevObjes.Commands.DeleteBirimYetkiIslevObjeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BirimYetkiIslevObjeHandlerTests
    {
        Mock<IBirimYetkiIslevObjeRepository> _birimYetkiIslevObjeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _birimYetkiIslevObjeRepository = new Mock<IBirimYetkiIslevObjeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task BirimYetkiIslevObje_GetQuery_Success()
        {
            //Arrange
            var query = new GetBirimYetkiIslevObjeQuery();

            _birimYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimYetkiIslevObje, bool>>>())).ReturnsAsync(new BirimYetkiIslevObje()
//propertyler buraya yazılacak
//{																		
//BirimYetkiIslevObjeId = 1,
//BirimYetkiIslevObjeName = "Test"
//}
);

            var handler = new GetBirimYetkiIslevObjeQueryHandler(_birimYetkiIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BirimYetkiIslevObjeId.Should().Be(1);

        }

        [Test]
        public async Task BirimYetkiIslevObje_GetQueries_Success()
        {
            //Arrange
            var query = new GetBirimYetkiIslevObjesQuery();

            _birimYetkiIslevObjeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<BirimYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new List<BirimYetkiIslevObje> { new BirimYetkiIslevObje() { /*TODO:propertyler buraya yazılacak BirimYetkiIslevObjeId = 1, BirimYetkiIslevObjeName = "test"*/ } });

            var handler = new GetBirimYetkiIslevObjesQueryHandler(_birimYetkiIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<BirimYetkiIslevObje>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task BirimYetkiIslevObje_CreateCommand_Success()
        {
            BirimYetkiIslevObje rt = null;
            //Arrange
            var command = new CreateBirimYetkiIslevObjeCommand();
            //propertyler buraya yazılacak
            //command.BirimYetkiIslevObjeName = "deneme";

            _birimYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(rt);

            _birimYetkiIslevObjeRepository.Setup(x => x.Add(It.IsAny<BirimYetkiIslevObje>())).Returns(new BirimYetkiIslevObje());

            var handler = new CreateBirimYetkiIslevObjeCommandHandler(_birimYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task BirimYetkiIslevObje_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBirimYetkiIslevObjeCommand();
            //propertyler buraya yazılacak 
            //command.BirimYetkiIslevObjeName = "test";

            _birimYetkiIslevObjeRepository.Setup(x => x.Query())
                                           .Returns(new List<BirimYetkiIslevObje> { new BirimYetkiIslevObje() { /*TODO:propertyler buraya yazılacak BirimYetkiIslevObjeId = 1, BirimYetkiIslevObjeName = "test"*/ } }.AsQueryable());

            _birimYetkiIslevObjeRepository.Setup(x => x.Add(It.IsAny<BirimYetkiIslevObje>())).Returns(new BirimYetkiIslevObje());

            var handler = new CreateBirimYetkiIslevObjeCommandHandler(_birimYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task BirimYetkiIslevObje_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBirimYetkiIslevObjeCommand();
            //command.BirimYetkiIslevObjeName = "test";

            _birimYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new BirimYetkiIslevObje() { /*TODO:propertyler buraya yazılacak BirimYetkiIslevObjeId = 1, BirimYetkiIslevObjeName = "deneme"*/ });

            _birimYetkiIslevObjeRepository.Setup(x => x.Update(It.IsAny<BirimYetkiIslevObje>())).Returns(new BirimYetkiIslevObje());

            var handler = new UpdateBirimYetkiIslevObjeCommandHandler(_birimYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task BirimYetkiIslevObje_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBirimYetkiIslevObjeCommand();

            _birimYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new BirimYetkiIslevObje() { /*TODO:propertyler buraya yazılacak BirimYetkiIslevObjeId = 1, BirimYetkiIslevObjeName = "deneme"*/});

            _birimYetkiIslevObjeRepository.Setup(x => x.Delete(It.IsAny<BirimYetkiIslevObje>()));

            var handler = new DeleteBirimYetkiIslevObjeCommandHandler(_birimYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

