# 🧑‍💼 TIAC HR System

A modern HR management system developed as part of an internship, built using microservice architecture with cutting-edge .NET technologies and a rich Angular frontend. The system handles employee records, holiday requests, approvals, authentication, and real-time updates with clean, scalable, and maintainable code practices.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Key Features](#key-features)
- [Security](#security)
- [Testing & Quality](#testing--quality)
- [Deployment](#deployment)
- [Frontend Highlights](#frontend)
- [Team & Collaboration](#team--collaboration)

---

<a name="overview"></a>
## 📖 Overview

TIAC HR System provides an efficient and scalable platform for managing:
- Employee records
- Holiday request submissions
- Approval workflows involving team leads and managers

The system uses PostgreSQL for persistent storage and is designed around **microservices**, with each core domain separated into individual services and connected through internal APIs.

---

<a name="tech-stack"></a>
## 🛠️ Tech Stack

| Layer             | Technology                                                                 |
|------------------|------------------------------------------------------------------------------|
| Backend           | .NET 7, ASP.NET Core, FastEndpoints, FluentValidation, MediatR, CQRS        |
| Frontend          | Angular, Angular Material, RxJS, WebSockets, JWT Auth                       |
| Database          | PostgreSQL with Entity Framework Core (Code First)                          |
| API Gateway       | YARP Reverse Proxy                                                          |
| DevOps            | Docker (frontend and backend), Docker Compose, company-hosted server        |
| Tools             | Postman, FluentAssertions, Visual Studio, VS Code                           |

---

<a name="architecture"></a>
## 🧱 Architecture

- **Microservice-based**: Independent services for `Employees`, `HolidayRequests`, and other HR-related domains
- **HTTP Communication**: Internal services communicate via HTTP Clients
- **CQRS Pattern**: Clear separation of command and query operations
- **Entity Framework Core**: Code-first migrations and database generation
- **Modular Design**: Each service encapsulates its own business logic, DTOs, and infrastructure
- **Clean Architecture Principles**: SOLID, separation of concerns, no "fat interfaces", and dependency injection via `HostBuilder`

---

<a name="key-features"></a>
## 🚀 Key Features

- 🧑‍💼 **Employee Management** – Create and manage employee data
- 🌴 **Holiday Requests** – Submit and track leave requests
- ✅ **Approval Workflow** – Requests routed to relevant team leads or managers via `HolidayRequestApprover`
- 📬 **Notification Routing** – Request is approved by the first eligible manager in the hierarchy
- 🌐 **WebSocket Integration** – Real-time updates for request status changes
- 📊 **Admin Dashboard** – View request stats and employee overviews

---

<a name="security"></a>
## 🔐 Security

- ✅ JWT-based authentication
- ✅ Role-based authorization
- 🔒 Angular route guards on frontend
- 🔐 Backend attribute-level protection and token validation

---

<a name="testing--quality"></a>
## 🧪 Testing & Quality

- 🧪 Postman collections for API testing
- ✅ FluentAssertions for backend unit/integration tests
- 🧹 Clean, documented code following company and community standards
- ✅ Full DTO layers on frontend and backend for safe and structured data exchange

---

<a name="frontend"></a>
## 🧭 Frontend Highlights

- Developed using **Angular** with **Angular Material**
- Structured feature-based modules for routing and state separation
- Secure API calls via JWT
- WebSocket service for live request status updates
- Centralized DTO usage and HTTP services

---

<a name="deployment"></a>
## 🐳 Deployment

- Dockerfiles for both frontend and backend
- Docker Compose for local orchestration
- Hosted on internal company server after containerization

---

<a name="team--collaboration"></a>
## 🤝 Team & Collaboration

- 👨‍💻 Developed collaboratively by **2 engineers** under the mentorship of a senior developer
- 🔄 Daily check-ins, code reviews, and design discussions
- 🧠 Shared responsibility across services — I worked on:
  - `Employee` and `HolidayRequest` services
  - DTOs, validations, approval logic
  - WebSocket updates and authentication flow
- 🔧 My colleague handled complementary microservices and integration
- 📦 Final solution deployed and tested in a Dockerized environment hosted by the company

---
