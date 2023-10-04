# Hacker News Aggregator sample
Simple proof of concept of usage of RestEase, Redis with .NET 6 to work with selected Hacker News endpoints.


## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* Visual Studio 2022 or latest version of JetBrains Rider
* .Net Core 6

### Installing
There is two ways to run this repository depending on where it hosted and does dev certificates is set.
#### If certificates not present
1. Clone the repository
2. Open project in visual studio or rider
3. Run redis service from docker compose
4. Run HackerNewsAggregator project
#### If certificates present
1. Clone the repository
2. Open project in visual studio or rider
3. Update repository network settings in order to let containers connect. Settings of this repository tweaked for running HackerNewsAggregator project through IDE.
4. Change docker-compose settings to make sure they compatible with your local certificates
5. Run docker compose

## Notes
#### RestEase
I used in this project RestEase first time in order to see it in action and get the idea how good it is compare to classic polly approach. 

As far as I see now - it contain basic retry policy compatibility with polly. But with this use case rate limit policy way more important and this package seem to be lacking. 
RestEase is overall nice to work with, but not great to debug in case something goes wrong. 

Given time I'd spend more time looking into alternatives to RestEase or a way to enable RestEase to provide various features that make polly so valuable. 

This project require loading 501 request in worse case scenario where there is no cached data. Requests loaded not in parallel at least, for heaviest API endpoint, but this can be improved. This area is lacking in optimizations in order to ensure highest performance with minimum chances to overload Hacker News Api.

#### Eventual consistency
Because this project does not subscribed to event bus of Hacker News cached records can easily become outdated. In order to mitigate this caches expiry time is used. But such approach have a lot of problematic moments requiring fine tuning.

#### Project may benefit from use of other cache, like Memcached
But it require more testing and real use data.