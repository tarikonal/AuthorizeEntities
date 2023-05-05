
using Business.Handlers.KullaniciYetkiIslevEngels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.KullaniciYetkiIslevEngels.Queries.GetKullaniciYetkiIslevEngelQuery;
using Entities.Concrete;
using static Business.Handlers.KullaniciYetkiIslevEngels.Queries.GetKullaniciYetkiIslevEngelsQuery;
using static Business.Handlers.KullaniciYetkiIslevEngels.Commands.CreateKullaniciYetkiIslevEngelCommand;
using Business.Handlers.KullaniciYetkiIslevEngels.Commands;
using Business.Constants;
using static Business.Handlers.KullaniciYetkiIslevEngels.Commands.UpdateKullaniciYetkiIslevEngelCommand;
using static Business.Handlers.KullaniciYetkiIslevEngels.Commands.DeleteKullaniciYetkiIslevEngelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class KullaniciYetkiIslevEngelHandlerTests
    {
        Mock<IKullaniciYetkiIslevEngelRepository> _kullaniciYetkiIslevEngelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kullaniciYetkiIslevEngelRepository = new Mock<IKullaniciYetkiIslevEngelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task KullaniciYetkiIslevEngel_GetQuery_Success()
        {
            //Arrange
            var query = new GetKullaniciYetkiIslevEngelQuery();

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevEngel, bool>>>())).ReturnsAsync(new KullaniciYetkiIslevEngel()
//propertyler buraya yazılacak
//{																		
//KullaniciYetkiIslevEngelId = 1,
//KullaniciYetkiIslevEngelName = "Test"
//}
);

            var handler = new GetKullaniciYetkiIslevEngelQueryHandler(_kullaniciYetkiIslevEngelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.KullaniciYetkiIslevEngelId.Should().Be(1);

        }

        [Test]
        public async Task KullaniciYetkiIslevEngel_GetQueries_Success()
        {
            //Arrange
            var query = new GetKullaniciYetkiIslevEngelsQuery();

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevEngel, bool>>>()))
                        .ReturnsAsync(new List<KullaniciYetkiIslevEngel> { new KullaniciYetkiIslevEngel() { /*TODO:propertyler buraya yazılacak KullaniciYetkiIslevEngelId = 1, KullaniciYetkiIslevEngelName = "test"*/ } });

            var handler = new GetKullaniciYetkiIslevEngelsQueryHandler(_kullaniciYetkiIslevEngelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<KullaniciYetkiIslevEngel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task KullaniciYetkiIslevEngel_CreateCommand_Success()
        {
            KullaniciYetkiIslevEngel rt = null;
            //Arrange
            var command = new CreateKullaniciYetkiIslevEngelCommand();
            //propertyler buraya yazılacak
            //command.KullaniciYetkiIslevEngelName = "deneme";

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevEngel, bool>>>()))
                        .ReturnsAsync(rt);

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.Add(It.IsAny<KullaniciYetkiIslevEngel>())).Returns(new KullaniciYetkiIslevEngel());

            var handler = new CreateKullaniciYetkiIslevEngelCommandHandler(_kullaniciYetkiIslevEngelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciYetkiIslevEngelRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task KullaniciYetkiIslevEngel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKullaniciYetkiIslevEngelCommand();
            //propertyler buraya yazılacak 
            //command.KullaniciYetkiIslevEngelName = "test";

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.Query())
                                           .Returns(new List<KullaniciYetkiIslevEngel> { new KullaniciYetkiIslevEngel() { /*TODO:propertyler buraya yazılacak KullaniciYetkiIslevEngelId = 1, KullaniciYetkiIslevEngelName = "test"*/ } }.AsQueryable());

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.Add(It.IsAny<KullaniciYetkiIslevEngel>())).Returns(new KullaniciYetkiIslevEngel());

            var handler = new CreateKullaniciYetkiIslevEngelCommandHandler(_kullaniciYetkiIslevEngelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task KullaniciYetkiIslevEngel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKullaniciYetkiIslevEngelCommand();
            //command.KullaniciYetkiIslevEngelName = "test";

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevEngel, bool>>>()))
                        .ReturnsAsync(new KullaniciYetkiIslevEngel() { /*TODO:propertyler buraya yazılacak KullaniciYetkiIslevEngelId = 1, KullaniciYetkiIslevEngelName = "deneme"*/ });

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.Update(It.IsAny<KullaniciYetkiIslevEngel>())).Returns(new KullaniciYetkiIslevEngel());

            var handler = new UpdateKullaniciYetkiIslevEngelCommandHandler(_kullaniciYetkiIslevEngelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciYetkiIslevEngelRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task KullaniciYetkiIslevEngel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKullaniciYetkiIslevEngelCommand();

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevEngel, bool>>>()))
                        .ReturnsAsync(new KullaniciYetkiIslevEngel() { /*TODO:propertyler buraya yazılacak KullaniciYetkiIslevEngelId = 1, KullaniciYetkiIslevEngelName = "deneme"*/});

            _kullaniciYetkiIslevEngelRepository.Setup(x => x.Delete(It.IsAny<KullaniciYetkiIslevEngel>()));

            var handler = new DeleteKullaniciYetkiIslevEngelCommandHandler(_kullaniciYetkiIslevEngelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciYetkiIslevEngelRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

