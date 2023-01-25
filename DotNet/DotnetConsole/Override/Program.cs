using System;

class Program
{
    static void Main(string[] args)
    {
        BaseClass baseClass = new BaseClass();
        baseClass.MyMethod();  // Output: "Base class method."

        DerivedClass derivedClass = new DerivedClass();
        derivedClass.MyMethod();  // Output: "Derived class method."
    }
}
class BaseClass
{
    public virtual void MyMethod()
    {
        Console.WriteLine("Base class method.");
    }
}

class DerivedClass : BaseClass
{
    public override void MyMethod()
    {
        Console.WriteLine("Derived class method.");
    }
}