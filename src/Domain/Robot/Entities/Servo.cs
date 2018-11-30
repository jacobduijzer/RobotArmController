using Domain.Robot.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Robot.Entities
{
    public class Servo : IServo
    {
        public int ServoId => throw new NotImplementedException();

        public int MinimumAngle => throw new NotImplementedException();

        public int MaximumAngle => throw new NotImplementedException();

        public int CurrentAngle => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public void SetAngle(int angle)
        {
            throw new NotImplementedException();
        }
    }
}
