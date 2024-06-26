﻿namespace DesignPatterns.Solid.LiskovSubstitution;
// The Liskov Substitution Principle states that objects of a superclass shall be replaceable with objects of its subclasses without breaking the application.
// The Liskov Substitution Principle is a way to ensure that inheritance is used correctly.
// If you have a class that inherits from another class, you should ensure the base class can be replaced with the subclass without affecting the behavior of the program.
public class LiskovSubstitution
{
    public static void Run()
    {
        static int Area(Rectangle r) => r.Width * r.Height;
        Rectangle rc = new(2, 3);
        WriteLine($"{rc} has area {Area(rc)}");

        Square sq = new();
        sq.Width = 4;
        WriteLine($"{sq} has area {Area(sq)}");
    }
}

public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }

    public Rectangle()
    {

    }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public override string ToString()
    {
        return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }
}

public class Square : Rectangle
{
    public override int Width
    {
        set { base.Width = base.Height = value; }
    }

    public override int Height
    {
        set { base.Width = base.Height = value; }
    }
}