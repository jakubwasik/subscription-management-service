# Subscription Management Service

## Prerequisites

* Docker installed
* Visual Studio 2022+ or Visual Studio Code

## Getting started

Service can be run with command:

```bash
cd src/ && docker compose up
```

Above command spins up two containers: `mssql` and `api`. Api is accessible on: http://localhost:9000/swagger/index.html 

In order to run from VS, run sql server with command:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

and then F5 in VS.  Api is accessible on: https://localhost:7051/swagger/index.html


## Domain Assumptions and Rationale
- User and Subscription entities are represented as one-to-one relationship
    - User can have only one subscription which can be either started (resumed) or stopped (paused)
    - Pausing subscription means that it won't be automatically renewed
      - User can still access resources until subscription period terminates (subscription is still active)
    - Starting subscription means that it will be automatically renewed every month
      - It's not automatically active. Subscription is active only if was paid
    - Operations on subscription / subscription history is beyond the scope of this implementation (for the sake of simplicity)

## Implementation Assumptions and Shortcuts
- Soultion has both some samples of `Integration Tests` and `Unit Test`
  - `Integration Test` spins up docker container with db for the whole test suite.
    - rationale: to be as close production implementation as possible


## TODO

This section defines what could be done next

- Authentication and Authorization
- Logging
- Increase test coverage
- Add message broker (i.e Service Bus, Rabbitmq) to integrate with Payment Service
- Add Open Telemetry and instrument Application
- Add Helm charts for k8s deployment