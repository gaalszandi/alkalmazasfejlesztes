﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EnvironmentSimulator
{
    public class Simulator : ISimulator
    {
        double speed;
        public double Speed
        {
            get => speed;
            set
            {
                if (!Double.Equals(speed, value))
                {
                    speed = value;
                    OnSpeedChanged?.Invoke(speed);
                }
            }
        }

        double direction;
        public double Direction
        {
            get => direction;
            set
            {
                if (!Double.Equals(direction, value))
                {
                    direction = value;
                    OnDirectionChanged?.Invoke(direction);
                }
            }
        }

        public double Turn { get; set; }

        public event OnSpeedChangedEvent OnSpeedChanged;
        public event OnDirectionChangedEvent OnDirectionChanged;

    }
}
