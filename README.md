# E-Recruitment Website Project

This project is an ASP.NET MVC web application for an E-Recruitment system. It features a comprehensive platform where recruiters can manage job offers and candidates can search for jobs and apply to them. The system is divided into three main sections: Job Offers, Recruiter Space, and Candidate Space.

## Features

### Job Offer Space
- **Accessible to all users**: Display all available job offers.
- **Job Offer Details**: Includes ID, recruiter ID, contract type (Permanent, Temporary), sector, required education level (Bachelor's, Master's, Engineering degree, etc.), position, salary, etc.
### Recruiter Space
- **Offer Management**: Recruiters can add, edit, or delete their job offers.
- **Candidate Applications**: View candidates who have applied to their offers and their resumes.
- **Application Statistics**: Access statistics on the applications received.
- **Browse Other Offers**: View job offers from other recruiters.

### Candidate Space
- **Job Offer Viewing**: Candidates can view all job offers.
- **Job Search**: Perform searches among the job offers.
- **Apply to Offers**: Submit applications to job offers.
- **Application History**: View the history of offers applied to.

## Development Approach

- The project can be developed using either the Code First or Database First approach.
- Recruiters and candidates must authenticate (Windows or Form Authentication) before performing any operations.
- The web interfaces are designed using Bootstrap for responsiveness and user-friendly navigation.
- Deployment is done on IIS (Internet Information Services).

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- SQL Server 2019 or later (for Database First approach)
- IIS for deployment

### Installation

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Update the connection string in `web.config` as per your database server.
5. Run the application.

## Deployment

- Ensure IIS is installed and configured on your server.
- Publish the ASP.NET MVC application from Visual Studio to a local folder.
- Copy the published files to your server and set up a new website in IIS pointing to the copied files.

## Built With

- ASP.NET MVC - The web framework used
- Bootstrap - For responsive design
- SQL Server - Database management

## Authors

- **Oussama MAZOUZ**
