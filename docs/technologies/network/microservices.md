# microservices

[Fallacies of distrubited systems](https://en.wikipedia.org/wiki/Fallacies_of_distributed_computing):
- The network is reliable;
- Latency is zero;
- Bandwidth is infinite;
- The network is secure;
- Topology doesn't change;
- There is one administrator;
- Transport cost is zero;
- The network is homogeneous.

To make a system consisting of microservices more reliable in terms of latency, bandwidth, and security, several strategies can be employed. These include implementing load balancing and caching to improve latency and bandwidth, using encryption and authentication mechanisms to enhance security, and employing monitoring and logging to detect and respond to potential issues.

Dealing with the fact that a network may not be secure as a microservice developer involves implementing secure communication protocols such as SSL/TLS, using strong authentication mechanisms, and implementing access control and authorization policies.

The fact that the network is not homogeneous or that topology has changed can affect the performance of microservices by introducing latency and potential points of failure. To mitigate this, strategies such as service discovery, dynamic routing, and circuit breakers can be employed to adapt to changes in network topology and maintain reliable communication between microservices.

## Communication types

The first axis defines if the protocol is synchronous or asynchronous:
- Synchronous protocol (HTTP/HTTPS).
- Asynchronous protocol (AMQP, for example RabbitMQ). 

The second axis defines if the communication has a single receiver or multiple receivers:
- Single receiver. Each request must be processed by exactly one receiver or service. An example of this communication is the [Command pattern](https://en.wikipedia.org/wiki/Command_pattern).
- Multiple receivers. Each request can be processed by zero to multiple receivers. This type of communication must be asynchronous. An example is the [publish/subscribe](https://en.wikipedia.org/wiki/Publish%E2%80%93subscribe_pattern) mechanism used in patterns like Event-driven architecture.

A microservice-based application will often use a combination of these communication styles. The most common type is single-receiver communication with a synchronous protocol like HTTP/HTTPS when invoking a regular Web API HTTP service. Microservices also typically use messaging protocols for asynchronous communication between microservices.
