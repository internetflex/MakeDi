Simple Dependency Injection

This was designed to be used with ASP.Net where my internet host prevented the use of my Castle based
DI - because castle used a form of access that violated their security protocols.

The design here is more of structural code than anything real world related.

For MVC resolution create a class the inherits from DefaultControllerFactory
and implements the function 
	IController GetControllerInstance(RequestContext requestContext, Type controllerType)

using Make's function Get(Type type) to resolve the controllerType.


Likewise for API service call create a class that implements IHttpControllerActivator
and implements the function
	
	IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)

using Make's function Get(Type type) to resolve the controllerType.