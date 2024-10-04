#!/bin/sh

dotnet build -t:Run -f:net9.0-ios -c Release -r ios-arm64 -p:RuntimeIdentifier=ios-arm64 -p:_DeviceName=00008110-001E30641AE1801E
