
using Business.Handlers.BirimAgacs.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.BirimAgacs.Queries.GetBirimAgacQuery;
using Entities.Concrete;
using static Business.Handlers.BirimAgacs.Queries.GetBirimAgacsQuery;
using static Business.Handlers.BirimAgacs.Commands.CreateBirimAgacCommand;
using Business.Handlers.BirimAgacs.Commands;
using Business.Constants;
using static Business.Handlers.BirimAgacs.Commands.UpdateBirimAgacCommand;
using static Business.Handlers.BirimAgacs.Commands.DeleteBirimAgacCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BirimAgacHandlerTests
    {
        Mock<IBirimAgacRepository> _birimAgacRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _birimAgacRepository = new Mock<IBirimAgacRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task BirimAgac_GetQuery_Success()
        {
            //Arrange
            var query = new GetBirimAgacQuery();

            _birimAgacRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimAgac, bool>>>())).ReturnsAsync(new BirimAgac()
//propertyler buraya yazılacak
//{																		
//BirimAgacId = 1,
//BirimAgacName = "Test"
//}
);

            var handler = new GetBirimAgacQueryHandler(_birimAgacRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BirimAgacId.Should().Be(1);

        }

        [Test]
        public async Task BirimAgac_GetQueries_Success()
        {
            //Arrange
            var query = new GetBirimAgacsQuery();

            _birimAgacRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<BirimAgac, bool>>>()))
                        .ReturnsAsync(new List<BirimAgac> { new BirimAgac() { /*TODO:propertyler buraya yazılacak BirimAgacId = 1, BirimAgacName = "test"*/ } });

            var handler = new GetBirimAgacsQueryHandler(_birimAgacRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<BirimAgac>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task BirimAgac_CreateCommand_Success()
        {
            BirimAgac rt = null;
            //Arrange
            var command = new CreateBirimAgacCommand();
            //propertyler buraya yazılacak
            //command.BirimAgacName = "deneme";

            _birimAgacRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimAgac, bool>>>()))
                        .ReturnsAsync(rt);

            _birimAgacRepository.Setup(x => x.Add(It.IsAny<BirimAgac>())).Returns(new BirimAgac());

            var handler = new CreateBirimAgacCommandHandler(_birimAgacRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimAgacRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task BirimAgac_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBirimAgacCommand();
            //propertyler buraya yazılacak 
            //command.BirimAgacName = "test";

            _birimAgacRepository.Setup(x => x.Query())
                                           .Returns(new List<BirimAgac> { new BirimAgac() { /*TODO:propertyler buraya yazılacak BirimAgacId = 1, BirimAgacName = "test"*/ } }.AsQueryable());

            _birimAgacRepository.Setup(x => x.Add(It.IsAny<BirimAgac>())).Returns(new BirimAgac());

            var handler = new CreateBirimAgacCommandHandler(_birimAgacRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task BirimAgac_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBirimAgacCommand();
            //command.BirimAgacName = "test";

            _birimAgacRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimAgac, bool>>>()))
                        .ReturnsAsync(new BirimAgac() { /*TODO:propertyler buraya yazılacak BirimAgacId = 1, BirimAgacName = "deneme"*/ });

            _birimAgacRepository.Setup(x => x.Update(It.IsAny<BirimAgac>())).Returns(new BirimAgac());

            var handler = new UpdateBirimAgacCommandHandler(_birimAgacRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimAgacRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task BirimAgac_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBirimAgacCommand();

            _birimAgacRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimAgac, bool>>>()))
                        .ReturnsAsync(new BirimAgac() { /*TODO:propertyler buraya yazılacak BirimAgacId = 1, BirimAgacName = "deneme"*/});

            _birimAgacRepository.Setup(x => x.Delete(It.IsAny<BirimAgac>()));

            var handler = new DeleteBirimAgacCommandHandler(_birimAgacRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimAgacRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

