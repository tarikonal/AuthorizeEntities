
using Business.Handlers.Birims.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Birims.Queries.GetBirimQuery;
using Entities.Concrete;
using static Business.Handlers.Birims.Queries.GetBirimsQuery;
using static Business.Handlers.Birims.Commands.CreateBirimCommand;
using Business.Handlers.Birims.Commands;
using Business.Constants;
using static Business.Handlers.Birims.Commands.UpdateBirimCommand;
using static Business.Handlers.Birims.Commands.DeleteBirimCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BirimHandlerTests
    {
        Mock<IBirimRepository> _birimRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _birimRepository = new Mock<IBirimRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Birim_GetQuery_Success()
        {
            //Arrange
            var query = new GetBirimQuery();

            _birimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Birim, bool>>>())).ReturnsAsync(new Birim()
//propertyler buraya yazılacak
//{																		
//BirimId = 1,
//BirimName = "Test"
//}
);

            var handler = new GetBirimQueryHandler(_birimRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BirimId.Should().Be(1);

        }

        [Test]
        public async Task Birim_GetQueries_Success()
        {
            //Arrange
            var query = new GetBirimsQuery();

            _birimRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Birim, bool>>>()))
                        .ReturnsAsync(new List<Birim> { new Birim() { /*TODO:propertyler buraya yazılacak BirimId = 1, BirimName = "test"*/ } });

            var handler = new GetBirimsQueryHandler(_birimRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Birim>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Birim_CreateCommand_Success()
        {
            Birim rt = null;
            //Arrange
            var command = new CreateBirimCommand();
            //propertyler buraya yazılacak
            //command.BirimName = "deneme";

            _birimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Birim, bool>>>()))
                        .ReturnsAsync(rt);

            _birimRepository.Setup(x => x.Add(It.IsAny<Birim>())).Returns(new Birim());

            var handler = new CreateBirimCommandHandler(_birimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Birim_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBirimCommand();
            //propertyler buraya yazılacak 
            //command.BirimName = "test";

            _birimRepository.Setup(x => x.Query())
                                           .Returns(new List<Birim> { new Birim() { /*TODO:propertyler buraya yazılacak BirimId = 1, BirimName = "test"*/ } }.AsQueryable());

            _birimRepository.Setup(x => x.Add(It.IsAny<Birim>())).Returns(new Birim());

            var handler = new CreateBirimCommandHandler(_birimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Birim_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBirimCommand();
            //command.BirimName = "test";

            _birimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Birim, bool>>>()))
                        .ReturnsAsync(new Birim() { /*TODO:propertyler buraya yazılacak BirimId = 1, BirimName = "deneme"*/ });

            _birimRepository.Setup(x => x.Update(It.IsAny<Birim>())).Returns(new Birim());

            var handler = new UpdateBirimCommandHandler(_birimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Birim_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBirimCommand();

            _birimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Birim, bool>>>()))
                        .ReturnsAsync(new Birim() { /*TODO:propertyler buraya yazılacak BirimId = 1, BirimName = "deneme"*/});

            _birimRepository.Setup(x => x.Delete(It.IsAny<Birim>()));

            var handler = new DeleteBirimCommandHandler(_birimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

