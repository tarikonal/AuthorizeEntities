
using Business.Handlers.Yetkis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Yetkis.Queries.GetYetkiQuery;
using Entities.Concrete;
using static Business.Handlers.Yetkis.Queries.GetYetkisQuery;
using static Business.Handlers.Yetkis.Commands.CreateYetkiCommand;
using Business.Handlers.Yetkis.Commands;
using Business.Constants;
using static Business.Handlers.Yetkis.Commands.UpdateYetkiCommand;
using static Business.Handlers.Yetkis.Commands.DeleteYetkiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class YetkiHandlerTests
    {
        Mock<IYetkiRepository> _yetkiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _yetkiRepository = new Mock<IYetkiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Yetki_GetQuery_Success()
        {
            //Arrange
            var query = new GetYetkiQuery();

            _yetkiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yetki, bool>>>())).ReturnsAsync(new Yetki()
//propertyler buraya yazılacak
//{																		
//YetkiId = 1,
//YetkiName = "Test"
//}
);

            var handler = new GetYetkiQueryHandler(_yetkiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.YetkiId.Should().Be(1);

        }

        [Test]
        public async Task Yetki_GetQueries_Success()
        {
            //Arrange
            var query = new GetYetkisQuery();

            _yetkiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Yetki, bool>>>()))
                        .ReturnsAsync(new List<Yetki> { new Yetki() { /*TODO:propertyler buraya yazılacak YetkiId = 1, YetkiName = "test"*/ } });

            var handler = new GetYetkisQueryHandler(_yetkiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Yetki>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Yetki_CreateCommand_Success()
        {
            Yetki rt = null;
            //Arrange
            var command = new CreateYetkiCommand();
            //propertyler buraya yazılacak
            //command.YetkiName = "deneme";

            _yetkiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yetki, bool>>>()))
                        .ReturnsAsync(rt);

            _yetkiRepository.Setup(x => x.Add(It.IsAny<Yetki>())).Returns(new Yetki());

            var handler = new CreateYetkiCommandHandler(_yetkiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yetkiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Yetki_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateYetkiCommand();
            //propertyler buraya yazılacak 
            //command.YetkiName = "test";

            _yetkiRepository.Setup(x => x.Query())
                                           .Returns(new List<Yetki> { new Yetki() { /*TODO:propertyler buraya yazılacak YetkiId = 1, YetkiName = "test"*/ } }.AsQueryable());

            _yetkiRepository.Setup(x => x.Add(It.IsAny<Yetki>())).Returns(new Yetki());

            var handler = new CreateYetkiCommandHandler(_yetkiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Yetki_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateYetkiCommand();
            //command.YetkiName = "test";

            _yetkiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yetki, bool>>>()))
                        .ReturnsAsync(new Yetki() { /*TODO:propertyler buraya yazılacak YetkiId = 1, YetkiName = "deneme"*/ });

            _yetkiRepository.Setup(x => x.Update(It.IsAny<Yetki>())).Returns(new Yetki());

            var handler = new UpdateYetkiCommandHandler(_yetkiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yetkiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Yetki_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteYetkiCommand();

            _yetkiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Yetki, bool>>>()))
                        .ReturnsAsync(new Yetki() { /*TODO:propertyler buraya yazılacak YetkiId = 1, YetkiName = "deneme"*/});

            _yetkiRepository.Setup(x => x.Delete(It.IsAny<Yetki>()));

            var handler = new DeleteYetkiCommandHandler(_yetkiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _yetkiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

