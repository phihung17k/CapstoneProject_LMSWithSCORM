#!/bin/bash
dotnet new tool-manifest --force
dotnet tool install dotnet-format
dotnet tool install Husky
dotnet husky install