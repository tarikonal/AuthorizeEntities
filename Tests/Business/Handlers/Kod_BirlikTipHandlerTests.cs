
using Business.Handlers.Kod_BirlikTips.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Kod_BirlikTips.Queries.GetKod_BirlikTipQuery;
using Entities.Concrete;
using static Business.Handlers.Kod_BirlikTips.Queries.GetKod_BirlikTipsQuery;
using static Business.Handlers.Kod_BirlikTips.Commands.CreateKod_BirlikTipCommand;
using Business.Handlers.Kod_BirlikTips.Commands;
using Business.Constants;
using static Business.Handlers.Kod_BirlikTips.Commands.UpdateKod_BirlikTipCommand;
using static Business.Handlers.Kod_BirlikTips.Commands.DeleteKod_BirlikTipCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class Kod_BirlikTipHandlerTests
    {
        Mock<IKod_BirlikTipRepository> _kod_BirlikTipRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kod_BirlikTipRepository = new Mock<IKod_BirlikTipRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Kod_BirlikTip_GetQuery_Success()
        {
            //Arrange
            var query = new GetKod_BirlikTipQuery();

            _kod_BirlikTipRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_BirlikTip, bool>>>())).ReturnsAsync(new Kod_BirlikTip()
//propertyler buraya yazılacak
//{																		
//Kod_BirlikTipId = 1,
//Kod_BirlikTipName = "Test"
//}
);

            var handler = new GetKod_BirlikTipQueryHandler(_kod_BirlikTipRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.Kod_BirlikTipId.Should().Be(1);

        }

        [Test]
        public async Task Kod_BirlikTip_GetQueries_Success()
        {
            //Arrange
            var query = new GetKod_BirlikTipsQuery();

            _kod_BirlikTipRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Kod_BirlikTip, bool>>>()))
                        .ReturnsAsync(new List<Kod_BirlikTip> { new Kod_BirlikTip() { /*TODO:propertyler buraya yazılacak Kod_BirlikTipId = 1, Kod_BirlikTipName = "test"*/ } });

            var handler = new GetKod_BirlikTipsQueryHandler(_kod_BirlikTipRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Kod_BirlikTip>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Kod_BirlikTip_CreateCommand_Success()
        {
            Kod_BirlikTip rt = null;
            //Arrange
            var command = new CreateKod_BirlikTipCommand();
            //propertyler buraya yazılacak
            //command.Kod_BirlikTipName = "deneme";

            _kod_BirlikTipRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_BirlikTip, bool>>>()))
                        .ReturnsAsync(rt);

            _kod_BirlikTipRepository.Setup(x => x.Add(It.IsAny<Kod_BirlikTip>())).Returns(new Kod_BirlikTip());

            var handler = new CreateKod_BirlikTipCommandHandler(_kod_BirlikTipRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_BirlikTipRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Kod_BirlikTip_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKod_BirlikTipCommand();
            //propertyler buraya yazılacak 
            //command.Kod_BirlikTipName = "test";

            _kod_BirlikTipRepository.Setup(x => x.Query())
                                           .Returns(new List<Kod_BirlikTip> { new Kod_BirlikTip() { /*TODO:propertyler buraya yazılacak Kod_BirlikTipId = 1, Kod_BirlikTipName = "test"*/ } }.AsQueryable());

            _kod_BirlikTipRepository.Setup(x => x.Add(It.IsAny<Kod_BirlikTip>())).Returns(new Kod_BirlikTip());

            var handler = new CreateKod_BirlikTipCommandHandler(_kod_BirlikTipRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Kod_BirlikTip_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKod_BirlikTipCommand();
            //command.Kod_BirlikTipName = "test";

            _kod_BirlikTipRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_BirlikTip, bool>>>()))
                        .ReturnsAsync(new Kod_BirlikTip() { /*TODO:propertyler buraya yazılacak Kod_BirlikTipId = 1, Kod_BirlikTipName = "deneme"*/ });

            _kod_BirlikTipRepository.Setup(x => x.Update(It.IsAny<Kod_BirlikTip>())).Returns(new Kod_BirlikTip());

            var handler = new UpdateKod_BirlikTipCommandHandler(_kod_BirlikTipRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_BirlikTipRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Kod_BirlikTip_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKod_BirlikTipCommand();

            _kod_BirlikTipRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Kod_BirlikTip, bool>>>()))
                        .ReturnsAsync(new Kod_BirlikTip() { /*TODO:propertyler buraya yazılacak Kod_BirlikTipId = 1, Kod_BirlikTipName = "deneme"*/});

            _kod_BirlikTipRepository.Setup(x => x.Delete(It.IsAny<Kod_BirlikTip>()));

            var handler = new DeleteKod_BirlikTipCommandHandler(_kod_BirlikTipRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kod_BirlikTipRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

