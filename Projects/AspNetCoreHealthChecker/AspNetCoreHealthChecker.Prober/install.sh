#!/usr/bin/env bash

kubectl apply -f ./Kubernetes/10-deployment.yaml \
              -f ./Kubernetes/01-sa.yaml \
              -f ./Kubernetes/clusterrole.yaml \
              -f ./Kubernetes/crb.yaml \
              -f ./Kubernetes/20-crd.yaml \

kubectl apply -f ./Kubernetes/rabbitmq-probe.yaml \
              -f ./Kubernetes/google-probe.yaml --validate=false 

kubectl scale --replicas=0 deployment/prober
kubectl scale --replicas=1 deployment/prober
