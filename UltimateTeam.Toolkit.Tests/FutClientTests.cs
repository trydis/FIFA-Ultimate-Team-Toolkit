using System;
using Moq;
using NUnit.Framework;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Tests
{
    //[TestFixture]
    //public class FutClientTests
    //{
    //    private const string IgnoredParameter = "foo";

    //    private Mock<IFutRequestFactory> _requestFactoryMock;

    //    private IFutClient _futClient;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        _requestFactoryMock = new Mock<IFutRequestFactory>();
    //        _futClient = new FutClient(_requestFactoryMock.Object);
    //    }

    //    [Test,
    //    ExpectedException(typeof(ArgumentException))]
    //    public async void Foo()
    //    {
    //        await _futClient.LoginAsync(new LoginDetails(null, IgnoredParameter, IgnoredParameter));
    //    }

    //    [Test,
    //    ExpectedException(typeof(ArgumentException))]
    //    public async void Foo2()
    //    {
    //        await _futClient.LoginAsync(new LoginDetails(IgnoredParameter, null, IgnoredParameter));
    //    }

    //    [Test,
    //    ExpectedException(typeof(FutException))]
    //    public async void Foo3()
    //    {
    //        _requestFactoryMock
    //            .Setup(factory => factory.CreateLoginRequest(It.IsAny<LoginDetails>()))
    //            .Throws<Exception>();

    //        await _futClient.LoginAsync(new LoginDetails(IgnoredParameter, IgnoredParameter, IgnoredParameter));
    //    }
    //}
}
