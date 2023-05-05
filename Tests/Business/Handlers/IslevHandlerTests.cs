
using Business.Handlers.Islevs.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Islevs.Queries.GetIslevQuery;
using Entities.Concrete;
using static Business.Handlers.Islevs.Queries.GetIslevsQuery;
using static Business.Handlers.Islevs.Commands.CreateIslevCommand;
using Business.Handlers.Islevs.Commands;
using Business.Constants;
using static Business.Handlers.Islevs.Commands.UpdateIslevCommand;
using static Business.Handlers.Islevs.Commands.DeleteIslevCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class IslevHandlerTests
    {
        Mock<IIslevRepository> _islevRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _islevRepository = new Mock<IIslevRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Islev_GetQuery_Success()
        {
            //Arrange
            var query = new GetIslevQuery();

            _islevRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Islev, bool>>>())).ReturnsAsync(new Islev()
//propertyler buraya yazılacak
//{																		
//IslevId = 1,
//IslevName = "Test"
//}
);

            var handler = new GetIslevQueryHandler(_islevRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.IslevId.Should().Be(1);

        }

        [Test]
        public async Task Islev_GetQueries_Success()
        {
            //Arrange
            var query = new GetIslevsQuery();

            _islevRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Islev, bool>>>()))
                        .ReturnsAsync(new List<Islev> { new Islev() { /*TODO:propertyler buraya yazılacak IslevId = 1, IslevName = "test"*/ } });

            var handler = new GetIslevsQueryHandler(_islevRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Islev>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Islev_CreateCommand_Success()
        {
            Islev rt = null;
            //Arrange
            var command = new CreateIslevCommand();
            //propertyler buraya yazılacak
            //command.IslevName = "deneme";

            _islevRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Islev, bool>>>()))
                        .ReturnsAsync(rt);

            _islevRepository.Setup(x => x.Add(It.IsAny<Islev>())).Returns(new Islev());

            var handler = new CreateIslevCommandHandler(_islevRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _islevRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Islev_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateIslevCommand();
            //propertyler buraya yazılacak 
            //command.IslevName = "test";

            _islevRepository.Setup(x => x.Query())
                                           .Returns(new List<Islev> { new Islev() { /*TODO:propertyler buraya yazılacak IslevId = 1, IslevName = "test"*/ } }.AsQueryable());

            _islevRepository.Setup(x => x.Add(It.IsAny<Islev>())).Returns(new Islev());

            var handler = new CreateIslevCommandHandler(_islevRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Islev_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateIslevCommand();
            //command.IslevName = "test";

            _islevRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Islev, bool>>>()))
                        .ReturnsAsync(new Islev() { /*TODO:propertyler buraya yazılacak IslevId = 1, IslevName = "deneme"*/ });

            _islevRepository.Setup(x => x.Update(It.IsAny<Islev>())).Returns(new Islev());

            var handler = new UpdateIslevCommandHandler(_islevRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _islevRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Islev_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteIslevCommand();

            _islevRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Islev, bool>>>()))
                        .ReturnsAsync(new Islev() { /*TODO:propertyler buraya yazılacak IslevId = 1, IslevName = "deneme"*/});

            _islevRepository.Setup(x => x.Delete(It.IsAny<Islev>()));

            var handler = new DeleteIslevCommandHandler(_islevRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _islevRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

