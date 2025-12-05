namespace SerializingShapes;

// Copied from my other projects, adapted for this solution. 
/// <summary>ANSI escape codes for console coloring</summary>
/// <remarks>
/// <para>Author: Eddie C.</para>
/// <para>Version: 1.2</para>
/// <para>Since: 2025-11-29</para>
/// </remarks>
public static class AnsiColorCodes
{
    /// <summary>Reset colors to default</summary>
    public const string Reset = "\e[0m";

    /// <summary> Red font and white background for error messages</summary>
    public const string HighlightError = "\e[48;2;255;255;255;38;2;255;0;0m";

    /// <summary>Bright green font and white background for success messages</summary>
    public const string GreenOnWhite = "\e[48;2;255;255;255;38;2;0;102;0m";

    /// <summary>Blue font and white background for general messages</summary>
    public const string BlueOnWhite = "\e[48;2;255;255;255;38;2;0;0;255m";

    /// <summary>rED font and white background for general messages</summary>
    public const string RedOnWhite = "\e[48;2;255;255;255m\e[38;2;255;0;0m";


    /// <summary> Light blue background color </summary>
    public const string Background = "\e[48;2;26;132;184m";

    /// <summary> White foreground color </summary>
    public const string Foreground = "\e[37m";
}