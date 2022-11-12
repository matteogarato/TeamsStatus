using System.Device.Gpio;
using TeamsStatus.ConsoleApp.Interfaces;

namespace TeamsStatus.ConsoleApp
{
    public class Stoplight : IStoplight
    {
        private readonly GpioController _gpioController;
        private readonly int _pin;

        public Stoplight()
        {
            _pin = 18;
            _gpioController = new GpioController();
            _gpioController.OpenPin(_pin, PinMode.Output);
        }

        public void SetGreen()
        {
            _gpioController.Write(_pin, PinValue.Low);
            #if DEBUG
            Console.WriteLine("Green");
            #endif
        }

        public void SetRed()
        {
            _gpioController.Write(_pin, PinValue.High);
            #if DEBUG
            Console.WriteLine("Red");
            #endif
        }
    }
}