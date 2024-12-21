using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;

public class BackDrop : Canvas
{
    private List<ItemImage> itemImages =
    [
        new ItemImage("さんた", "christmas_ornament01_santa.png"),
        new ItemImage("はなわ", "christmas_ornament02_wreath.png"),
        new ItemImage("ゆきだるま", "christmas_ornament03_snowman.png"),
        new ItemImage("ぷれぜんと", "christmas_ornament05_present.png"),
        new ItemImage("つりー", "christmas_ornament07_tree.png"),
        // 他のアイテムもここに追加
    ];
//     private itemImages = [
//     {"name": "さんた", "fileName": "christmas_ornament01_santa.png"},
//     { "name": "はなわ", "fileName": "christmas_ornament02_wreath.png"},
//     {"name": "ゆきだるま", "fileName": "christmas_ornament03_snowman.png"},
//     {"name": "ゆきだるま", "fileName": "christmas_ornament04_snowman.png"},
//     {"name": "ぷれぜんと", "fileName": "christmas_ornament05_present.png"},
//     {"name": "ぷれぜんと", "fileName": "christmas_ornament06_present.png"},
//     {"name": "つりー", "fileName": "christmas_ornament07_tree.png"},
//     {"name": "くつした", "fileName": "christmas_ornament08_sock.png"},
//     {"name": "すず", "fileName": "christmas_ornament09_bells.png"},
//     {"name": "すず", "fileName": "christmas_ornament10_bell.png"},
//     {"name": "ほし", "fileName": "christmas_ornament11_star.png"},
//     {"name": "あめ", "fileName": "christmas_ornament12_candy.png"},
//     {"name": "ろうそく", "fileName": "christmas_ornament13_candle.png"},
//     {"name": "ひいらぎ", "fileName": "christmas_ornament14_hiiragi.png"},
//     {"name": "まつぼっくり", "fileName": "christmas_ornament15_matsubokkuri.png"},
//     {"name": "となかい", "fileName": "christmas_ornament16_tonakai.png"},
//     {"name": "ほし", "fileName": "christmas_ornament17_topstar.png"},
//     {"name": "すたー", "fileName": "christmas_ornament17_topstar.png"},
//   ];

   private String Imagedirectory = "Assets";
  
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
        SetLeft(image, random.Next(0, 1200));
        SetTop(image, random.Next(0,800));

        // Canvasに追加
        Children.Add(image);
    }
    public void Reset(){
        Children.Clear();
        Update();
    }
}


class ItemImage
{
    public string Name { get; }
    public string FileName { get; }

    public ItemImage(string name, string fileName)
    {
        Name = name;
        FileName = fileName;
    }
}