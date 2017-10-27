# NService Bus Command Queue Example

Contains two projects, publisher and subscriber. Subscriber takes a command and then SendsLocal until commands are completely processed.

## Configuration

Requires two databases for persistence:

 - NServiceBusPublisher
 - NServiceBusSubscriber

Tables will be populated by NServiceBus