#!/usr/bin/env bash

tag=$(
  tr -dc A-Za-z0-9 </dev/urandom | head -c 13
  echo ''
)

docker build -f Dockerfile -t dines97/prober2:${tag} .

docker push dines97/prober:${tag}

kubectl apply -f ./Kubernetes/10-deploym

kubectl apply -f ./Kubernetes
