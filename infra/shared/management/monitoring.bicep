@description('Azure region where monitoring resources (Log Analytics, Application Insights, Storage) are deployed.')
param location string

@description('Standard resource tags applied for governance, cost management, and operational tracking.')
param tags object

@description('Storage Account used for retention, archival, and export of monitoring and diagnostic data (hot access tier).')
resource storageAccount 'Microsoft.Storage/storageAccounts@2025-01-01' = {
  name: 'apimaccmon${uniqueString(resourceGroup().id, resourceGroup().name)}'
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  identity: {
    type: 'SystemAssigned'
  }
  tags: tags
  properties: {
    accessTier: 'Hot'
  }
}

@description('Diagnostic Settings capturing storage account metrics and forwarding to Log Analytics and archival Storage.')
resource storageAccountDiag 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: '${storageAccount.name}-diagnostics'
  scope: storageAccount
  properties: {
    workspaceId: logAnalytics.id
    storageAccountId: storageAccount.id
    metrics: [
      {
        category: 'AllMetrics'
        enabled: true
      }
    ]
  }
}

@description('Log Analytics workspace aggregating platform, network, and application telemetry for APIM Landing Zone analytics.')
resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2025-02-01' = {
  name: 'eshop-${uniqueString(resourceGroup().id, resourceGroup().name)}-law'
  location: location
  tags: tags
  identity: {
    type: 'SystemAssigned'
  }
}

@description('Diagnostic Settings enabling workspace self-monitoring (logs + metrics) to Log Analytics and Storage for auditing.')
resource logAnalyticsDiag 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: '${logAnalytics.name}-diagnostics'
  scope: logAnalytics
  properties: {
    workspaceId: logAnalytics.id
    storageAccountId: storageAccount.id
    logs: [
      {
        categoryGroup: 'AllLogs'
        enabled: true
      }
    ]
    metrics: [
      {
        category: 'AllMetrics'
        enabled: true
      }
    ]
  }
}

@description('Application Insights component providing performance monitoring, distributed tracing, and dependency analysis integrated with Log Analytics.')
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'eshop-${uniqueString(resourceGroup().id, resourceGroup().name)}-appi'
  location: location
  kind: 'web'
  tags: tags
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalytics.id
  }
}

@description('Diagnostic Settings exporting comprehensive Application Insights telemetry categories and metrics to Log Analytics and Storage.')
resource appInsightsDiag 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  scope: appInsights
  name: '${appInsights.name}-diagnostics'
  properties: {
    workspaceId: logAnalytics.id
    storageAccountId: storageAccount.id
    logs: [
      {
        category: 'AppRequests'
        enabled: true
      }
      {
        category: 'AppDependencies'
        enabled: true
      }
      {
        category: 'AppExceptions'
        enabled: true
      }
      {
        category: 'AppPageViews'
        enabled: true
      }
      {
        category: 'AppPerformanceCounters'
        enabled: true
      }
      {
        category: 'AppAvailabilityResults'
        enabled: true
      }
      {
        category: 'AppTraces'
        enabled: true
      }
      {
        category: 'AppEvents'
        enabled: true
      }
      {
        category: 'AppMetrics'
        enabled: true
      }
      {
        category: 'AppBrowserTimings'
        enabled: true
      }
      {
        category: 'AppSystemEvents'
        enabled: true
      }
      {
        category: 'OTelResources'
        enabled: true
      }
    ]
    metrics: [
      {
        category: 'AllMetrics'
        enabled: true
      }
    ]
  }
}

