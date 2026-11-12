public static class CharacterSelection
{
    public static string[] Names = { "Jane", "John" };
    public static int[] Selected = { -1, -1 };   // [0] = Player 1, [1] = Player 2

    public static bool BothReady { get { return Selected[0] >= 0 && Selected[1] >= 0; } }
}