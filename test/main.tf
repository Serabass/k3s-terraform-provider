
terraform {
  required_providers {
    k3s = {
      source  = "example.com/serabass/k3s"
      version = "~> 1.0.0"
    }
  }
}

locals {
  master = {
    name     = "master"
    ip       = "192.168.88.15"
    username = "a"
    password = "a"
  }
  agents = [
    {
      name     = "lenovo"
      ip       = "192.168.88.11"
      username = "a"
      password = "a"
    }
  ]
}

provider "k3s" {
  # Configuration options
  k3s_version = "v1.32.3"
}

resource "k3s_sandbox" "sandbox" {
  cluster_id = "0"
}

resource "k3s_server" "master" {
  name = local.master.name

  ssh = {
    host     = local.master.ip
    port     = 22
    username = local.master.username
    password = local.master.password
  }
}

# resource "k3s_agent" "agent" {
#   count = length(local.agents)
# 
#   name  = local.agents[count.index].name
#   token = k3s_server.master.token
#   url   = k3s_server.master.url
# 
#   ssh = {
#     host     = local.agents[count.index].ip
#     port     = 22
#     username = local.agents[count.index].username
#     password = local.agents[count.index].password
#   }
# }

output "master_token" {
  value = k3s_server.master.token
}

output "master_url" {
  value = k3s_server.master.url
}

output "master_kube_config" {
  value = k3s_server.master.kube_config
}
