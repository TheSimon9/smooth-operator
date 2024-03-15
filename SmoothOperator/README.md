Step 1: Creare il progetto e seguire 

    https://buehler.github.io/dotnet-operator-sdk/src/KubeOps.Operator/README.html

Step 2: Creare robe che servono per operatore sotto cart /deploy 

    dotnet new tool-manifest
    dotnet tool install KubeOps.Cli
    dotnet kubeops generate op smoothoperator --out ./deploy

Step 3: Buildare il dockerfile creato e caricarlo da qualche parte

    docker build -t smoothoperator:latest .
    docker tag smoothoperator:latest rg.fr-par.scw.cloud/namespace-sleepy-matsumoto/smoothoperator:latest
    docker push rg.fr-par.scw.cloud/namespace-sleepy-matsumoto/smoothoperator:latest


Step 4: Deploy

    k apply -k deploy