using System;
using System.Linq;
using SocOps.Models;
using SocOps.Services;
using Xunit;

namespace SocOps.Tests;

public class BingoGameServiceContractTests
{
    [Fact]
    public void Service_Should_Expose_Mode_Api_For_Scavenger_Hunt()
    {
        var type = typeof(BingoGameService);

        var modeProperty = type.GetProperty("CurrentMode");
        Assert.NotNull(modeProperty);
        Assert.Equal(typeof(PlayMode), modeProperty!.PropertyType);

        var startScavenger = type.GetMethod("StartScavengerGame");
        Assert.NotNull(startScavenger);
        Assert.Equal(typeof(void), startScavenger!.ReturnType);
    }

    [Fact]
    public void Service_Should_Expose_Progress_Metrics_For_List_Mode()
    {
        var type = typeof(BingoGameService);

        var markedCount = type.GetProperty("MarkedCount");
        var totalCount = type.GetProperty("TotalCount");
        var progressPercent = type.GetProperty("ProgressPercent");

        Assert.NotNull(markedCount);
        Assert.NotNull(totalCount);
        Assert.NotNull(progressPercent);

        Assert.Equal(typeof(int), markedCount!.PropertyType);
        Assert.Equal(typeof(int), totalCount!.PropertyType);
        Assert.Equal(typeof(int), progressPercent!.PropertyType);
    }
}
