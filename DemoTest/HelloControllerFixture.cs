using System;
using Xunit;
using DemoWebApp.Controllers;
using FluentAssertions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DemoTest
{
    public class HelloControllerFixture
    {
        [Fact]
        public void Visiting_hello_route_returns_hi()
        {
            var controller = new HelloController();

            controller.Get().Should().BeEquivalentTo((ActionResult < IEnumerable<string> > )new string[] { "Hi" });
        }

        [Fact]
        public void Visiting_hello_slash_name_route_returns_hi_name()
        {
            var controller = new HelloController();

            controller.Get("RV").Should().BeEquivalentTo((ActionResult<IEnumerable<string>>)new string[] { "Hi RV" });
        }
    }
}
