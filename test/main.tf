
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
  agents = []
}

provider "k3s" {
  # Configuration options
  k3s_version = "v1.32.3"
}

resource "k3s_cluster" "smarthome" {
  name = "smarthome"
}

resource "k3s_sandbox" "sandbox" {
  cluster_id = "0"
}

resource "k3s_server" "master" {
  cluster_id = "0" # k3s_cluster.smarthome.id
  name       = local.master.name
  ssh = {
    host       = local.master.ip
    port       = 22
    username   = local.master.username
    password   = local.master.password
  }
}

output "cluster_id" {
  value = k3s_cluster.smarthome.id
}

output "master_token" {
  value = k3s_server.master.token
}

output "master_url" {
  value = k3s_server.master.url
}
