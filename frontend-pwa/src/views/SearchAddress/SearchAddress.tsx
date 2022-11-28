import React, { useEffect, useState } from "react";
import { Container, Grid, Paper } from "@mui/material";
import { ListAddress } from "./ListAddress";
import MyMap from "./SearchMap";
import { AutocompleteProps, useJsApiLoader } from "@react-google-maps/api";
import SelectLocation from "./SelectLocation";
import { toast } from "react-toastify";
import { useGetAddressByAreaQuery } from "../../store/services/Address";
import { IAddressPerson, IAddressRequest } from "../../store/types/Address";
import MainCard from "../../components/cards/MainCard";

const libraries: (
  | "places"
  | "drawing"
  | "geometry"
  | "localContext"
  | "visualization"
)[] = ["places"];
const SearchAddress = () => {
  const [selectedPerson, setSelectedPerson] = useState<IAddressPerson | null>(
    null
  );
  //TODO reemplaze people con informacion de la API
  const [people, setPeople] = useState<any[]>([]);
  const [center, setCenter] = React.useState<
    google.maps.LatLng | google.maps.LatLngLiteral | undefined
  >({
    lat: -34.63714351232913,
    lng: -58.7029105423287,
  });
  const [zoom, setZoom] = React.useState<number>(15);

  const [googleMap, setGoogleMap] = React.useState<any>(null);

  const [cardinalsPoints, setcardinalsPoints] = React.useState<IAddressRequest>(
    {
      latitudeFrom: 0,
      longitudeFrom: 0,
      latitudeTo: 0,
      longitudeTo: 0,
    }
  );
  const { data, error, isLoading } = useGetAddressByAreaQuery(cardinalsPoints, {
    skip: cardinalsPoints.longitudeFrom === 0 ? true : false,
  });
  const ConfigCardinalPoints = (map: any) => {
    if (map && map.getBounds()) {
      let ne = map!.getBounds().getNorthEast();
      let sw = map!.getBounds().getSouthWest();
      const request = {
        latitudeFrom: ne.lat(),
        longitudeFrom: sw.lng(),
        latitudeTo: sw.lat(),
        longitudeTo: ne.lng(),
      };
      setcardinalsPoints(request);
    } else {
      const request = {
        latitudeFrom: -34.629705360129776,
        latitudeTo: -34.64735975191487,
        longitudeFrom: -58.71186668733232,
        longitudeTo: -58.69040901521318,
      };
      setcardinalsPoints(request);
    }
  };

  const onDrag = () => {
    console.log("ondrasg", googleMap);
    if (!googleMap) return;
    ConfigCardinalPoints(googleMap);
  };

  React.useEffect(() => {
    if (data) {
      setPeople(data);
    }
  }, [data]);

  React.useEffect(() => {
    if (selectedPerson) {
      setCenter({
        lat: selectedPerson.latitude,
        lng: selectedPerson.longitude,
      });
    }
  }, [selectedPerson]);

  const [isLoaded, setIsLoaded] = React.useState<boolean>(false);
  const [count, setCount] = React.useState(0)
  const getIsLoaded = (load: boolean,) => {
    console.group("contando: " + count)
    console.log(load)
    console.groupEnd();
    setCount(count + 1)
    setIsLoaded(load)

  }
  const getMap = (m: any) => {
    setGoogleMap(m)
    ConfigCardinalPoints(m);
  }
  return (
    <MainCard title="Buscar Personas" secondary={undefined} >
      <Grid container spacing={3}>
        <Grid item xs={12} sm={12} md={12} lg={4}>
            <Grid container spacing={1} direction="column">
              <Grid item sx={{}}>
                <ListAddress
                  people={people}
                  getSelectionPerson={(person: any) => {
                    setSelectedPerson(person);
                  }}
                />
              </Grid>
            </Grid>
        </Grid>
        <Grid item xs={12} sm={12} md={12} lg={8}>
          <MyMap
            zoom={zoom}
            people={people}
            selectedPerson={selectedPerson}
            onDrag={onDrag}
            centerTo={center}
            isLoaded={isLoaded}
            getIsLoaded={getIsLoaded}
            googleMap={googleMap}
            getMap={getMap}
          />
        </Grid>
      </Grid>
    </MainCard>
  );
};

export default SearchAddress;