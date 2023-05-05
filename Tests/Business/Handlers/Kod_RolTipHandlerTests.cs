
using Business.Handlers.Kod_RolTips.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Kod_RolTips.Queries.GetKod_RolTipQuery;
using Entities.Concrete;
using static Business.Handlers.Kod_RolTips.Queries.GetKod_RolTipsQuery;
using static Business.Handlers.Kod_RolTips.Commands.CreateKod_RolTipCommand;
using Business.Handlers.Kod_RolTips.Commands;
using Business.Constants;
using static Business.Handlers.Kod_RolTips.Commands.UpdateKod_RolTipCommand;
using static Business.Handlers.Kod_RolTips.Commands.DeleteKod_RolTipCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class Kod_RolTipHandlerTests
    {
        Mock<IKod_RolTipRepository> _kod_RolTipRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kod_RolTipRepository = new Mock<IKod_RolTipRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Kod_RolTip_GetQuery_Success()
        {
            //Arrange
            var query = new GetKod_RolTipQuery();

            _kod_RolTipRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_RolTip, bool>>>())).ReturnsAsync(new Kod_RolTip()
//propertyler buraya yazılacak
//{																		
//Kod_RolTipId = 1,
//Kod_RolTipName = "Test"
//}
);

            var handler = new GetKod_RolTipQueryHandler(_kod_RolTipRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.Kod_RolTipId.Should().Be(1);

        }

        [Test]
        public async Task Kod_RolTip_GetQueries_Success()
        {
            //Arrange
            var query = new GetKod_RolTipsQuery();

            _kod_RolTipRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Kod_RolTip, bool>>>()))
                        .ReturnsAsync(new List<Kod_RolTip> { new Kod_RolTip() { /*TODO:propertyler buraya yazılacak Kod_RolTipId = 1, Kod_RolTipName = "test"*/ } });

            var handler = new GetKod_RolTipsQueryHandler(_kod_RolTipRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Kod_RolTip>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Kod_RolTip_CreateCommand_Success()
        {
            Kod_RolTip rt = null;
            //Arrange
            var command = new CreateKod_RolTipCommand();
            //propertyler buraya yazılacak
            //command.Kod_RolTipName = "deneme";

            _kod_RolTipRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_RolTip, bool>>>()))
                        .ReturnsAsync(rt);

            _kod_RolTipRepository.Setup(x => x.Add(It.IsAny<Kod_RolTip>())).Returns(new Kod_RolTip());

            var handler = new CreateKod_RolTipCommandHandler(_kod_RolTipRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_RolTipRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Kod_RolTip_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKod_RolTipCommand();
            //propertyler buraya yazılacak 
            //command.Kod_RolTipName = "test";

            _kod_RolTipRepository.Setup(x => x.Query())
                                           .Returns(new List<Kod_RolTip> { new Kod_RolTip() { /*TODO:propertyler buraya yazılacak Kod_RolTipId = 1, Kod_RolTipName = "test"*/ } }.AsQueryable());

            _kod_RolTipRepository.Setup(x => x.Add(It.IsAny<Kod_RolTip>())).Returns(new Kod_RolTip());

            var handler = new CreateKod_RolTipCommandHandler(_kod_RolTipRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Kod_RolTip_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKod_RolTipCommand();
            //command.Kod_RolTipName = "test";

            _kod_RolTipRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_RolTip, bool>>>()))
                        .ReturnsAsync(new Kod_RolTip() { /*TODO:propertyler buraya yazılacak Kod_RolTipId = 1, Kod_RolTipName = "deneme"*/ });

            _kod_RolTipRepository.Setup(x => x.Update(It.IsAny<Kod_RolTip>())).Returns(new Kod_RolTip());

            var handler = new UpdateKod_RolTipCommandHandler(_kod_RolTipRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_RolTipRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Kod_RolTip_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKod_RolTipCommand();

            _kod_RolTipRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_RolTip, bool>>>()))
                        .ReturnsAsync(new Kod_RolTip() { /*TODO:propertyler buraya yazılacak Kod_RolTipId = 1, Kod_RolTipName = "deneme"*/});

            _kod_RolTipRepository.Setup(x => x.Delete(It.IsAny<Kod_RolTip>()));

            var handler = new DeleteKod_RolTipCommandHandler(_kod_RolTipRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_RolTipRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

