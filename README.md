## Overview
The Energy System project is a web-based platform designed to optimize energy consumption and reduce electricity costs. By intelligently managing battery storage, the system charges during off-peak hours when electricity prices are lower and discharges during peak hours when prices are higher. This automation enhances energy efficiency and minimizes manual intervention.

## Features
<li>Intelligent Energy Management: Utilizes real-time electricity pricing to determine optimal battery charging and discharging schedules.</Li>

<li>Automated Operations: Schedules battery charging during low-cost periods and discharging during high-cost periods without user intervention.</li>

<li>Real-Time Monitoring: Provides live tracking of energy consumption, battery status, and cost savings.</li>

<li>User-Friendly Interface: Offers an intuitive dashboard for users to monitor and manage their energy usage effectively.</li>

## System Architecture
The project follows a modular architecture comprising three primary layers:

### Services Layer: 
Contains the business logic and algorithms for energy optimization.

#### Projects:
<li>EnergySystem.Services: Implements core services for energy management.</li>
<li>EnergySystem.Services.Data: Manages data-related operations and interactions.</li>

### Data Layer: 
Manages data persistence, including user information, energy consumption records, and battery status.

#### Projects:
<li>EnergySystem.Data: Defines the database context and entities.</li>
<li>EnergySystem.Data.Models: Contains the data models representing the database schema.</li>
<li>EnergySystem.Data.Seeding: Handles initial data seeding for the application.</li>
<li>EnergySystem.Data.Common: Provides common data-related utilities and repositories.</li>

### Web Layer: 
Handles user interactions through a responsive web interface, facilitating seamless communication with the services layer.

#### Projects:
<li>EnergySystem.Web: The main web application project containing controllers and views.</li>
<li>EnergySystem.Web.ViewModels: Defines the view models used to pass data between controllers and views.</li>
