# Smooth Operator

The aim of this project is to show an example of a Kubernetes Operator developed through the KubeOps library.

It's a Kubernetes operator that watches over the `sites.intre.com` CustomResource

Reference:

    https://buehler.github.io/dotnet-operator-sdk/src/KubeOps.Operator/README.html


## How to generate the deploy manifests

The KubeOps Cli automatically generates the manifest that you need in order to deploy and use the Operator.

    dotnet new tool-manifest
    dotnet tool install KubeOps.Cli
    dotnet kubeops generate op smoothoperator --out ./deploy


## How to run

- Generate the deploy manifests
- Build the Dockerfile under ./deploy folder and push it somewhere (over the rainbows)
- Deploy the manifests under the ./deploy folder in a kubernetes cluster

