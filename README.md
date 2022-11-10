# DevFreela
Projeto destinado a estudos entre os tópicos:

 - Desenvolvimento de APIs com ASP.NET
 - Arquitetura Limpa
 - EntityFramework e Dapper
 - Padrão CQRS
 - Padrão Repository
 - Validações de APIs
 - Autorização e Autenticação com JWT
 - Testes Unitários
 - Esteira com Azure Pipelines
 - Microsserviços e Mensageria
 
## Azure Pipelines <h2>
Um pequeno guia de como criar uma esteira simples no azure devops:

Ir em Azure Pipelines

build -->

New pipeline -> Azure Repos Git -> Projeto -> Starter pipeline -> Show assistant -> .NET Core -> Command (build) -> Add 

test -->
New pipeline -> Azure Repos Git -> Projeto -> Starter pipeline -> Show assistant -> .NET Core -> Command (test) -> Add 

publish -->
New pipeline -> Azure Repos Git -> Projeto -> Starter pipeline -> Show assistant -> .NET Core -> Command (publish) -> Add 

Para realizar deploy automático irá ter que configurar um WebApp, configurando, basta adicionar ele -> Show assistant -> Azure App Service Deploy

O código YAML irá ficar assim:

```yaml
# Starter pipeline
# Start with a minimal pipeline that you can customize to build and deploy your code.
# Add steps that build, run tests, deploy, and more:
# https://aka.ms/yaml

trigger:
- master
- develop

pool:
  vmImage: ubuntu-latest

steps:
- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
  displayName: 'Build'
  
- task: DotNetCoreCLI@2
  inputs:
    command: 'test'
  displayName: 'Test'
  
  - task: DotNetCoreCLI@2
  inputs:
    command: 'publish'
    publishWebProjects: true
  
- task: AzureRmWebAppDeployment@4
  inputs:
    ConnectionType: 'AzureRM'
    azureSubscription: 'Azure for Students(1)(censurado rs)'
    appType: 'webAppLinux'
    WebAppName: 'abedevfreela'
    packageForLinux: '$(System.DefaultWorkingDirectory)/**/*.zip'
    RuntimeStack: 'DOTNETCORE|6.0'
```
