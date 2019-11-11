# microurl

A personal url shortener and code snippet sharing tool

[![Run on Google Cloud](https://storage.googleapis.com/cloudrun/button.svg)](https://console.cloud.google.comcloudshell/editor?shellonly=true&cloudshell_image=gcr.io/cloudrun/button&cloudshell_git_repo=https://github.com/thomasgassmann/microurl)

## Overview

microurl is an url shortener and a code snippet sharing tool with an ASP.NET Core backend and an Angular frontend. It was designed to work with Google Cloud Run and stores all data in Google Cloud Datastore (though other data source might be supported too in the future).

To edit code snippets it currently uses [Monaco Editor](https://github.com/microsoft/monaco-editor) and in order to guarantee faster load times and a better user experience (especially on mobile), microurl can also be installed as a PWA.

## Configuration

1. [Setup Authentication](https://cloud.google.com/docs/authentication/production) for Google Cloud Datastore
2. Edit `src/MicroUrl.Web/appsettings.json` and update `Storage.Project` to your Google Cloud project.
3. Edit `src/MicroUrl.Web/appsettings.json` and update `MicroUrlSettings.AnalyticsId` with your Google Analytics Id.
4. Apply indicies using the `applyIndicies.sh` script

## Run microurl

1. `dotnet build`
2. [Configure microurl](#Configuration)
3. `dotnet run --project src/MicroUrl.Web`
4. microurl should now be running on `localhost:8080`

## License

MIT License

Copyright (c) 2019 Thomas Gassmann

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
