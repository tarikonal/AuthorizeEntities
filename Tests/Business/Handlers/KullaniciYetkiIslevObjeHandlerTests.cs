
using Business.Handlers.KullaniciYetkiIslevObjes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.KullaniciYetkiIslevObjes.Queries.GetKullaniciYetkiIslevObjeQuery;
using Entities.Concrete;
using static Business.Handlers.KullaniciYetkiIslevObjes.Queries.GetKullaniciYetkiIslevObjesQuery;
using static Business.Handlers.KullaniciYetkiIslevObjes.Commands.CreateKullaniciYetkiIslevObjeCommand;
using Business.Handlers.KullaniciYetkiIslevObjes.Commands;
using Business.Constants;
using static Business.Handlers.KullaniciYetkiIslevObjes.Commands.UpdateKullaniciYetkiIslevObjeCommand;
using static Business.Handlers.KullaniciYetkiIslevObjes.Commands.DeleteKullaniciYetkiIslevObjeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class KullaniciYetkiIslevObjeHandlerTests
    {
        Mock<IKullaniciYetkiIslevObjeRepository> _kullaniciYetkiIslevObjeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kullaniciYetkiIslevObjeRepository = new Mock<IKullaniciYetkiIslevObjeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task KullaniciYetkiIslevObje_GetQuery_Success()
        {
            //Arrange
            var query = new GetKullaniciYetkiIslevObjeQuery();

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevObje, bool>>>())).ReturnsAsync(new KullaniciYetkiIslevObje()
//propertyler buraya yazılacak
//{																		
//KullaniciYetkiIslevObjeId = 1,
//KullaniciYetkiIslevObjeName = "Test"
//}
);

            var handler = new GetKullaniciYetkiIslevObjeQueryHandler(_kullaniciYetkiIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.KullaniciYetkiIslevObjeId.Should().Be(1);

        }

        [Test]
        public async Task KullaniciYetkiIslevObje_GetQueries_Success()
        {
            //Arrange
            var query = new GetKullaniciYetkiIslevObjesQuery();

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new List<KullaniciYetkiIslevObje> { new KullaniciYetkiIslevObje() { /*TODO:propertyler buraya yazılacak KullaniciYetkiIslevObjeId = 1, KullaniciYetkiIslevObjeName = "test"*/ } });

            var handler = new GetKullaniciYetkiIslevObjesQueryHandler(_kullaniciYetkiIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<KullaniciYetkiIslevObje>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task KullaniciYetkiIslevObje_CreateCommand_Success()
        {
            KullaniciYetkiIslevObje rt = null;
            //Arrange
            var command = new CreateKullaniciYetkiIslevObjeCommand();
            //propertyler buraya yazılacak
            //command.KullaniciYetkiIslevObjeName = "deneme";

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(rt);

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.Add(It.IsAny<KullaniciYetkiIslevObje>())).Returns(new KullaniciYetkiIslevObje());

            var handler = new CreateKullaniciYetkiIslevObjeCommandHandler(_kullaniciYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task KullaniciYetkiIslevObje_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKullaniciYetkiIslevObjeCommand();
            //propertyler buraya yazılacak 
            //command.KullaniciYetkiIslevObjeName = "test";

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.Query())
                                           .Returns(new List<KullaniciYetkiIslevObje> { new KullaniciYetkiIslevObje() { /*TODO:propertyler buraya yazılacak KullaniciYetkiIslevObjeId = 1, KullaniciYetkiIslevObjeName = "test"*/ } }.AsQueryable());

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.Add(It.IsAny<KullaniciYetkiIslevObje>())).Returns(new KullaniciYetkiIslevObje());

            var handler = new CreateKullaniciYetkiIslevObjeCommandHandler(_kullaniciYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task KullaniciYetkiIslevObje_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKullaniciYetkiIslevObjeCommand();
            //command.KullaniciYetkiIslevObjeName = "test";

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new KullaniciYetkiIslevObje() { /*TODO:propertyler buraya yazılacak KullaniciYetkiIslevObjeId = 1, KullaniciYetkiIslevObjeName = "deneme"*/ });

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.Update(It.IsAny<KullaniciYetkiIslevObje>())).Returns(new KullaniciYetkiIslevObje());

            var handler = new UpdateKullaniciYetkiIslevObjeCommandHandler(_kullaniciYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task KullaniciYetkiIslevObje_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKullaniciYetkiIslevObjeCommand();

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciYetkiIslevObje, bool>>>()))
                        .ReturnsAsync(new KullaniciYetkiIslevObje() { /*TODO:propertyler buraya yazılacak KullaniciYetkiIslevObjeId = 1, KullaniciYetkiIslevObjeName = "deneme"*/});

            _kullaniciYetkiIslevObjeRepository.Setup(x => x.Delete(It.IsAny<KullaniciYetkiIslevObje>()));

            var handler = new DeleteKullaniciYetkiIslevObjeCommandHandler(_kullaniciYetkiIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciYetkiIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

