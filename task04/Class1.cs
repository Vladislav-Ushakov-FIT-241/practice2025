using System;

namespace task04
{
    public interface ISpaceship
    {
        void MoveForward();      // Движение вперед
        void Rotate(int angle);  // Поворот на угол (градусы)
        void Fire();             // Выстрел ракетой
        int Speed { get; }       // Скорость корабля
        int FirePower { get; }   // Мощность выстрела
    }

    public class Cruiser : ISpaceship
    {
        public int Speed { get; } = 50;
        public int FirePower { get; } = 100;
        public int Position { get; private set; } = 0;
        public int Angle { get; private set; } = 0;
        public int Ammo { get; private set; } = 4;

        public void MoveForward()
        {
            Position += Speed;
        }

        public void Rotate(int angle)
        {
            Angle = (Angle + angle) % 360;
        }

        public void Fire()
        {
            if (Ammo > 0) Ammo--;
        }
    }

    public class Fighter : ISpaceship
    {
        public int Speed { get; } = 100;
        public int FirePower { get; } = 20;
        public int Position { get; private set; } = 0;
        public int Angle { get; private set; } = 0;
        public int Ammo { get; private set; } = 20;

        public void MoveForward()
        {
            Position += Speed;
        }

        public void Rotate(int angle)
        {
            Angle = (Angle + angle) % 360;
        }

        public void Fire()
        {
            if (Ammo > 0) Ammo--;
        }
    }
}
