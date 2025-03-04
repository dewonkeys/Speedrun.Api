# Speedrun API

A .NET Core API for managing speedrun data including segments, strains, and systems.

## Features
- CRUD operations for speedrun segments - the individually timed legs of a speedrun - "Brush teeth" - "Shower" - etc.
- CRUD operations for speedrun strains - systems may have different strains - like "Morning Routine: Rushed" and "Morning Routine: Full"
- CRUD operations for speedrun systems - the overarching speedrun, i.e. "Morning Routine"

## API Endpoints

### Segments
- `GET /api/srsegments` - Get all segments
- `GET /api/srsegments/{id}` - Get a specific segment by ID
- `POST /api/srsegments` - Create a new segment
- `PUT /api/srsegments/{id}` - Update an existing segment
- `DELETE /api/srsegments/{id}` - Delete a segment

### Strains
- `GET /api/srstrains` - Get all strains
- `GET /api/srstrains/{id}` - Get a specific strain by ID
- `POST /api/srstrains` - Create a new strain
- `PUT /api/srstrains/{id}` - Update an existing strain
- `DELETE /api/srstrains/{id}` - Delete a strain

### Systems
- `GET /api/srsystems` - Get all systems
- `GET /api/srsystems/{id}` - Get a specific system by ID
- `POST /api/srsystems` - Create a new system
- `PUT /api/srsystems/{id}` - Update an existing system
- `DELETE /api/srsystems/{id}` - Delete a system

## Technologies
- .NET Core
- Entity Framework Core
- RESTful API design