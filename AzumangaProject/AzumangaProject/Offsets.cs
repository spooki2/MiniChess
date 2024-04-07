namespace AzumangaProject;

public static class Offsets
{
    public const int Up = -6;
    public const int Down = 6;
    public const int Left = -1;
    public const int Right = 1;
    public const int diagRU = Up + Right;
    public const int diagRD = Down + Right;
    public const int diagLU = Up + Left;
    public const int diagLD = Down + Left;
    public const int knightUUL = Up + Up + Left;
    public const int knightUUR = Up + Up + Right;
    public const int knightRRU = Right + Right + Up;
    public const int knightRRD = Right + Right + Down;
    public const int knightDDR = Down + Down + Right;
    public const int knightDDL = Down + Down + Left;
    public const int knightLLD = Left + Left + Down;
    public const int knightLLU = Left + Left + Up;

    public static List<int> OffsetList = new List<int>
    {
        Up, Down, Left, Right, diagRU, diagRD, diagLU, diagLD, knightUUL, knightUUR, knightRRU, knightRRD, knightDDR,
        knightDDL, knightLLD, knightLLU
    };

    public static int[] UpVector = { 0, 1 };
    public static int[] DownVector = { 0, -1 };
    public static int[] LeftVector = { -1, 0 };
    public static int[] RightVector = { 1, 0 };
    public static int[] diagRUVector = { 1, 1 };
    public static int[] diagRDVector = { 1, -1 };
    public static int[] diagLUVector = { -1, 1 };
    public static int[] diagLDVector = { -1, -1 };
    public static int[] knightUULVector = { -1, 2 };
    public static int[] knightUURVector = { 1, 2 };
    public static int[] knightRRUVector = { 2, 1 };
    public static int[] knightRRDVector = { 2, -1 };
    public static int[] knightDDRVector = { 1, -2 };
    public static int[] knightDDLVector = { -1, -2 };
    public static int[] knightLLDVector = { -2, -1 };
    public static int[] knightLLUVector = { -2, 1 };

    public static List<int[]> OffsetVectorList = new List<int[]>
    {
        UpVector, DownVector, LeftVector, RightVector, diagRUVector, diagRDVector, diagLUVector, diagLDVector,
        knightUULVector, knightUURVector, knightRRUVector, knightRRDVector, knightDDRVector, knightDDLVector,
        knightLLDVector, knightLLUVector
    };

    public static Dictionary<int, int[]> offsetToVector = new Dictionary<int, int[]>();

    static Offsets() {
        foreach (int i in OffsetList)
        {
            offsetToVector[i] = OffsetVectorList[OffsetList.IndexOf(i)];
        }
    }
}