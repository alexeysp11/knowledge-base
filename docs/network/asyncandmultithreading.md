# Use of asynchronous and multithreading programming 

## Microservice architecture 

ASP.NET platform is multithreaded by its nature and provides programming models that shield us from the complexity of using threads.

ASP.NET uses the .NET [threadpool](https://learn.microsoft.com/en-us/dotnet/api/system.threading.threadpool) (which is configurable).
Each request is received by one of the threads in the threadpool, until each thread is already occupied. 
Then requests queue at the IIS Stack, until this also spills over. From there new requests are meet with the very ugly 'Server is unavailable' message.

In computer programming, a [thread pool](https://en.wikipedia.org/wiki/Thread_pool) is a software design pattern for achieving concurrency of execution in a computer program. Often also called a replicated workers or worker-crew model, a thread pool maintains multiple threads waiting for tasks to be allocated for concurrent execution by the supervising program.

In ASP.NET, multithreading is implemented through the use of controllers and the underlying framework. ASP.NET is inherently multithreaded, meaning that it can handle multiple requests concurrently using separate threads. Controllers in ASP.NET provide a way to handle incoming requests and process them in a multithreaded manner, allowing for efficient handling of web requests without needing to manage threads directly.

The ASP.NET framework abstracts away much of the complexity of multithreading, providing a programming model that shields developers from having to manage threads manually. This allows developers to focus on writing code to handle requests and business logic without needing to worry about low-level thread management.

Microservice architecture can be used as an alternative to implementing multithreading manually in code. In a microservice architecture, different components of an application are developed and deployed as independent services, each running in its own process and communicating with each other over a network. This allows for scalability, resilience, and flexibility in managing different parts of the application.

The pros of using microservice architecture include improved scalability, fault isolation, technology diversity, and easier deployment and management of individual services. However, there are also cons such as increased complexity in managing distributed systems, potential performance overhead due to network communication, and additional operational overhead in managing multiple services.

Implementing multithreading manually in code may be more profitable in certain cases where fine-grained control over thread management is necessary, such as in performance-critical sections of code or when dealing with specific hardware or resource constraints.

