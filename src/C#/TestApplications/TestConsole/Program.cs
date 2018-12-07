using System;
using System.Threading.Tasks;
using Application.Communication.Services;
using Domain.Robot.Entities;
using Infrastructure.Communication;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var baseServo = Servo.Builder()
                .WithName("Base")
                .WithMinimumAngle(0)
                .WithMaximumAngle(180)
                .WithServoId(1)
                .Build();

            var serialConnection = SerialConnection.Builder()
                                                .WithPortName("COM4")
                                                .WithBaudRate(9600)
                                                .WithLogger(new ConsoleLogger())
                                                .Build();

            var communicationService = new CommunicationService(serialConnection);

            var robot = Robot.Builder()
                .WithCommunicationService(communicationService)
                .WithServo(baseServo)
                .Build();

            robot.Initialize();
            robot.MoveServo(baseServo.ServoId, baseServo.MaximumAngle);

            await Task.Delay(1000);

            robot.MoveServo(baseServo.ServoId, baseServo.MinimumAngle);

            Console.ReadLine();
        }
    }
}
