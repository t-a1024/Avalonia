using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;

class BackDrop : Canvas
{
    private readonly List<ItemImage> itemImages =
    [
        new("さんた", "christmas_ornament01_santa.png"),
        new ItemImage("はなわ", "christmas_ornament02_wreath.png"),
        new ItemImage("ゆきだるま", "christmas_ornament03_snowman.png"),
        new ItemImage("ゆきだるま", "christmas_ornament04_snowman.png"),
        new ItemImage("ぷれぜんと", "christmas_ornament05_present.png"),
        new ItemImage("ぷれぜんと", "christmas_ornament06_present.png"),
        new ItemImage("つりー", "christmas_ornament07_tree.png"),
        new ItemImage("くつした", "christmas_ornament08_sock.png"),
        new ItemImage("すず", "christmas_ornament09_bells.png"),
        new ItemImage("すず", "christmas_ornament10_bell.png"),
        new ItemImage("ほし", "christmas_ornament11_star.png"),
        new ItemImage("あめ", "christmas_ornament12_candy.png"),
        new ItemImage("ろうそく", "christmas_ornament13_candle.png"),
        new ItemImage("ひいらぎ", "christmas_ornament14_hiiragi.png"),
        new ItemImage("まつぼっくり", "christmas_ornament15_matsubokkuri.png"),
        new ItemImage("となかい", "christmas_ornament16_tonakai.png"),
        new ItemImage("ほし", "christmas_ornament17_topstar.png"),
        new ItemImage("すたー", "christmas_ornament17_topstar.png"),
    ];

   private readonly string Imagedirectory = "Assets";
  
    public BackDrop()
    {

        Update();
    }

    public void Update(){
        // 長方形で背景を塗りつぶす
        var rectangle = new Rectangle
        {
            Fill = Brushes.LightBlue,  // 塗りつぶし色
            Width = 9999,
            Height = 9999
        };

        // CanvasにRectangleを追加
        Children.Add(rectangle);
    }
    public void AddBackItem(string word)
    {
        // 名前で一致するアイテムを検索
        var matchedItem = itemImages.Find(item => item.Name == word);
        if (matchedItem == null)
        {
            Console.WriteLine($"Item with name '{word}' not found.");
            return;
        }

        // 画像のファイルパスを作成
        var filePath = System.IO.Path.Combine(Imagedirectory, matchedItem.FileName);

        // 画像を読み込み
        var image = new Image
        {
            Source = new Avalonia.Media.Imaging.Bitmap(filePath),
            Width = 100, // 必要に応じてサイズ調整
            Height = 100
        };

        // ランダムな位置に配置
        var random = new Random();
        SetLeft(image, random.Next(0, 1100));
        SetTop(image, random.Next(0,650));

        // Canvasに追加
        Children.Add(image);
    }
    public void Reset(){
        Children.Clear();
        Update();
    }
}


class ItemImage(string name, string fileName)
{
    public string Name { get; } = name;
    public string FileName { get; } = fileName;
}