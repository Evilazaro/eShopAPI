#!/bin/bash

# Log in to Azure
echo "Logging in to Azure..."
az login

# Create Resource Group
echo "Creating Resource Group..."
az group create -l EastUS2 -n eShop-rg

# Deploy template with in-line parameters
echo "Deploying Azure Resource..."
az deployment group create -g eShop-rg --template-uri https://raw.githubusercontent.com/Evilazaro/eShopAPI/main/src/eShopAPIOnContainers/eShopOnKubernetes/eShopAPI/deploy/AKS/AzureResources/main.json --parameters \
    resourceName=eShop \
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

clear
echo "AKS has been deployed successfuly"
echo ""
echo ""
echo ""
echo ""

# Building docker image
echo "Building Docker Image"
az acr login --name creshop
docker-compose -f ../../../docker-compose.yml build
docker push creshop.azurecr.io/eshop/eshop-api:${TIMESTAMP}

clear
echo "Docker image has been pushed to the ACR Repository"
echo ""
echo ""
echo ""
echo ""
# Get credentials for your new AKS cluster & login (interactive)
echo "Getting AKS credentials and logging in..."
az aks get-credentials -g eShop-rg -n aks-eShop
kubectl get nodes

clear
echo "Deploying eShopAPI to Azure Kubernetes Services"
kubectl apply -f ../SharedResources/. -f ../.

echo "Script execution complete."
