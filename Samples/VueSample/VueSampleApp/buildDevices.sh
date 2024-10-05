#!/bin/sh

set -xe

dotnet build -t:Run -f:net9.0-ios -c Debug -r ios-arm64 -p:RuntimeIdentifier=ios-arm64 -p:_DeviceName=00008130-00022CD20A42001C
