using Pulumi;
using Pulumi.AzureNative.App;
using Pulumi.AzureNative.App.Inputs;
using Pulumi.AzureNative.ContainerRegistry;
using ContainerRegistryInputs = Pulumi.AzureNative.ContainerRegistry.Inputs;
using Pulumi.AzureNative.ManagedIdentity;
using Pulumi.AzureNative.Resources;
using ManagedServiceIdentityType = Pulumi.AzureNative.App.ManagedServiceIdentityType;



return await Pulumi.Deployment.RunAsync(() =>
{
  string region = "norwayeast";
  string shortRegion = "noe";
  string appName = "bankapp";


  // Create an Azure Resource Group
  var resourceGroup = new ResourceGroup($"rg-{shortRegion}-{appName}", new ResourceGroupArgs
  {
    ResourceGroupName = $"rg-{shortRegion}-{appName}",
    Location = region
  });

  var appUserIdentity = new UserAssignedIdentity($"uai-{shortRegion}-{appName}", new UserAssignedIdentityArgs
  {
    ResourceGroupName = resourceGroup.Name,
    Location = region
  });

  var acr = new Registry($"acr{shortRegion}{appName}", new RegistryArgs
  {
    ResourceGroupName = resourceGroup.Name,
    Location = resourceGroup.Location,
    Sku = new ContainerRegistryInputs.SkuArgs { Name = "Basic" },
  });

  var containerEnv = new ManagedEnvironment($"cae-{shortRegion}-{appName}", new ManagedEnvironmentArgs
  {
    ResourceGroupName = resourceGroup.Name,
    Location = resourceGroup.Location,
  });

  var containerApp = new ContainerApp($"ca-{shortRegion}-{appName}", new ContainerAppArgs
  {
    ResourceGroupName = resourceGroup.Name,
    Location = resourceGroup.Location,
    EnvironmentId = containerEnv.Id,
    Configuration = new ConfigurationArgs
    {
      Registries = [
        new RegistryCredentialsArgs {
          Server = acr.LoginServer,
          Identity = appUserIdentity.PrincipalId,
        }
      ],
    },
    Identity = new ManagedServiceIdentityArgs
    {
      Type = ManagedServiceIdentityType.UserAssigned,
      UserAssignedIdentities = { { appUserIdentity.Id } }
    },
    Template = new TemplateArgs
    {
      Containers = {
         new ContainerArgs
         {
           Name = "bankapp",
           Image = $"{acr.LoginServer}/bankapp:latest",
           Probes = {
             new ContainerAppProbeArgs
             {
                Type = Type.Readiness,
                HttpGet = new ContainerAppProbeHttpGetArgs
                {
                  Path = "/health",
                  Port = 80
                },
                InitialDelaySeconds = 5,
                PeriodSeconds = 10,
                FailureThreshold = 3
             },
           }
         }
       }
    }
  });


});
