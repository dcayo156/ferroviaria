import React from "react";
import { GoogleMap, InfoWindow, Marker } from "@react-google-maps/api";
import { useGetAddressByAreaQuery } from "../../store/services/Address";
import IconMarker from "../../assets/img/ubicacion.png";
import { IAddressPerson, IAddressRequest } from "../../store/types/Address";
import Map from "../../components/Map";

interface MyMapProps {
  people: any[];
  selectedPerson: IAddressPerson | null;
  zoom: number;
  onDrag: (() => void | Promise<void>) | undefined;
  isLoaded: boolean;
  centerTo: google.maps.LatLng | google.maps.LatLngLiteral | undefined;
  googleMap: any;
  getIsLoaded: (isload: boolean) => void;
  getMap: (map: any) => void;
}

const MyMap: React.FunctionComponent<MyMapProps> = ({
  people,
  selectedPerson,
  isLoaded,
  getIsLoaded,
  zoom,
  onDrag,
  getMap,
  centerTo,
}) => {
  return (
    <Map
      getMap={getMap}
      getIsLoaded={getIsLoaded}
      onDragEnd={onDrag}
      onZoomChanged={onDrag}
      centerTo={centerTo}
    >
      {isLoaded &&
        people &&
        people!.map((person) => {
          return selectedPerson != null &&
            selectedPerson.personId === person.personId ? (
            <Marker
              key={person.personId}
              position={{
                lat: person.latitude,
                lng: person.longitude,
              }}
              icon={{
                url: IconMarker,
                anchor: new window.google.maps.Point(25, 45),
                scaledSize: new window.google.maps.Size(47, 47),
              }}
            />
          ) : (
            <Marker
              key={person.personId}
              position={{
                lat: person.latitude,
                lng: person.longitude,
              }}
            />
          );
        })}
    </Map>
  );
};

export default MyMap;
