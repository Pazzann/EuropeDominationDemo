using Godot;
using System.Linq;
using EuropeDominationDemo.Scripts.Scenarios;
using EuropeDominationDemo.Scripts.Units;

namespace EuropeDominationDemo.Scripts.Handlers;


public partial class SelectorBoxHandler : GameHandler
{
	
	private bool _isMouseDown = false;
	private Vector2 _startMousePos;
	private Vector2 _endMousePos;
	private bool _wasSelectedUnit = false;
	private ColorRect _selectionRect;
	

	
	public override void Init(MapData mapData)
	{
		_mapData = mapData;
		_selectionRect = GetNode<ColorRect>("./SelectionRect");
	}

	public override bool InputHandle(InputEvent @event, int tileId)
	{
		switch (@event)
		{
			case InputEventMouseButton { ButtonIndex: MouseButton.Left} when _isMouseDown && !@event.IsPressed():{
				_isMouseDown = false;
				_endMousePos = _selectionRect.GetGlobalMousePosition();
				_selectionEnded();
				_selectionRect.Size = Vector2.Zero;
				break;
			}
			case InputEventMouseButton { ButtonIndex: MouseButton.Left}:
			{
				if (@event.IsPressed())
				{
					_isMouseDown = true;
					_startMousePos = _selectionRect.GetGlobalMousePosition();
					_selectionRect.GlobalPosition = _startMousePos;
					_selectionRect.Size = new Vector2(1, 1);
				}

				break;
			}
			case InputEventMouseMotion:
			{
				if(_isMouseDown)
					_update();
				break;
			}
		}

		if (_wasSelectedUnit)
		{
			_wasSelectedUnit = false;
			return true;
		}
		return false;
	}

	public override void ViewModUpdate(float zoom)
	{
		
	}

	public override void GUIInteractionHandler(GUIEvent @event)
	{
		
	}

	private void _update()
	{
		_selectionRect.Scale = new Vector2(_selectionRect.GetGlobalMousePosition().X > _startMousePos.X ? 1 : -1, _selectionRect.GetGlobalMousePosition().Y > _startMousePos.Y ? 1 : -1);
		_selectionRect.Size = (_selectionRect.GetGlobalMousePosition() - _startMousePos) * _selectionRect.Scale;
	}

	private void _selectionEnded()
	{
		var allUnits = GetTree().GetNodesInGroup("ArmyUnit");
		if (allUnits == null)
			return;
		var trueRect = _selectionRect.GetGlobalRect();
		trueRect = new Rect2(new Vector2(_selectionRect.Scale.X > 0 ? trueRect.Position.X : trueRect.Position.X - trueRect.Size.X, _selectionRect.Scale.Y > 0 ? trueRect.Position.Y : trueRect.Position.Y - trueRect.Size.Y), trueRect.Size);
		var selectedUnits = (from unit in allUnits where ((ArmyUnit)unit).IsInsideRect(trueRect) select unit as ArmyUnit).ToList();
		ArmyUnit.SelectUnits(allUnits, selectedUnits);
		
		
		_mapData.CurrentSelectedUnits = selectedUnits;
		
		if (selectedUnits.Count > 0)
			_wasSelectedUnit = true;
	}
	
	public override void DayTick()
	{
		
	}

	public override void MonthTick()
	{
		
	}

	public override void YearTick()
	{
		
	}
}
