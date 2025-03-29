
terraform {
  required_providers {
    k3s = {
      source = "example.com/serabass/k3s"
      version = "~> 1.0.0"
    }
  }
}

provider "k3s" {
  # Configuration options
}
