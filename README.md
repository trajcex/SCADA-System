# SCADA-System

## Overview
This project implements a SCADA (Supervisory Control and Data Acquisition) system, which is widely used in industrial automation to monitor and control processes, infrastructure, and facilities. SCADA systems are crucial in industries such as manufacturing, power generation, water treatment, and transportation, where real-time data acquisition, control, and monitoring are essential for operational efficiency and safety.

This SCADA system provides comprehensive functionalities for managing digital and analog tags, user authentication, real-time monitoring, alarm management, and reporting.

## Components

### Database Manager
- Provides a user interface for managing tags, scanning status, and output tag values.
- Handles user registration and login.
- Defines alarms for tag values.

### Trending App
- Displays current values of input tags that are on scan.

### SCADA Core
- Central component enabling client-server communication.
- Includes Simulation Driver for signal generation and Tag Processing for timely tag value updates.
- Manages Real-Time Driver for handling RTU data.

### Real-Time Unit (RTU)
- Simulates real-time data acquisition from field devices.
- Sends digitally signed messages to the Real-Time Driver.

### Alarm Display
- Console application displaying active alarms with details.

### Report Manager
- Menu-driven application for generating and displaying various system reports.

## Configuration Files
- `scadaConfig.xml`: Stores system configuration, ensuring the latest configuration is saved on startup/shutdown.
- `alarmConfig.xml`: Stores alarm configurations.

## Log Files
- `alarmsLog.txt`: Logs details of alarm events.

## Usage
To use the SCADA system, follow these steps:
1. Start the SCADA Core to initialize the system.
2. Use the Database Manager for tag management, user operations, and alarm definitions.
3. Monitor tag values with the Trending App.
4. Check alarm events using the Alarm Display.
5. Generate reports through the Report Manager.
6. Ensure configurations are saved and loaded properly via `scadaConfig.xml` and `alarmConfig.xml`.
