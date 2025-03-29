
terraform {
  required_providers {
    k3sprovider = {
      source = "example.com/serabass/k3sprovider"
      version = "~> 1.0.0"
    }
  }
}

provider "k3sprovider" {
  # Configuration options
}
