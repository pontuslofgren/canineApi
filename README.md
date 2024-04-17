# Canine Cloud - Stepping into to the cloud

## A. Scenario

It finally happened. After years of negligence the pre-Y2K server in the basement has died. Not much of value was lost, but one thing of importance, the website used by the neighborhoods local dog shelter website, **Canine Cloud**, is now gone.

The pictures, the website, all gone... But we can rebuild it! We have the technology! We can make it better than it was. Better, stronger, faster. We can remake it, in the cloud!

## B. What you will be working on

You will be creating the start of Canine Cloud, the replacement to the old static 'HTML 4.01' website previously used by the neighboring dog shelter.

We will work on this application throughout the week and refine it step by step. With the power of the cloud with quick turnarounds and deployments!

The initial goal is to create a WebAPI with connection to an Azure database, while also using and Azure Blob storage.

## C. Setup

Ensure that you have created an Azure account with a free subscription. At least one active per mob.

A word of caution! We are creating resources that are publicly available on the internet this time so it's important to not make sensitive information such as connection strings and passwords public. Never hard-code any sensitive information into your application, not even during initial development. It's incredibly easy to forget what you have in code and accidentally push the wrong things.

One way of keeping things secure during development is to use `dotnet user-secrets` (https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)

To set this up, run the following commands (the init command should only be run once per project and then be commited):

```
dotnet user-secrets init
dotnet user-secrets set "KeyName" "<YourSecretKey>"
```

With the secrets setup locally, you will have then to add them then to the [Azure applications App Settings](https://learn.microsoft.com/en-us/azure/app-service/configure-common?tabs=portal) later when the application is deployed.

## D. Induvidual practise

#### Induvidual practise

Using the Azure-CLI, follow the instructions from the following [tutorial](https://learn.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api) and deploy a WebApp to Azure to ensure that you have everything setup correctly.  
No need to use the sample repository https://github.com/Azure-Samples/dotnet-core-api, a normal default Weather API works fine, skipping the CORS section of the tutorial

- With everything up and running, delete the now created resource group: `az group delete --name myResourceGroup`

With this done. You are free to use VS Code, Rider, Azure Portal or any other you see fit onward for managing your toold.

## E. Lab Instructions

### Day 1

#### Backend

- Create a WebAPI application, to keep things simple
- After you have created this empty application, immediatly move it to the cloud!
- Create an Azure SQL database ([free tier](https://learn.microsoft.com/en-us/azure/azure-sql/database/free-offer?view=azuresql)) with a name such as CanineDB, this will be the main database used to store all the data about dogs. make sure you pick the correct free one!
- Connect the WebAPI to the newly created database and setup a Dog entity with fitting fields (name, birth-year etc.). The dog shelter is trusting you with the logic on what fields should be added!
  - This will require allowing the WebApp to talk to the DB in Azure
- Setup a controller so that you can GET, POST and DELETE dogs from the database.

#### Frontend

- In the same project, setup a new React project using the `npm create vite@latest my-vue-app -- --template react-ts` command
- Immediately put the React app in the cloud via your repo, using [GitHub Actions](https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions)
  - [This page](https://learn.microsoft.com/en-us/azure/static-web-apps/build-configuration?tabs=github-actions) can be of use to setup everything correctly.
  - Setup so that the Back-End is also setup for GitHub Actions, with [mono-repo support](https://learn.microsoft.com/en-us/azure/static-web-apps/build-configuration?tabs=github-actions#monorepo-support)
- Connect the front-end it to the WebAPI, remember CORS and Firewalls!

### Day 2

- Create a blob container for your photos and write the boilerplate code needed to upload the photos to it.
  - You might find this [tutorial helpful](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images)
- Setup styling to display your beautiful dogs!

## Tips

- Since Azure is a service, the goal of today is to familiarize yourself with the environment. See if you can get [GitHub Actions](https://learn.microsoft.com/en-us/azure/app-service/deploy-github-actions?tabs=applevel%2Caspnetcore#set-up-a-github-actions-workflow-manually) working for both your front-end and back-end!

- See if you can deploy your HackDay project! Your front-end can be deployed either on GitHub pages or as a [Static Web App](https://learn.microsoft.com/en-us/azure/static-web-apps/overview) on Azure. You just need to remember to build the SPA (npm run build) instead of just running it in dev mode.

Good luck and have fun!
