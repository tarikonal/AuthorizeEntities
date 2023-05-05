
using Business.Handlers.KullaniciRols.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.KullaniciRols.Queries.GetKullaniciRolQuery;
using Entities.Concrete;
using static Business.Handlers.KullaniciRols.Queries.GetKullaniciRolsQuery;
using static Business.Handlers.KullaniciRols.Commands.CreateKullaniciRolCommand;
using Business.Handlers.KullaniciRols.Commands;
using Business.Constants;
using static Business.Handlers.KullaniciRols.Commands.UpdateKullaniciRolCommand;
using static Business.Handlers.KullaniciRols.Commands.DeleteKullaniciRolCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class KullaniciRolHandlerTests
    {
        Mock<IKullaniciRolRepository> _kullaniciRolRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _kullaniciRolRepository = new Mock<IKullaniciRolRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task KullaniciRol_GetQuery_Success()
        {
            //Arrange
            var query = new GetKullaniciRolQuery();

            _kullaniciRolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciRol, bool>>>())).ReturnsAsync(new KullaniciRol()
//propertyler buraya yazılacak
//{																		
//KullaniciRolId = 1,
//KullaniciRolName = "Test"
//}
);

            var handler = new GetKullaniciRolQueryHandler(_kullaniciRolRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.KullaniciRolId.Should().Be(1);

        }

        [Test]
        public async Task KullaniciRol_GetQueries_Success()
        {
            //Arrange
            var query = new GetKullaniciRolsQuery();

            _kullaniciRolRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<KullaniciRol, bool>>>()))
                        .ReturnsAsync(new List<KullaniciRol> { new KullaniciRol() { /*TODO:propertyler buraya yazılacak KullaniciRolId = 1, KullaniciRolName = "test"*/ } });

            var handler = new GetKullaniciRolsQueryHandler(_kullaniciRolRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<KullaniciRol>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task KullaniciRol_CreateCommand_Success()
        {
            KullaniciRol rt = null;
            //Arrange
            var command = new CreateKullaniciRolCommand();
            //propertyler buraya yazılacak
            //command.KullaniciRolName = "deneme";

            _kullaniciRolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciRol, bool>>>()))
                        .ReturnsAsync(rt);

            _kullaniciRolRepository.Setup(x => x.Add(It.IsAny<KullaniciRol>())).Returns(new KullaniciRol());

            var handler = new CreateKullaniciRolCommandHandler(_kullaniciRolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciRolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task KullaniciRol_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateKullaniciRolCommand();
            //propertyler buraya yazılacak 
            //command.KullaniciRolName = "test";

            _kullaniciRolRepository.Setup(x => x.Query())
                                           .Returns(new List<KullaniciRol> { new KullaniciRol() { /*TODO:propertyler buraya yazılacak KullaniciRolId = 1, KullaniciRolName = "test"*/ } }.AsQueryable());

            _kullaniciRolRepository.Setup(x => x.Add(It.IsAny<KullaniciRol>())).Returns(new KullaniciRol());

            var handler = new CreateKullaniciRolCommandHandler(_kullaniciRolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task KullaniciRol_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateKullaniciRolCommand();
            //command.KullaniciRolName = "test";

            _kullaniciRolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciRol, bool>>>()))
                        .ReturnsAsync(new KullaniciRol() { /*TODO:propertyler buraya yazılacak KullaniciRolId = 1, KullaniciRolName = "deneme"*/ });

            _kullaniciRolRepository.Setup(x => x.Update(It.IsAny<KullaniciRol>())).Returns(new KullaniciRol());

            var handler = new UpdateKullaniciRolCommandHandler(_kullaniciRolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciRolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task KullaniciRol_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteKullaniciRolCommand();

            _kullaniciRolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<KullaniciRol, bool>>>()))
                        .ReturnsAsync(new KullaniciRol() { /*TODO:propertyler buraya yazılacak KullaniciRolId = 1, KullaniciRolName = "deneme"*/});

            _kullaniciRolRepository.Setup(x => x.Delete(It.IsAny<KullaniciRol>()));

            var handler = new DeleteKullaniciRolCommandHandler(_kullaniciRolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _kullaniciRolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

