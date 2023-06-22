using EuropeDominationDemo.Scripts.Math;
using Godot;

namespace EuropeDominationDemo.Scripts.Text;

public partial class TextBezierCurve : Node
{
	private PackedScene _textScene;
	public BezierCurve Curve;
	public Arc TextPath;
	public float FontSize;
	public string TextOnCurve;

	public override void _Ready()
	{
		_textScene = (PackedScene)GD.Load("res://Prefabs/Letter.tscn");
		DrawText();
	}

	public void DrawText()
	{
		if (Curve.Segment1.X > Curve.Segment2.X)
			(Curve.Segment1, Curve.Segment2) = (Curve.Segment2, Curve.Segment1);

		for (int i = 0; i < TextOnCurve.Length; i++)
		{
			var t = (float)i / ((float)(TextOnCurve.Length - 1));
			var obj = (Label)_textScene.Instantiate();

			//obj.LabelSettings.FontSize = Mathf.RoundToInt(FontSize);

			obj.Size = new Vector2((float)obj.LabelSettings.FontSize / obj.Size.Y * obj.Size.X,
				(float)obj.LabelSettings.FontSize);

			//obj.Scale = new Vector2(FontSize / (float)obj.LabelSettings.FontSize, FontSize / (float)obj.LabelSettings.FontSize);

			obj.Position = TextPath.GetPoint(t) - obj.Size / 2;
			obj.PivotOffset = obj.Size / 2;
			var angle = TextPath.GetTangent(t).Angle();
			obj.Rotation = angle;
			obj.Text = TextOnCurve[i].ToString();
			
			AddChild(obj);
		}
	}
}
