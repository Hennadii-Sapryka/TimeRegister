using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using TimeRegisterApp.Models;

namespace TimeRegisterApp.UnitTests
{
    public class TestBase
    {
        protected Fixture Fixture { get; set; }

        protected IMapper Mapper { get; set; }

        public TestBase()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }
    }
}
