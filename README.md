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
Implement IInterceptor or use WrappedInterceptor and inject an Action<IInterceptionContext>

```
class MyInterceptor : IInterceptor
{
  public void Invoke(IInterceptionContext context)
  {
    // DoStuff();
  }
}

...
public void AnyAction(IInterceptionContext context)
{ /* DoOtherStuff()*/ }

var wrapper = new WrapperInterceptor(AnyAction);
...

```

Next up instantiate the Pipe with the static Factory.Create method.
This method has several overloads and lets you define which predefined Actions should be performed on pre and postprocessing the 
objects. Furthermore you can specify a default IInterceptioncontext. This example uses both of the above defined Interceptors.

When you acquired the pipe, you can start the interception, which will trigger a preprocessing for all interceptors and directly after that will 
start a postprocessing. To invoke either alone, call the wanted method on the IInterceptionPipe instance.
```
var pipe = Factory.Create(new MyInterceptor(), wrapper);

pipe.StartIntercept();
```

## todo
make async / return Task
rename forward and backward to pre and postprocess
with InterceptionContext a second argument of Type T has to be provided. (does not intercept any object til now)
