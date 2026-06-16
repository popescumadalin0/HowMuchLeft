# HowMuchLeft

A **Blazor Server** web application that calculates the total consumption of ingredients (coffee, chocolate, tea, milk, water) by uploading a CSV report. Deployed on Netlify.

## Overview

HowMuchLeft is a simple utility app aimed at tracking ingredient usage. Users upload a CSV file (e.g. a daily/weekly consumption report), and the app computes the total amounts consumed for each ingredient category.

## Features

- Upload a CSV report file
- Automatically calculates totals for:
  - Coffee (g)
    - Chocolate (g)
      - Tea (g)
        - Milk (ml)
          - Water (ml)
          - Edit recipes via the dedicated page
          - Authentication support (ASP.NET Identity)
          - Clean UI with MudBlazor components

          ## Technologies

          - C# / .NET
          - Blazor Server (ASP.NET Core)
          - MudBlazor UI library
          - BlazorBootstrap
          - ASP.NET Core Identity
          - Netlify (deployment)

          ## Project Structure

          ```
          HowMuchLeft/
            Components/
                Pages/
                      Home.razor           Main page — CSV upload and totals display
                            EditRecipes.razor    Recipe management page
                                  Auth.razor           Authentication page
                                      Layout/                App layout components
                                        Models/                  Data models
                                          Extensions/              Helper extension methods
                                            Program.cs               App entry point and service configuration
                                            HowMuchLeft.sln
                                            netlify.toml               Netlify deployment config
                                            ```

                                            ## Setup

                                            1. Clone the repository:
                                               ```bash
                                                  git clone https://github.com/popescumadalin0/HowMuchLeft.git
                                                     cd HowMuchLeft
                                                        ```

                                                        2. Build and run:
                                                           ```bash
                                                              dotnet run --project HowMuchLeft
                                                                 ```

                                                                 3. Open your browser at `https://localhost:5001`

                                                                 ## Usage

                                                                 1. Navigate to the home page
                                                                 2. Click **"Upload your report"** and select a `.csv` file
                                                                 3. The app will parse the file and display the total consumed quantities for each ingredient
