# Job Portal API (.NET)

A simple ASP.NET Core Web API for:

- **Employees** to register and upload resumes.
- **Employers** to search for candidates by skills, experience, and location.
- **UAN validation** to ensure employee UAN is exactly 12 digits.

## Project Structure

- `src/JobPortalApi` - main API project

## Endpoints

### 1) Employee registration with resume upload

`POST /api/employees/register` (multipart/form-data)

Form fields:
- `FullName`
- `Email`
- `PhoneNumber`
- `Uan` (must be 12 digits)
- `SkillsCsv` (comma-separated skills)
- `YearsOfExperience`
- `PreferredLocation`
- `resume` (file upload)

### 2) Employer search candidates

`GET /api/employers/candidates/search?skill=dotnet&minimumYearsOfExperience=3&preferredLocation=Bengaluru`

### 3) Get employee profile by id

`GET /api/employees/{id}`

## Run

```bash
cd src/JobPortalApi
dotnet restore
dotnet run
```

Then open Swagger (development environment):
`https://localhost:<port>/swagger`

## Notes

- Resume files are stored under `src/JobPortalApi/Uploads`.
- Data storage is in-memory (`CandidateRepository`) for simplicity.
