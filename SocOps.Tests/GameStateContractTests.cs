using System;
using System.Linq;
using SocOps.Models;
using Xunit;

namespace SocOps.Tests;

public class GameStateContractTests
{
    [Fact]
    public void GameState_Should_Expose_Scavenger_States()
    {
        var names = Enum.GetNames(typeof(GameState));

        Assert.Contains("ScavengerPlaying", names);
        Assert.Contains("ScavengerBingo", names);
    }
}
