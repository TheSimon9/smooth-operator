Step 1: Creare il progetto e seguire 

    https://buehler.github.io/dotnet-operator-sdk/src/KubeOps.Operator/README.html

Step 2: Creare robe che servono per operatore sotto cart /deploy 

    dotnet kubeops generate op poltronesofa --out ./deploy

Step 3: Buildare il dockerfile creato e caricarlo da qualche parte

    docker build -t poltronesofa:latest .
    docker tag poltronesofa:latest rg.fr-par.scw.cloud/namespace-sleepy-matsumoto/poltronesofa:latest
    docker push rg.fr-par.scw.cloud/namespace-sleepy-matsumoto/poltronesofa:latest