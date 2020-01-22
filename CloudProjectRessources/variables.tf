
#
# TERRAFORM AUTHENTICATION
# EXECUTE COMMAND : az account list
#  subscription_id = id
#  tenant_id = tennantId
# EXECUTE COMMAND : az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/SUBSCRIPTION_ID"
#  client_id = appId
#  client_secret = password

variable "tenant_id" {
  type = string
}
variable "subscription_id" {
  type = string
}
variable "client_id" {
  type = string
}
variable "client_secret" {
  type = string
}

