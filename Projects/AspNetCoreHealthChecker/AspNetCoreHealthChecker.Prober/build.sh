#!/usr/bin/env bash

eval "$(minikube docker-env)"

docker build -t prober -f ./Dockerfile --no-cache ..
# docker push dines97/prober
