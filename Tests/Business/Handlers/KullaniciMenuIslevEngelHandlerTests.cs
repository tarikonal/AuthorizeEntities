
using Business.Handlers.KullaniciMenuIslevEngels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.KullaniciMenuIslevEngels.Queries.GetKullaniciMenuIslevEngelQuery;
using Entities.Concrete;
using static Business.Handlers.KullaniciMenuIslevEngels.Queries.GetKullaniciMenuIslevEngelsQuery;
using static Business.Handlers.KullaniciMenuIslevEngels.Commands.CreateKullaniciMenuIslevEngelCommand;
using Business.Handlers.KullaniciMenuIslevEngels.Commands;
using Business.Constants;
using static Business.Handlers.KullaniciMenuIslevEngels.Commands.UpdateKullaniciMenuIslevEngelCommand;
using static Business.Handlers.KullaniciMenuIslevEngels.Commands.DeleteKullaniciMenuIslevEngelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class KullaniciMenuIslevEngelHandlerTests
    {
        Mock<IKullaniciMenuIslevEngelRepository> _kullaniciMenuIslevEngelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kullaniciMenuIslevEngelRepository = new Mock<IKullaniciMenuIslevEngelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task KullaniciMenuIslevEngel_GetQuery_Success()
        {
            //Arrange
            var query = new GetKullaniciMenuIslevEngelQuery();

            _kullaniciMenuIslevEngelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciMenuIslevEngel, bool>>>())).ReturnsAsync(new KullaniciMenuIslevEngel()
//propertyler buraya yazılacak
//{																		
//KullaniciMenuIslevEngelId = 1,
//KullaniciMenuIslevEngelName = "Test"
//}
);

            var handler = new GetKullaniciMenuIslevEngelQueryHandler(_kullaniciMenuIslevEngelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.KullaniciMenuIslevEngelId.Should().Be(1);

        }

        [Test]
        public async Task KullaniciMenuIslevEngel_GetQueries_Success()
        {
            //Arrange
            var query = new GetKullaniciMenuIslevEngelsQuery();

            _kullaniciMenuIslevEngelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<KullaniciMenuIslevEngel, bool>>>()))
                        .ReturnsAsync(new List<KullaniciMenuIslevEngel> { new KullaniciMenuIslevEngel() { /*TODO:propertyler buraya yazılacak KullaniciMenuIslevEngelId = 1, KullaniciMenuIslevEngelName = "test"*/ } });

            var handler = new GetKullaniciMenuIslevEngelsQueryHandler(_kullaniciMenuIslevEngelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<KullaniciMenuIslevEngel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task KullaniciMenuIslevEngel_CreateCommand_Success()
        {
            KullaniciMenuIslevEngel rt = null;
            //Arrange
            var command = new CreateKullaniciMenuIslevEngelCommand();
            //propertyler buraya yazılacak
            //command.KullaniciMenuIslevEngelName = "deneme";

            _kullaniciMenuIslevEngelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciMenuIslevEngel, bool>>>()))
                        .ReturnsAsync(rt);

            _kullaniciMenuIslevEngelRepository.Setup(x => x.Add(It.IsAny<KullaniciMenuIslevEngel>())).Returns(new KullaniciMenuIslevEngel());

            var handler = new CreateKullaniciMenuIslevEngelCommandHandler(_kullaniciMenuIslevEngelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciMenuIslevEngelRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task KullaniciMenuIslevEngel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKullaniciMenuIslevEngelCommand();
            //propertyler buraya yazılacak 
            //command.KullaniciMenuIslevEngelName = "test";

            _kullaniciMenuIslevEngelRepository.Setup(x => x.Query())
                                           .Returns(new List<KullaniciMenuIslevEngel> { new KullaniciMenuIslevEngel() { /*TODO:propertyler buraya yazılacak KullaniciMenuIslevEngelId = 1, KullaniciMenuIslevEngelName = "test"*/ } }.AsQueryable());

            _kullaniciMenuIslevEngelRepository.Setup(x => x.Add(It.IsAny<KullaniciMenuIslevEngel>())).Returns(new KullaniciMenuIslevEngel());

            var handler = new CreateKullaniciMenuIslevEngelCommandHandler(_kullaniciMenuIslevEngelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task KullaniciMenuIslevEngel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKullaniciMenuIslevEngelCommand();
            //command.KullaniciMenuIslevEngelName = "test";

            _kullaniciMenuIslevEngelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciMenuIslevEngel, bool>>>()))
                        .ReturnsAsync(new KullaniciMenuIslevEngel() { /*TODO:propertyler buraya yazılacak KullaniciMenuIslevEngelId = 1, KullaniciMenuIslevEngelName = "deneme"*/ });

            _kullaniciMenuIslevEngelRepository.Setup(x => x.Update(It.IsAny<KullaniciMenuIslevEngel>())).Returns(new KullaniciMenuIslevEngel());

            var handler = new UpdateKullaniciMenuIslevEngelCommandHandler(_kullaniciMenuIslevEngelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciMenuIslevEngelRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task KullaniciMenuIslevEngel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKullaniciMenuIslevEngelCommand();

            _kullaniciMenuIslevEngelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciMenuIslevEngel, bool>>>()))
                        .ReturnsAsync(new KullaniciMenuIslevEngel() { /*TODO:propertyler buraya yazılacak KullaniciMenuIslevEngelId = 1, KullaniciMenuIslevEngelName = "deneme"*/});

            _kullaniciMenuIslevEngelRepository.Setup(x => x.Delete(It.IsAny<KullaniciMenuIslevEngel>()));

            var handler = new DeleteKullaniciMenuIslevEngelCommandHandler(_kullaniciMenuIslevEngelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciMenuIslevEngelRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

