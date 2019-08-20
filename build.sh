#!/bin/bash

docker build . -t gcr.io/thomas-default/microurl:latest
docker push gcr.io/thomas-default/microurl:latest
