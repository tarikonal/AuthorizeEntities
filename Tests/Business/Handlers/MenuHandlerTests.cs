
using Business.Handlers.Menus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Menus.Queries.GetMenuQuery;
using Entities.Concrete;
using static Business.Handlers.Menus.Queries.GetMenusQuery;
using static Business.Handlers.Menus.Commands.CreateMenuCommand;
using Business.Handlers.Menus.Commands;
using Business.Constants;
using static Business.Handlers.Menus.Commands.UpdateMenuCommand;
using static Business.Handlers.Menus.Commands.DeleteMenuCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MenuHandlerTests
    {
        Mock<IMenuRepository> _menuRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _menuRepository = new Mock<IMenuRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Menu_GetQuery_Success()
        {
            //Arrange
            var query = new GetMenuQuery();

            _menuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Menu, bool>>>())).ReturnsAsync(new Menu()
//propertyler buraya yazılacak
//{																		
//MenuId = 1,
//MenuName = "Test"
//}
);

            var handler = new GetMenuQueryHandler(_menuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.MenuId.Should().Be(1);

        }

        [Test]
        public async Task Menu_GetQueries_Success()
        {
            //Arrange
            var query = new GetMenusQuery();

            _menuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Menu, bool>>>()))
                        .ReturnsAsync(new List<Menu> { new Menu() { /*TODO:propertyler buraya yazılacak MenuId = 1, MenuName = "test"*/ } });

            var handler = new GetMenusQueryHandler(_menuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Menu>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Menu_CreateCommand_Success()
        {
            Menu rt = null;
            //Arrange
            var command = new CreateMenuCommand();
            //propertyler buraya yazılacak
            //command.MenuName = "deneme";

            _menuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Menu, bool>>>()))
                        .ReturnsAsync(rt);

            _menuRepository.Setup(x => x.Add(It.IsAny<Menu>())).Returns(new Menu());

            var handler = new CreateMenuCommandHandler(_menuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _menuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Menu_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateMenuCommand();
            //propertyler buraya yazılacak 
            //command.MenuName = "test";

            _menuRepository.Setup(x => x.Query())
                                           .Returns(new List<Menu> { new Menu() { /*TODO:propertyler buraya yazılacak MenuId = 1, MenuName = "test"*/ } }.AsQueryable());

            _menuRepository.Setup(x => x.Add(It.IsAny<Menu>())).Returns(new Menu());

            var handler = new CreateMenuCommandHandler(_menuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Menu_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateMenuCommand();
            //command.MenuName = "test";

            _menuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Menu, bool>>>()))
                        .ReturnsAsync(new Menu() { /*TODO:propertyler buraya yazılacak MenuId = 1, MenuName = "deneme"*/ });

            _menuRepository.Setup(x => x.Update(It.IsAny<Menu>())).Returns(new Menu());

            var handler = new UpdateMenuCommandHandler(_menuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _menuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Menu_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteMenuCommand();

            _menuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Menu, bool>>>()))
                        .ReturnsAsync(new Menu() { /*TODO:propertyler buraya yazılacak MenuId = 1, MenuName = "deneme"*/});

            _menuRepository.Setup(x => x.Delete(It.IsAny<Menu>()));

            var handler = new DeleteMenuCommandHandler(_menuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _menuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

