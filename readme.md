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