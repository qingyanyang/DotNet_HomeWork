## Assignment

1. Define the delegates, please practice the different types:

- single delegate - the delegate only used by score top 1 student

e.g. define the delegate HonourBonus for the top 1 score student, associate the top 1 student's ReceiveBonus method with this delegate

- multicast delegate

e.g. define the delegate CourseStarting for all students, associate the all student's Start method with this delegate

In the main method, please initiate the instance of delegate to call associated methods and print out the result (in console log)

2. Define the events

Please consider the difference when using event keyword with the above delegate.

- Update existing Course class which will be treated as Subject/Observable/Publisher, to add an event - ScheduledTimeChanged (means the course time updated/changed)
- Update the existing Student class, which will be the Observer/Subscriber, to add the event handler (method) to listen above event and accordingly update the behavior (print out something in console log)
- Simulate some students could quite the course, please remove the subscription from these students
- Imaging new students joined and interested in the course, please add the new subscriptions for the event.
- Please trigger the time changed event, and print out all subscriber/observer reactions (print out something in console log)
