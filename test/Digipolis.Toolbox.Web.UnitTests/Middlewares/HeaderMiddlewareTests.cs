﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Digipolis.Toolbox.Web.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace Digipolis.Toolbox.Web.UnitTests.Middlewares
{
    public class HeaderMiddlewareTests
    {
        [Fact]
        void NextDelegateNullRaisesArgumentNullException()
        {
            var logger = Mock.Of<ILogger<HeadersMiddleware>>();
            var ex = Assert.Throws<ArgumentNullException>(() => new HeadersMiddleware(null, logger));
            Assert.Equal("next", ex.ParamName);
        }

        [Fact]
        void LoggerNullRaisesArgumentNullException()
        {
            var nextdelegate = new RequestDelegate(CreateDefaultHttpContex);
            var ex = Assert.Throws<ArgumentNullException>(() => new HeadersMiddleware(nextdelegate, null));
            Assert.Equal("logger", ex.ParamName);
        }

        [Fact]
        void HandlerIsCalledForCorrespondingHeaderKey()
        {
            var handler = new Mock<IHeaderHandler>();
            var headers = new List<KeyValuePair<string, IHeaderHandler>>() { new KeyValuePair<string, IHeaderHandler>("aKey", handler.Object) };

            var headerValues = new StringValues();

            handler.Setup(x => x.Handle(headerValues)).Verifiable();

            var nextdelegate = new RequestDelegate(CreateDefaultHttpContex);
            var logger = Mock.Of<ILogger<HeadersMiddleware>>();
            var middleware = new HeadersMiddleware(nextdelegate, logger);

            //var 

        }

        private Task CreateDefaultHttpContex(HttpContext httpContext)
        {
            return Task.Run(() => { return new DefaultHttpContext(); });
        }
    }
}
