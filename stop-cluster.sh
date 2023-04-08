#!/bin/bash
set -xe

kubectl delete -f ./server/
kubectl delete -f ./service/