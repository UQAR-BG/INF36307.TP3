#!/bin/bash
set -xe

if [ "$1" = "-b" ] || [ "$1" = "--build" ] ; then
    docker build -t user-worker-service ./service/
fi

kubectl apply -f ./server/

echo "Waiting for cluster to start ..."
sleep 5s

kubectl apply -f ./service/