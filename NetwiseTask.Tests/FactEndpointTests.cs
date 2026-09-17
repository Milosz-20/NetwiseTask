using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using NetwiseTask.Endpoints;
using NetwiseTask.Models;
using NetwiseTask.Services;

namespace NetwiseTask.Tests;

public class FactEndpointTests
{
    [Fact]
    public async Task Handle_ReturnsOkWithFact_WhenServiceSucceeds()
    {
        var fact = new CatFact("Test fact", 9);
        var catFactServiceMock = new Mock<ICatFactService>();
        catFactServiceMock.Setup(s => s.GetRandomFactAsync()).ReturnsAsync(fact);
        var logWriterMock = new Mock<IFactLogWriter>();

        var result = await FactEndpoint.Handle(catFactServiceMock.Object, logWriterMock.Object);

        var okResult = Assert.IsType<Ok<CatFact>>(result);
        Assert.Equal(fact, okResult.Value);
        logWriterMock.Verify(w => w.AppendAsync(fact), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsBadGateway_WhenCatFactServiceThrows()
    {
        var catFactServiceMock = new Mock<ICatFactService>();
        catFactServiceMock.Setup(s => s.GetRandomFactAsync()).ThrowsAsync(new HttpRequestException());
        var logWriterMock = new Mock<IFactLogWriter>();

        var result = await FactEndpoint.Handle(catFactServiceMock.Object, logWriterMock.Object);

        var problemResult = Assert.IsType<ProblemHttpResult>(result);
        Assert.Equal(StatusCodes.Status502BadGateway, problemResult.StatusCode);
        logWriterMock.Verify(w => w.AppendAsync(It.IsAny<CatFact>()), Times.Never);
    }
}
