#!/bin/bash

az login
gh auth login

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