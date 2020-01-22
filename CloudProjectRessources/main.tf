
terraform {
  required_version = ">= 0.12.0"
  required_providers {
    azurerm = ">=1.30.0"
  }
}

provider "azurerm" {
  version = ">=1.30.0"

  tenant_id       = var.tenant_id
  subscription_id = var.subscription_id
  client_id       = var.client_id
  client_secret   = var.client_secret
}

# Ressource Group
resource "azurerm_resource_group" "main" {
  name     = "TestForMyLife"
  location = "North Europe"
}

# SQL Server
resource "azurerm_sql_server" "sql_instance" {
  name                         = "esgi-4al1-group03-sql-server"
  resource_group_name          = "${azurerm_resource_group.main.name}"
  location                     = "${azurerm_resource_group.main.location}"
  version                      = "12.0"
  administrator_login          = "4dm1n157r470r"
  administrator_login_password = "4-v3ry-53cr37-p455w0rd"
  
}

# Firewall Rules for SQL Server
resource "azurerm_sql_firewall_rule" "azure_services_firewall_rule" {
  name                 = "Allow All Azure Service"
  resource_group_name  = "${azurerm_resource_group.main.name}" 
  server_name          = "${azurerm_sql_server.sql_instance.name}"
  start_ip_address     = "0.0.0.0"
  end_ip_address       = "0.0.0.0"
}

# Database
resource "azurerm_sql_database" "database" {
  name                = "TodoDB"
  resource_group_name = "${azurerm_resource_group.main.name}"
  location            = "${azurerm_resource_group.main.location}"
  server_name         = "${azurerm_sql_server.sql_instance.name}"
  import {
    storage_uri                  = "https://staesgi4al1cloudbacpac.blob.core.windows.net/tododb/TodoDB-2020-1-12-23-44.bacpac"
    storage_key                  = "IHjkgnlfprk0gW075+R67fzjZbdHiTo6FgLaLtTAu6VA/T+a1KUg2Bo+9ixghEEdigmJvw1382PhuZ8WGNyc1Q=="
    storage_key_type             = "StorageAccessKey"
    administrator_login          = "${azurerm_sql_server.sql_instance.administrator_login}"
    administrator_login_password = "${azurerm_sql_server.sql_instance.administrator_login_password}"
    authentication_type          = "SQL"
    operation_mode               = "Import"
  }
}

# WebApp Service Plan
resource "azurerm_app_service_plan" "webapp" {
  name                = "webapp_service_plan"
  location            = "${azurerm_resource_group.main.location}"
  resource_group_name = "${azurerm_resource_group.main.name}"

  sku {
    tier = "Standard"
    size = "S1"
  }
}

# WebApp
resource "azurerm_app_service" "webapp" {
  name                = "esgi-4al1-group03-cloud-project-api"
  location            = "${azurerm_resource_group.main.location}"
  resource_group_name = "${azurerm_resource_group.main.name}"
  app_service_plan_id = "${azurerm_app_service_plan.webapp.id}"
}

# Storage Account for Azure Function
resource "azurerm_storage_account" "azure_function" {
  name                     = "testafesgi03"
  resource_group_name      = "${azurerm_resource_group.main.name}"
  location                 = "${azurerm_resource_group.main.location}"
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

# Azure Function Service plan
resource "azurerm_app_service_plan" "azure_function" {
  name                = "azure-functions-test-service-plan"
  location            = "${azurerm_resource_group.main.location}"
  resource_group_name = "${azurerm_resource_group.main.name}"
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

# Azure Function
resource "azurerm_function_app" "azure_function" {
  name                      = "esgi-4al1-group03-cloud-project-azure-function"
  location                  = "${azurerm_resource_group.main.location}"
  resource_group_name       = "${azurerm_resource_group.main.name}"
  app_service_plan_id       = "${azurerm_app_service_plan.azure_function.id}"
  storage_connection_string = "${azurerm_storage_account.azure_function.primary_connection_string}"
}


# Docker Front
resource "azurerm_container_group" "docker_front" {
  name                     = "cloud_container_instance"
  resource_group_name      = "${azurerm_resource_group.main.name}"
  location                 = "${azurerm_resource_group.main.location}"
  ip_address_type          = "public"
  dns_name_label           = "esgi-4al1-group03-cloud-project-front"
  os_type                  = "linux"
  container {
    name                   = "container"
    image                  = "docker.io/effobless/esgi-cloud-project-image:1.0"
    port                   = "80"
    cpu                    = "0.5"
    memory                 = "1.5"
  }
}
