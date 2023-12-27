#!/bin/bash

# Log in to Azure
echo "Logging in to Azure..."
az login

# Create Resource Group
echo "Creating Resource Group..."
az group create -l EastUS2 -n eShop-Solution-rg

# Deploy template with in-line parameters
echo "Deploying Azure Resource..."
az deployment group create -g eShop-Solution-rg --template-uri https://github.com/Azure/AKS-Construction/releases/download/0.10.3/main.json --parameters \
    resourceName=eShop-AKS-Cluster \
    upgradeChannel=stable \
    SystemPoolType=Standard \
    agentCountMax=20 \
    osDiskType=Managed \
    osDiskSizeGB=32 \
    registries_sku=Premium \
    acrPushRolePrincipalId=$(az ad signed-in-user show --query id --out tsv) \
    omsagent=true \
    retentionInDays=30 \
    maxPods=238 \
    ingressApplicationGateway=true

# Create Azure Active Directory (Azure AD) Application and Service Principal
echo "Creating Azure AD Application and Service Principal..."
app=($(az ad app create --display-name eShopAPI --query "[appId,id]" -o tsv | tr ' ' "\n"))
spId=$(az ad sp create --id ${app[0]} --query id -o tsv)
subId=$(az account show --query id -o tsv)

# Assign the 'Owner' role to the Service Principal
echo "Assigning 'Owner' role to the Service Principal..."
az role assignment create --role owner --assignee-object-id $spId --assignee-principal-type ServicePrincipal --scope /subscriptions/$subId/resourceGroups/eShop-Solution-rg

# Create a new federated identity credential
echo "Creating a new federated identity credential..."
az rest --method POST --uri "https://graph.microsoft.com/beta/applications/${app[1]}/federatedIdentityCredentials" --body "{\"name\":\"eShopAPI-main-gh\",\"issuer\":\"https://token.actions.githubusercontent.com\",\"subject\":\"repo:Evilazaro/eShopAPI:ref:refs/heads/main\",\"description\":\"Access to branch main\",\"audiences\":[\"api://AzureADTokenExchange\"]}"

# Set Secrets using GitHub CLI (gh)
echo "Setting Secrets in GitHub repository..."
gh secret set --repo https://github.com/Evilazaro/eShopAPI AZURE_CLIENT_ID -b ${app[0]}
gh secret set --repo https://github.com/Evilazaro/eShopAPI AZURE_TENANT_ID -b $(az account show --query tenantId -o tsv)
gh secret set --repo https://github.com/Evilazaro/eShopAPI AZURE_SUBSCRIPTION_ID -b $subId
gh secret set --repo https://github.com/Evilazaro/eShopAPI USER_OBJECT_ID -b $spId

# Get credentials for your new AKS cluster & login (interactive)
echo "Getting AKS credentials and logging in..."
az aks get-credentials -g eShop-Solution-rg -n aks-eShop-AKS-Cluster
kubectl get nodes

echo "Script execution complete."
