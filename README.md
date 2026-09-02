# EventCore

Event check-in platform built with .NET microservices and React — focused on solving real-time concurrency (duplicate QR scans) and live capacity tracking via SignalR.

> 🚧 Work in progress — portfolio project, actively being built in public.

## The problem

Organizers create events with limited capacity. Attendees register and receive a QR code. Staff scans that QR at the entrance to check attendees in, in real time. Two things make this non-trivial:
- **Never letting the same QR be checked in twice**, even under concurrent scans at the door.
- Keeping a **live capacity counter** in sync across every connected screen.

## Architecture

Two independently deployable microservices behind an API Gateway:

- **Events & Registration Service** — event CRUD, attendee registration (capacity-aware), QR generation. Publishes an async event when a ticket is created.
- **Check-in Service** — owns a local, eventually-consistent copy of valid tickets, validates scanned QR codes, protects against double check-in, and pushes live capacity updates to connected clients via SignalR.
- **Gateway** (YARP) — single entry point for the frontend, routes to both services.

Each service owns its own database (SQL Server) and its own solution — they only communicate over the network, never through shared code or a shared database.

## Tech stack

.NET 10 · ASP.NET Core · Entity Framework Core · SQL Server · YARP · SignalR · React · xUnit · Docker

## Status / Roadmap

- [x] Domain model & architecture decisions
- [x] Solution setup (3 services, Docker Compose for local SQL Server)
- [ ] Events & Registration Service
- [ ] Check-in Service (concurrency-safe check-in)
- [ ] Service-to-service communication
- [ ] Unit tests (concurrency-focused)
- [ ] React frontend
- [ ] Full Dockerization
- [ ] Azure deployment
- [ ] Architecture decision records ([docs/decisions](./docs/decisions))
