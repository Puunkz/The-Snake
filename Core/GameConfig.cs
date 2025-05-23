namespace The_Snake.Core;

public static class GameConfig
{
   public const int CellSize = 20;
   public const int GridWidth = 30;
   public const int GridHeight = 30;
   
   public static int ScreenWidth => CellSize * GridWidth;
   public static int ScreenHeight => CellSize * GridHeight;
}