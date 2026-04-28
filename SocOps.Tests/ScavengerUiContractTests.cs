using System.IO;
using Xunit;

namespace SocOps.Tests;

public class ScavengerUiContractTests
{
    [Fact]
    public void StartScreen_Should_Offer_Bingo_And_Scavenger_Mode_Buttons()
    {
        var path = PathHelper.InRepo("SocOps", "Components", "StartScreen.razor");
        var source = File.ReadAllText(path);

        Assert.Contains("OnStartBingo", source);
        Assert.Contains("OnStartScavenger", source);
        Assert.Contains("Start Scavenger Hunt", source);
    }

    [Fact]
    public void Home_Should_Render_ScavengerScreen_When_Scavenger_Mode_Is_Active()
    {
        var path = PathHelper.InRepo("SocOps", "Pages", "Home.razor");
        var source = File.ReadAllText(path);

        Assert.Contains("ScavengerScreen", source);
        Assert.Contains("PlayMode.ScavengerHunt", source);
    }

    [Fact]
    public void ScavengerScreen_Component_Should_Exist_With_Checkboxes_And_ProgressMeter()
    {
        var path = PathHelper.InRepo("SocOps", "Components", "ScavengerScreen.razor");
        Assert.True(File.Exists(path), "ScavengerScreen.razor should exist.");

        var source = File.ReadAllText(path);

        Assert.Contains("type=\"checkbox\"", source);
        Assert.Contains("role=\"progressbar\"", source);
        Assert.Contains("ProgressPercent", source);
    }

    [Fact]
    public void AppCss_Should_Define_Scavenger_List_And_Progress_Meter_Styles()
    {
        var path = PathHelper.InRepo("SocOps", "wwwroot", "css", "app.css");
        var source = File.ReadAllText(path);

        Assert.Contains(".scavenger-list", source);
        Assert.Contains(".scavenger-item", source);
        Assert.Contains(".progress-track", source);
        Assert.Contains(".progress-fill", source);
    }
}
