
using Business.Handlers.KullaniciMenuIslevObjes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.KullaniciMenuIslevObjes.Queries.GetKullaniciMenuIslevObjeQuery;
using Entities.Concrete;
using static Business.Handlers.KullaniciMenuIslevObjes.Queries.GetKullaniciMenuIslevObjesQuery;
using static Business.Handlers.KullaniciMenuIslevObjes.Commands.CreateKullaniciMenuIslevObjeCommand;
using Business.Handlers.KullaniciMenuIslevObjes.Commands;
using Business.Constants;
using static Business.Handlers.KullaniciMenuIslevObjes.Commands.UpdateKullaniciMenuIslevObjeCommand;
using static Business.Handlers.KullaniciMenuIslevObjes.Commands.DeleteKullaniciMenuIslevObjeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class KullaniciMenuIslevObjeHandlerTests
    {
        Mock<IKullaniciMenuIslevObjeRepository> _kullaniciMenuIslevObjeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kullaniciMenuIslevObjeRepository = new Mock<IKullaniciMenuIslevObjeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task KullaniciMenuIslevObje_GetQuery_Success()
        {
            //Arrange
            var query = new GetKullaniciMenuIslevObjeQuery();

            _kullaniciMenuIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciMenuIslevObje, bool>>>())).ReturnsAsync(new KullaniciMenuIslevObje()
//propertyler buraya yazılacak
//{																		
//KullaniciMenuIslevObjeId = 1,
//KullaniciMenuIslevObjeName = "Test"
//}
);

            var handler = new GetKullaniciMenuIslevObjeQueryHandler(_kullaniciMenuIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.KullaniciMenuIslevObjeId.Should().Be(1);

        }

        [Test]
        public async Task KullaniciMenuIslevObje_GetQueries_Success()
        {
            //Arrange
            var query = new GetKullaniciMenuIslevObjesQuery();

            _kullaniciMenuIslevObjeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<KullaniciMenuIslevObje, bool>>>()))
                        .ReturnsAsync(new List<KullaniciMenuIslevObje> { new KullaniciMenuIslevObje() { /*TODO:propertyler buraya yazılacak KullaniciMenuIslevObjeId = 1, KullaniciMenuIslevObjeName = "test"*/ } });

            var handler = new GetKullaniciMenuIslevObjesQueryHandler(_kullaniciMenuIslevObjeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<KullaniciMenuIslevObje>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task KullaniciMenuIslevObje_CreateCommand_Success()
        {
            KullaniciMenuIslevObje rt = null;
            //Arrange
            var command = new CreateKullaniciMenuIslevObjeCommand();
            //propertyler buraya yazılacak
            //command.KullaniciMenuIslevObjeName = "deneme";

            _kullaniciMenuIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciMenuIslevObje, bool>>>()))
                        .ReturnsAsync(rt);

            _kullaniciMenuIslevObjeRepository.Setup(x => x.Add(It.IsAny<KullaniciMenuIslevObje>())).Returns(new KullaniciMenuIslevObje());

            var handler = new CreateKullaniciMenuIslevObjeCommandHandler(_kullaniciMenuIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciMenuIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task KullaniciMenuIslevObje_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKullaniciMenuIslevObjeCommand();
            //propertyler buraya yazılacak 
            //command.KullaniciMenuIslevObjeName = "test";

            _kullaniciMenuIslevObjeRepository.Setup(x => x.Query())
                                           .Returns(new List<KullaniciMenuIslevObje> { new KullaniciMenuIslevObje() { /*TODO:propertyler buraya yazılacak KullaniciMenuIslevObjeId = 1, KullaniciMenuIslevObjeName = "test"*/ } }.AsQueryable());

            _kullaniciMenuIslevObjeRepository.Setup(x => x.Add(It.IsAny<KullaniciMenuIslevObje>())).Returns(new KullaniciMenuIslevObje());

            var handler = new CreateKullaniciMenuIslevObjeCommandHandler(_kullaniciMenuIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task KullaniciMenuIslevObje_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKullaniciMenuIslevObjeCommand();
            //command.KullaniciMenuIslevObjeName = "test";

            _kullaniciMenuIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciMenuIslevObje, bool>>>()))
                        .ReturnsAsync(new KullaniciMenuIslevObje() { /*TODO:propertyler buraya yazılacak KullaniciMenuIslevObjeId = 1, KullaniciMenuIslevObjeName = "deneme"*/ });

            _kullaniciMenuIslevObjeRepository.Setup(x => x.Update(It.IsAny<KullaniciMenuIslevObje>())).Returns(new KullaniciMenuIslevObje());

            var handler = new UpdateKullaniciMenuIslevObjeCommandHandler(_kullaniciMenuIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciMenuIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task KullaniciMenuIslevObje_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKullaniciMenuIslevObjeCommand();

            _kullaniciMenuIslevObjeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciMenuIslevObje, bool>>>()))
                        .ReturnsAsync(new KullaniciMenuIslevObje() { /*TODO:propertyler buraya yazılacak KullaniciMenuIslevObjeId = 1, KullaniciMenuIslevObjeName = "deneme"*/});

            _kullaniciMenuIslevObjeRepository.Setup(x => x.Delete(It.IsAny<KullaniciMenuIslevObje>()));

            var handler = new DeleteKullaniciMenuIslevObjeCommandHandler(_kullaniciMenuIslevObjeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciMenuIslevObjeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

