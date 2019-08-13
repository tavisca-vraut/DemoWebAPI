using System;
using Xunit;
using DemoWebApp.Controllers;
using FluentAssertions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DemoTest
{
    public class HiControllerFixture
    {
        [Fact]
        public void Visiting_hi_route_returns_hello()
        {
            var controller = new HiController();

            controller.Get().Should().BeEquivalentTo((ActionResult<IEnumerable<string>>)new string[] { "Hello" });
        }

        [Fact]
        public void Visiting_hi_slash_name_route_returns_hello_name()
        {
            var controller = new HiController();

            controller.Get("RV").Should().BeEquivalentTo((ActionResult<IEnumerable<string>>)new string[] { "Hello RV" });
        }
    }
}
