using System;

class Program
{
    static void Main(string[] args)
    {
        MyStaticClassTwo.CounterWrite();
        MyStaticClassThird.CounterWrite();
        MyConcrateClass myConcrateClass = new MyConcrateClass();
        myConcrateClass.Hello();

        MyConcrateClass.HelloStatic();
    }
}

public static class MyStaticClass
{
    public static int counter { get; set; }
}

public class MyStaticClassTwo
{
    public static void CounterWrite()
    {
        MyStaticClass.counter += 1;
        Console.WriteLine(MyStaticClass.counter);
    }
}

public class MyStaticClassThird
{
    public static void CounterWrite()
    {
        MyStaticClass.counter += 1;
        Console.WriteLine(MyStaticClass.counter);
    }
}

public class MyConcrateClass
{
    public static void HelloStatic()
    {

        Console.WriteLine("Hello from concrate inside static");
    }

    public void Hello()
    {

        Console.WriteLine("Hello from concrate");
    }
}