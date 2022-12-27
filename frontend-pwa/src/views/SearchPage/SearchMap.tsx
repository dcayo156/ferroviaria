import React, { useState } from "react";
import { InfoWindowF, Marker } from "@react-google-maps/api";
import Map from "../../components/Map";
import { IResultPeopleAddressByTag } from "../../store/types/delete-Category";
import { useGetFindPeopleByIdQuery } from "../../store/services/Person";
import { Button, CardActions, CardContent, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";

interface MyMapProps {
  people: any[];
  selectedPerson: IResultPeopleAddressByTag | null;
  zoom: number;
  onDrag: (() => void | Promise<void>) | undefined;
  isLoaded: boolean;
  centerTo: google.maps.LatLng | google.maps.LatLngLiteral | undefined;
  googleMap: any;
  getIsLoaded: (isload: boolean) => void;
  getMap: (map: any) => void;
  onSetSelectedPerson: (person: IResultPeopleAddressByTag | null) => void;
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
  onSetSelectedPerson,
}) => {
  const [personID, setPersonID] = useState<string | null>(null);
  const {
    data: personInfo
  } = useGetFindPeopleByIdQuery(personID, {
    skip: personID==null ? true : false,
  });
  const navigate=useNavigate();

  const handleClickMarker = (person: any) => {
    setPersonID(person.personId);
    onSetSelectedPerson(person);
    console.log("Llamada a FindPeopleByID");
  };

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
        people!.map((person,index) => {
          return (
            <Marker
              key={`${person.personId}_${index}`}
              position={{
                lat: person.latitude,
                lng: person.longitude,
              }}
              onClick={() => {
                handleClickMarker(person);
              }}
            >
              {selectedPerson != null &&
              selectedPerson.personId === person.personId &&
              personInfo != null ? (
                <InfoWindowF
                  position={{
                    lat: person.latitude,
                    lng: person.longitude,
                  }}
                  onCloseClick={() => {
                    onSetSelectedPerson(null);
                  }}
                >
                  <React.Fragment>
                    <CardContent>
                      <Typography variant="h5" component="div">
                        {`${personInfo!.firstName} ${personInfo!.secondName} ${personInfo!.lastName}`}
                      </Typography>
                      <Typography sx={{ mb: 1.5 }} color="text.secondary">
                        {person.street}
                      </Typography>
                      <Typography variant="body2">
                      {personInfo!.tags.map((t)=> t.name).join('|')}
                      </Typography>
                    </CardContent>
                    <CardActions>
                      <Button size="small" onClick={()=> navigate(`/person/${person.personId}/edit`)}>Mas informacion</Button> {/*TODO: Cambiar el navigate a una pagina de informacion de la persona */}
                    </CardActions>
                  </React.Fragment>
                </InfoWindowF>
              ) : null}
            </Marker>
          );
        })}
    </Map>
  );
};

export default MyMap;
