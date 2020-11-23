using MediatR;
using Moq;

namespace Test.UnitTests
{
    public class BaseControllerTest
    {
        private Mock<IMediator> _mediator;

        protected Mock<IMediator> Mediator => _mediator ?? 
            (_mediator = new Mock<IMediator>());
    }
}