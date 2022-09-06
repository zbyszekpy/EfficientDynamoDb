#!/bin/bash

docker run -d -p 8000:8000  amazon/dynamodb-local -jar DynamoDBLocal.jar -inMemory -sharedDb
aws dynamodb create-table --cli-input-yaml file://test.yaml --endpoint http://localhost:8000
