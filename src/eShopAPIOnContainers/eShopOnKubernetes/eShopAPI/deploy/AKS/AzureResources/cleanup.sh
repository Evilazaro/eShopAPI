#!/bin/bash

# Step 1: Log in to Azure
echo "Step 1: Logging in to Azure..."
az login

# Step 2: Get the Azure AD application details
echo "Step 2: Retrieving Azure AD application details..."
appInfo=($(az ad app list --display-name eShopAPI --query '[[0].appId,[0].id]' -o tsv))

# Step 3: Delete federated identity credentials
echo "Step 3: Deleting federated identity credentials..."
credentialsId=$(az rest -m GET -u "https://graph.microsoft.com/beta/applications/${appInfo[1]}/federatedIdentityCredentials" --query "value[0].id" -o tsv)
az rest -m DELETE -u "https://graph.microsoft.com/beta/applications/${appInfo[1]}/federatedIdentityCredentials/$credentialsId"

# Step 4: Delete the Azure AD service principal
echo "Step 4: Deleting the Azure AD service principal..."
az ad sp delete --id ${appInfo[0]}

# Step 5: Script completed
echo "Step 5: Script completed successfully."
