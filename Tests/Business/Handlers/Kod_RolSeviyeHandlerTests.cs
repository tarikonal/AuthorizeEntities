
using Business.Handlers.Kod_RolSeviyes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Kod_RolSeviyes.Queries.GetKod_RolSeviyeQuery;
using Entities.Concrete;
using static Business.Handlers.Kod_RolSeviyes.Queries.GetKod_RolSeviyesQuery;
using static Business.Handlers.Kod_RolSeviyes.Commands.CreateKod_RolSeviyeCommand;
using Business.Handlers.Kod_RolSeviyes.Commands;
using Business.Constants;
using static Business.Handlers.Kod_RolSeviyes.Commands.UpdateKod_RolSeviyeCommand;
using static Business.Handlers.Kod_RolSeviyes.Commands.DeleteKod_RolSeviyeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class Kod_RolSeviyeHandlerTests
    {
        Mock<IKod_RolSeviyeRepository> _kod_RolSeviyeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kod_RolSeviyeRepository = new Mock<IKod_RolSeviyeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Kod_RolSeviye_GetQuery_Success()
        {
            //Arrange
            var query = new GetKod_RolSeviyeQuery();

            _kod_RolSeviyeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_RolSeviye, bool>>>())).ReturnsAsync(new Kod_RolSeviye()
//propertyler buraya yazılacak
//{																		
//Kod_RolSeviyeId = 1,
//Kod_RolSeviyeName = "Test"
//}
);

            var handler = new GetKod_RolSeviyeQueryHandler(_kod_RolSeviyeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.Kod_RolSeviyeId.Should().Be(1);

        }

        [Test]
        public async Task Kod_RolSeviye_GetQueries_Success()
        {
            //Arrange
            var query = new GetKod_RolSeviyesQuery();

            _kod_RolSeviyeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Kod_RolSeviye, bool>>>()))
                        .ReturnsAsync(new List<Kod_RolSeviye> { new Kod_RolSeviye() { /*TODO:propertyler buraya yazılacak Kod_RolSeviyeId = 1, Kod_RolSeviyeName = "test"*/ } });

            var handler = new GetKod_RolSeviyesQueryHandler(_kod_RolSeviyeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Kod_RolSeviye>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Kod_RolSeviye_CreateCommand_Success()
        {
            Kod_RolSeviye rt = null;
            //Arrange
            var command = new CreateKod_RolSeviyeCommand();
            //propertyler buraya yazılacak
            //command.Kod_RolSeviyeName = "deneme";

            _kod_RolSeviyeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_RolSeviye, bool>>>()))
                        .ReturnsAsync(rt);

            _kod_RolSeviyeRepository.Setup(x => x.Add(It.IsAny<Kod_RolSeviye>())).Returns(new Kod_RolSeviye());

            var handler = new CreateKod_RolSeviyeCommandHandler(_kod_RolSeviyeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_RolSeviyeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Kod_RolSeviye_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKod_RolSeviyeCommand();
            //propertyler buraya yazılacak 
            //command.Kod_RolSeviyeName = "test";

            _kod_RolSeviyeRepository.Setup(x => x.Query())
                                           .Returns(new List<Kod_RolSeviye> { new Kod_RolSeviye() { /*TODO:propertyler buraya yazılacak Kod_RolSeviyeId = 1, Kod_RolSeviyeName = "test"*/ } }.AsQueryable());

            _kod_RolSeviyeRepository.Setup(x => x.Add(It.IsAny<Kod_RolSeviye>())).Returns(new Kod_RolSeviye());

            var handler = new CreateKod_RolSeviyeCommandHandler(_kod_RolSeviyeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Kod_RolSeviye_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKod_RolSeviyeCommand();
            //command.Kod_RolSeviyeName = "test";

            _kod_RolSeviyeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_RolSeviye, bool>>>()))
                        .ReturnsAsync(new Kod_RolSeviye() { /*TODO:propertyler buraya yazılacak Kod_RolSeviyeId = 1, Kod_RolSeviyeName = "deneme"*/ });

            _kod_RolSeviyeRepository.Setup(x => x.Update(It.IsAny<Kod_RolSeviye>())).Returns(new Kod_RolSeviye());

            var handler = new UpdateKod_RolSeviyeCommandHandler(_kod_RolSeviyeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_RolSeviyeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Kod_RolSeviye_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKod_RolSeviyeCommand();

            _kod_RolSeviyeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_RolSeviye, bool>>>()))
                        .ReturnsAsync(new Kod_RolSeviye() { /*TODO:propertyler buraya yazılacak Kod_RolSeviyeId = 1, Kod_RolSeviyeName = "deneme"*/});

            _kod_RolSeviyeRepository.Setup(x => x.Delete(It.IsAny<Kod_RolSeviye>()));

            var handler = new DeleteKod_RolSeviyeCommandHandler(_kod_RolSeviyeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_RolSeviyeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

