﻿using System.Collections.Generic;
using EuropeDominationDemo.Scripts.Scenarios;
using Godot;

namespace EuropeDominationDemo.Scripts.Handlers;

public class CallMulticaster
{
    private readonly List<GameHandler> _gameHandlers;
    private MapData _mapData;

    private int _previousMonth;
    private int _previousYear;

    public CallMulticaster(List<GameHandler> gameHandlers)
    {
        _gameHandlers = gameHandlers;
    }


    public void Init(MapData mapData)
    {
        _mapData = mapData;
        foreach (var handler in _gameHandlers)
        {
            handler.Init(mapData);
        }
    }

    public void InputHandle(InputEvent @event, int tileId)
    {
        foreach (var handler in _gameHandlers)
        {
            if (handler.InputHandle(@event, tileId))
                return;
        }
    }

    public void TimeTick()
    {
        _mapData.Scenario.Date = _mapData.Scenario.Date.Add(_mapData.Scenario.Ts);

        foreach (var handler in _gameHandlers)
        {
            handler.DayTick();
        }

        if (_previousMonth != _mapData.Scenario.Date.Month)
        {
            foreach (var handler in _gameHandlers)
            {
                handler.MonthTick();
            }

            _previousMonth = _mapData.Scenario.Date.Month;
        }

        if (_previousYear != _mapData.Scenario.Date.Year)
        {
            foreach (var handler in _gameHandlers)
            {
                handler.YearTick();
            }

            _previousYear = _mapData.Scenario.Date.Year;
        }
    }

    public void ViewModUpdate(float zoom)
    {
        foreach (var handler in _gameHandlers)
        {
            handler.ViewModUpdate(zoom);
        }
    }

    public void GUIInteractionHandler(GUIEvent @event)
    {
        foreach (var handler in _gameHandlers)
        {
            handler.GUIInteractionHandler(@event);
        }
    }
}