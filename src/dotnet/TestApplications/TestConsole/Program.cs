using System;
using System.Threading.Tasks;
using Application.Communication.Services;
using Application.Robot.Services;
using Domain.Robot.Entities;
using Infrastructure.Communication;
using Infrastructure.Instructions;

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

            var instructionsRepository = new InstructionsRepository();

            var communicationService = new CommunicationService(serialConnection);

            var robot = Robot.Builder()
                .WithServo(baseServo)
                .Build();

            var robotService = new RobotService(communicationService, 
                                                instructionsRepository,
                                                robot);

            //robotService.Initialize();
            //robot.MoveServo(baseServo.ServoId, baseServo.MaximumAngle);

            //await Task.Delay(1000);

            //robot.MoveServo(baseServo.ServoId, baseServo.MinimumAngle);

            Console.ReadLine();
        }
    }
}
