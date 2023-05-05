
using Business.Handlers.Projes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Projes.Queries.GetProjeQuery;
using Entities.Concrete;
using static Business.Handlers.Projes.Queries.GetProjesQuery;
using static Business.Handlers.Projes.Commands.CreateProjeCommand;
using Business.Handlers.Projes.Commands;
using Business.Constants;
using static Business.Handlers.Projes.Commands.UpdateProjeCommand;
using static Business.Handlers.Projes.Commands.DeleteProjeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjeHandlerTests
    {
        Mock<IProjeRepository> _projeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projeRepository = new Mock<IProjeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Proje_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjeQuery();

            _projeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Proje, bool>>>())).ReturnsAsync(new Proje()
//propertyler buraya yazılacak
//{																		
//ProjeId = 1,
//ProjeName = "Test"
//}
);

            var handler = new GetProjeQueryHandler(_projeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjeId.Should().Be(1);

        }

        [Test]
        public async Task Proje_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjesQuery();

            _projeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Proje, bool>>>()))
                        .ReturnsAsync(new List<Proje> { new Proje() { /*TODO:propertyler buraya yazılacak ProjeId = 1, ProjeName = "test"*/ } });

            var handler = new GetProjesQueryHandler(_projeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Proje>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Proje_CreateCommand_Success()
        {
            Proje rt = null;
            //Arrange
            var command = new CreateProjeCommand();
            //propertyler buraya yazılacak
            //command.ProjeName = "deneme";

            _projeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Proje, bool>>>()))
                        .ReturnsAsync(rt);

            _projeRepository.Setup(x => x.Add(It.IsAny<Proje>())).Returns(new Proje());

            var handler = new CreateProjeCommandHandler(_projeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _projeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Proje_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjeCommand();
            //propertyler buraya yazılacak 
            //command.ProjeName = "test";

            _projeRepository.Setup(x => x.Query())
                                           .Returns(new List<Proje> { new Proje() { /*TODO:propertyler buraya yazılacak ProjeId = 1, ProjeName = "test"*/ } }.AsQueryable());

            _projeRepository.Setup(x => x.Add(It.IsAny<Proje>())).Returns(new Proje());

            var handler = new CreateProjeCommandHandler(_projeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Proje_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjeCommand();
            //command.ProjeName = "test";

            _projeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Proje, bool>>>()))
                        .ReturnsAsync(new Proje() { /*TODO:propertyler buraya yazılacak ProjeId = 1, ProjeName = "deneme"*/ });

            _projeRepository.Setup(x => x.Update(It.IsAny<Proje>())).Returns(new Proje());

            var handler = new UpdateProjeCommandHandler(_projeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _projeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Proje_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjeCommand();

            _projeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Proje, bool>>>()))
                        .ReturnsAsync(new Proje() { /*TODO:propertyler buraya yazılacak ProjeId = 1, ProjeName = "deneme"*/});

            _projeRepository.Setup(x => x.Delete(It.IsAny<Proje>()));

            var handler = new DeleteProjeCommandHandler(_projeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _projeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

