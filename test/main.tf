
terraform {
  required_providers {
    k3s = {
      source = "example.com/serabass/k3s"
      version = "~> 1.0.0"
    }
  }
}

locals {
  master = {
    name = "master"
    ip = "192.168.88.100"
    username = "a"
    password = "a"
  }
  agents = []
}

provider "k3s" {
  # Configuration options
}

resource "k3s_cluster" "test" {
  name = "test"
}

resource "k3s_server" "master" {
  cluster_id = k3s_cluster.test.id
  name = local.master.name
  host = local.master.ip
  port = 22
  username = local.master.username
  password = local.master.password
  version = "v1.32.3+k3s1"
}

output "cluster_id" {
  value = k3s_cluster.test.id
}
