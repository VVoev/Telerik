﻿namespace Strategy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Animal dog = new Dog();

            Animal bird = new Bird();

            dog.TryToFly();

            bird.TryToFly();

            dog.SetFlyingAbility(new ItFlys());

            dog.TryToFly();
        }
    }
}