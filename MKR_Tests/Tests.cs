﻿using Xunit;
using Xunit.Abstractions;

namespace MKR_Tests;

public class Tests
{
    private readonly ITestOutputHelper _output;

    public Tests(ITestOutputHelper output)
    {
        _output = output;

        InitLocation();
    }

    static void InitLocation()
    {
        var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(Path.GetFullPath(Path.Combine(projectRoot, @"../../../")));
    }

    [Theory]
    [InlineData("???", "abc", 64)] // 4 * 4 * 4 = 64
    [InlineData("???", "000", 1)] // 1 * 1 * 1 = 1
    [InlineData("abc", "999", 0)] // 0 * 0 * 0 = 0
    [InlineData("????", "12a3", 4)] // 1 * 1 * 4 * 1 = 4
    [InlineData("4abbe", "?????", 256)] // 1 * 4 * 4 * 4 * 4 = 256
    public void CalcCount_ReturnsCorrectCount(string p1, string p2, int expectedCount)
    {
        var res = CrossPlatformMKR.Program.CalcCount(p1, p2);

        Assert.Equal(expectedCount, res);
        _output.WriteLine($"p1: \"{p1}\" | p2: \"{p2}\" | expected: {expectedCount} | Result: {res}");
    }

    [Theory]
    [InlineData("12345", "45ac")]
    [InlineData("", "")]
    [InlineData("1234567890", "abcdefabcd")]
    [InlineData("&81", "xzq")]
    public void CheckInputs_ShouldThrowArgumentException_ForInvalidData(string? p1, string? p2)
    {
        var exception = Assert.Throws<ArgumentException>(() => CrossPlatformMKR.Program.CheckInputs(p1, p2));

        _output.WriteLine($"p1: \"{p1}\" | p2: \"{p2}\" | message: {exception.Message}");
    }

    [Theory]
    [InlineData("12345", null)]
    [InlineData(null, null)]
    [InlineData("123c45", null)]
    public void CheckInputs_ShouldThrowArgumentNullException_ForInvalidData(string? p1, string? p2)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => CrossPlatformMKR.Program.CheckInputs(p1, p2));

        _output.WriteLine($"p1: \"{p1}\" | p2: \"{p2}\" | message: {exception.Message}");
    }
}