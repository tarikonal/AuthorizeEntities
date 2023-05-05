
using Business.Handlers.BirimAgacKullaniciRols.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.BirimAgacKullaniciRols.Queries.GetBirimAgacKullaniciRolQuery;
using Entities.Concrete;
using static Business.Handlers.BirimAgacKullaniciRols.Queries.GetBirimAgacKullaniciRolsQuery;
using static Business.Handlers.BirimAgacKullaniciRols.Commands.CreateBirimAgacKullaniciRolCommand;
using Business.Handlers.BirimAgacKullaniciRols.Commands;
using Business.Constants;
using static Business.Handlers.BirimAgacKullaniciRols.Commands.UpdateBirimAgacKullaniciRolCommand;
using static Business.Handlers.BirimAgacKullaniciRols.Commands.DeleteBirimAgacKullaniciRolCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BirimAgacKullaniciRolHandlerTests
    {
        Mock<IBirimAgacKullaniciRolRepository> _birimAgacKullaniciRolRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _birimAgacKullaniciRolRepository = new Mock<IBirimAgacKullaniciRolRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task BirimAgacKullaniciRol_GetQuery_Success()
        {
            //Arrange
            var query = new GetBirimAgacKullaniciRolQuery();

            _birimAgacKullaniciRolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimAgacKullaniciRol, bool>>>())).ReturnsAsync(new BirimAgacKullaniciRol()
//propertyler buraya yazılacak
//{																		
//BirimAgacKullaniciRolId = 1,
//BirimAgacKullaniciRolName = "Test"
//}
);

            var handler = new GetBirimAgacKullaniciRolQueryHandler(_birimAgacKullaniciRolRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BirimAgacKullaniciRolId.Should().Be(1);

        }

        [Test]
        public async Task BirimAgacKullaniciRol_GetQueries_Success()
        {
            //Arrange
            var query = new GetBirimAgacKullaniciRolsQuery();

            _birimAgacKullaniciRolRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<BirimAgacKullaniciRol, bool>>>()))
                        .ReturnsAsync(new List<BirimAgacKullaniciRol> { new BirimAgacKullaniciRol() { /*TODO:propertyler buraya yazılacak BirimAgacKullaniciRolId = 1, BirimAgacKullaniciRolName = "test"*/ } });

            var handler = new GetBirimAgacKullaniciRolsQueryHandler(_birimAgacKullaniciRolRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<BirimAgacKullaniciRol>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task BirimAgacKullaniciRol_CreateCommand_Success()
        {
            BirimAgacKullaniciRol rt = null;
            //Arrange
            var command = new CreateBirimAgacKullaniciRolCommand();
            //propertyler buraya yazılacak
            //command.BirimAgacKullaniciRolName = "deneme";

            _birimAgacKullaniciRolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimAgacKullaniciRol, bool>>>()))
                        .ReturnsAsync(rt);

            _birimAgacKullaniciRolRepository.Setup(x => x.Add(It.IsAny<BirimAgacKullaniciRol>())).Returns(new BirimAgacKullaniciRol());

            var handler = new CreateBirimAgacKullaniciRolCommandHandler(_birimAgacKullaniciRolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimAgacKullaniciRolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task BirimAgacKullaniciRol_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBirimAgacKullaniciRolCommand();
            //propertyler buraya yazılacak 
            //command.BirimAgacKullaniciRolName = "test";

            _birimAgacKullaniciRolRepository.Setup(x => x.Query())
                                           .Returns(new List<BirimAgacKullaniciRol> { new BirimAgacKullaniciRol() { /*TODO:propertyler buraya yazılacak BirimAgacKullaniciRolId = 1, BirimAgacKullaniciRolName = "test"*/ } }.AsQueryable());

            _birimAgacKullaniciRolRepository.Setup(x => x.Add(It.IsAny<BirimAgacKullaniciRol>())).Returns(new BirimAgacKullaniciRol());

            var handler = new CreateBirimAgacKullaniciRolCommandHandler(_birimAgacKullaniciRolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task BirimAgacKullaniciRol_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBirimAgacKullaniciRolCommand();
            //command.BirimAgacKullaniciRolName = "test";

            _birimAgacKullaniciRolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimAgacKullaniciRol, bool>>>()))
                        .ReturnsAsync(new BirimAgacKullaniciRol() { /*TODO:propertyler buraya yazılacak BirimAgacKullaniciRolId = 1, BirimAgacKullaniciRolName = "deneme"*/ });

            _birimAgacKullaniciRolRepository.Setup(x => x.Update(It.IsAny<BirimAgacKullaniciRol>())).Returns(new BirimAgacKullaniciRol());

            var handler = new UpdateBirimAgacKullaniciRolCommandHandler(_birimAgacKullaniciRolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimAgacKullaniciRolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task BirimAgacKullaniciRol_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBirimAgacKullaniciRolCommand();

            _birimAgacKullaniciRolRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BirimAgacKullaniciRol, bool>>>()))
                        .ReturnsAsync(new BirimAgacKullaniciRol() { /*TODO:propertyler buraya yazılacak BirimAgacKullaniciRolId = 1, BirimAgacKullaniciRolName = "deneme"*/});

            _birimAgacKullaniciRolRepository.Setup(x => x.Delete(It.IsAny<BirimAgacKullaniciRol>()));

            var handler = new DeleteBirimAgacKullaniciRolCommandHandler(_birimAgacKullaniciRolRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _birimAgacKullaniciRolRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

