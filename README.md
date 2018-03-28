# heitech.InterceptXt
Customizable Pipeline (think WCF Messageinterception, ASP Net MVC Middleware)


## What and why
If you ever used .Net Remoting, WCF or Asp.Net MVC you will notice the pattern.
If not, this library lets you pre- and postprocess objects with objects that implement the IInterceptor interface. 

You simply need to register IInterceptors in the order you want them to be intercepted.
You can also provide content between different Interceptionsteps with the IInterceptionContext, which is a wrapper around a Dictionary<string, object>.

A possible scenario is to use decorated components and build a specific interceptor for each decoration.
The Interceptor can then process the decoration, add new ones or remove others etc.

## How
Implement IInterceptor<T> or use WrappedInterceptor<T> and inject an Action<IInterceptionContext, T>

```
class MyInterceptor<string> : IInterceptor<string>
{
  public void Invoke(IInterceptionContext context)
  {
    // DoStuff();
  }
}

...
public void AnyAction(IInterceptionContext context, string item)
{ /* DoOtherStuff()*/ }

var wrapper = new WrapperInterceptor<string>(AnyAction);
...

```

Next up instantiate the Pipe<T> with the static Factory.Create<T> method.
This method has several overloads and lets you define which predefined Actions should be performed on pre and postprocessing the 
objects. Furthermore you can specify a default IInterceptioncontext. This example uses both of the above defined Interceptors.

When you acquired the pipe, you can start the interception, which will trigger a preprocessing for all interceptors and directly after that will 
start a postprocessing. To invoke either alone, call the desired method on the IInterceptionPipe<T> instance.
```
var pipe = Factory.Create<string>(new MyInterceptor(), wrapper);

pipe.Process("any interceptable string value");
```

## todo
make async / return Task methods
