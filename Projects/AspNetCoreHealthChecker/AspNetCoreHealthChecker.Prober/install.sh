#!/usr/bin/env bash

kubectl apply -f ./Kubernetes/10-deployment.yaml -f ./Kubernetes/01-sa.yaml

kubectl scale --replicas=0 deployment/prober
kubectl scale --replicas=1 deployment/prober
