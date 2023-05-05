
using Business.Handlers.Objes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Objes.Queries.GetObjeQuery;
using Entities.Concrete;
using static Business.Handlers.Objes.Queries.GetObjesQuery;
using static Business.Handlers.Objes.Commands.CreateObjeCommand;
using Business.Handlers.Objes.Commands;
using Business.Constants;
using static Business.Handlers.Objes.Commands.UpdateObjeCommand;
using static Business.Handlers.Objes.Commands.DeleteObjeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ObjeHandlerTests
    {
        Mock<IObjeRepository> _objeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _objeRepository = new Mock<IObjeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Obje_GetQuery_Success()
        {
            //Arrange
            var query = new GetObjeQuery();

            _objeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Obje, bool>>>())).ReturnsAsync(new Obje()
//propertyler buraya yazılacak
//{																		
//ObjeId = 1,
//ObjeName = "Test"
//}
);

            var handler = new GetObjeQueryHandler(_objeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ObjeId.Should().Be(1);

        }

        [Test]
        public async Task Obje_GetQueries_Success()
        {
            //Arrange
            var query = new GetObjesQuery();

            _objeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Obje, bool>>>()))
                        .ReturnsAsync(new List<Obje> { new Obje() { /*TODO:propertyler buraya yazılacak ObjeId = 1, ObjeName = "test"*/ } });

            var handler = new GetObjesQueryHandler(_objeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Obje>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Obje_CreateCommand_Success()
        {
            Obje rt = null;
            //Arrange
            var command = new CreateObjeCommand();
            //propertyler buraya yazılacak
            //command.ObjeName = "deneme";

            _objeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Obje, bool>>>()))
                        .ReturnsAsync(rt);

            _objeRepository.Setup(x => x.Add(It.IsAny<Obje>())).Returns(new Obje());

            var handler = new CreateObjeCommandHandler(_objeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _objeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Obje_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateObjeCommand();
            //propertyler buraya yazılacak 
            //command.ObjeName = "test";

            _objeRepository.Setup(x => x.Query())
                                           .Returns(new List<Obje> { new Obje() { /*TODO:propertyler buraya yazılacak ObjeId = 1, ObjeName = "test"*/ } }.AsQueryable());

            _objeRepository.Setup(x => x.Add(It.IsAny<Obje>())).Returns(new Obje());

            var handler = new CreateObjeCommandHandler(_objeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Obje_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateObjeCommand();
            //command.ObjeName = "test";

            _objeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Obje, bool>>>()))
                        .ReturnsAsync(new Obje() { /*TODO:propertyler buraya yazılacak ObjeId = 1, ObjeName = "deneme"*/ });

            _objeRepository.Setup(x => x.Update(It.IsAny<Obje>())).Returns(new Obje());

            var handler = new UpdateObjeCommandHandler(_objeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _objeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Obje_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteObjeCommand();

            _objeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Obje, bool>>>()))
                        .ReturnsAsync(new Obje() { /*TODO:propertyler buraya yazılacak ObjeId = 1, ObjeName = "deneme"*/});

            _objeRepository.Setup(x => x.Delete(It.IsAny<Obje>()));

            var handler = new DeleteObjeCommandHandler(_objeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _objeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

