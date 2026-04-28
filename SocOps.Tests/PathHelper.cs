using System;
using System.IO;

namespace SocOps.Tests;

internal static class PathHelper
{
    public static string RepoRoot()
    {
        return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../"));
    }

    public static string InRepo(params string[] segments)
    {
        var all = new string[segments.Length + 1];
        all[0] = RepoRoot();
        Array.Copy(segments, 0, all, 1, segments.Length);
        return Path.Combine(all);
    }
}
