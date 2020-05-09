# Readify Knock! Knock! API implementation

![Continuous Integration](https://github.com/Ceridan/readify-knock-knock/workflows/Continuous%20Integration/badge.svg)

This project is my implementation of the funny recruitment process task from the [Readify](https://knockknock.readify.net/).
The main purpose is to build end-to-end simple Web API with Docker containerization and full CI/CD pipeline and also validate final result with Readify checking system.

> ## Caution
>
> Please do not use code from this repo when passing the first stage of Readify recruitment process.
Your application will be rejected and you may be banned.

## How to build and run Docker container with Web API from scratch

First of all you need to clone this repo.

```bash
git clone git@github.com:Ceridan/readify-knock-knock.git
cd readify-knock-knock
```

You must have Docker installed on your machine for the following steps.
Also you need to register on the [Readify recruitment portal](https://join.readify.net/)
to obtain your token to pass as the value for the READIFY_TOKEN environment variable.

```bash
docker build --rm --tag readify-knock-knock .

docker run --rm --name readify-knock-knock \
    -e ASPNETCORE_ENVIRONMENT=production \
    -e READIFY_TOKEN=<YOUR_READIFY_TOKEN>
    -p 12345:80 -d readify-knock-knock
```

Now your API is up and running on the port 12345.
You can test all API endpoints with Swagger http://localhost:12345/swagger
or send requests directly to the exposed API http://localhost:12345/api
