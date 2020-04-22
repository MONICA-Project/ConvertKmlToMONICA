# Monica ConvertKmlToMONICA
<!-- Short description of the project. -->

ConvertKmlToMONICA
ConvertKmlToMONICA is a small utility to convert KML files into MONICA POIs (Points Of Interest)

The ConvertKmlToMONICA is based on the following technologies:
* REST API of COP.API

The ConvertKmlToMONICA provides the following main functionalities:
* Converting a KML file with Placemarks and Polygons to MONICA POIs and zones



The COPUpdater is implemented in ASP.NET Core 2.1 and is a simple command line based tool.

<!-- A teaser figure may be added here. It is best to keep the figure small (<500KB) and in the same repo -->

## Getting Started
The COP.API is developed in Visual Studio using Dotnet Core 2.1.

The easiest way to build it is to clone the repository using Visual Studio 2017 or higher and then build the software or to use the DotNet Core 2.1 SDK.
The KML objects should have the name as The POI name and description should be the type of the POI. If more info is needed please change the source accordingly.

To run the latest version of ConvertKmlToMONICA:
```bash
ConvertKmlToMONICA <cop endpoint> <kml file>
```


## Development
To start development it is enough to clone the repository and then build it either using Visual Studio or Dotnet Core SDK to build and run the API.


### Prerequisite

* Dotnet Core SDK 2.1 available [here](https://dotnet.microsoft.com/download/dotnet-core/2.1)
    - Or use Visual Studio 2017 or higher



### Build

```bash
dotnet build
```

### Endpoints exposed
None
## 
## Contributing
Contributions are welcome. 

Please fork, make your changes, and submit a pull request. For major changes, please open an issue first and discuss it with the other authors.

## Affiliation
![MONICA](https://github.com/MONICA-Project/template/raw/master/monica.png)  
This work is supported by the European Commission through the [MONICA H2020 PROJECT](https://www.monica-project.eu) under grant agreement No 732350.
