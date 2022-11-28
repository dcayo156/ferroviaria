import * as React from 'react';
import { IAddress } from '../../../store/types/Person';
import Map from '../../../components/Map'
import { Marker } from '@react-google-maps/api';
import { IAddressLocation } from '../../../store/types/Address';


const center = {
    lat: -34.63714351232913,
    lng: -58.7029105423287,
  };


interface GeoCodeProps {
    location: IAddressLocation
    sendLocation: (info: IAddress) => void  
}

const GeoCode: React.FunctionComponent<GeoCodeProps> = ({location,sendLocation}) => {   

    const [marker, setMarker] = React.useState(center);

    React.useEffect(()=> {
        if (location.lat === 0) {return}
        setMarker(location)
    },[location])

    const onDblClickMap = (e: any) => {
        setMarker({
            lat: e.latLng.lat(),
            lng: e.latLng.lng()
        })
        
            const geocoder = new google.maps.Geocoder();
            geocoder.geocode({
                'location': {
                    lat: e.latLng.lat(),
                    lng: e.latLng.lng()
                }
            }, function (results, status) {
                if (status == "OK") {
                    let place=results![0];
                    var locality=place.address_components.find(a=>a.types.includes("locality")||a.types.includes("political"))
                    const street = place.address_components[1].short_name;
                    const address: IAddress = {
                        street: street,
                        city: 0,
                        state: 0,
                        country: 0,
                        zipCode: "none",
                        latitude: place.geometry.location.lat(),
                        longitude: place.geometry.location.lng(),
                        description: locality!.short_name
                    }
                    sendLocation(address);
                }
                else {

                }
            });
        
    }
    const [isLoaded,setIsLoaded]=React.useState(false);
    const onLoadMarker = (marker:any) => {
        
    }
    const LoadedStatus=(res:boolean)=>{
        setIsLoaded(res);
    }
    const actionBeforeFind=(centro: google.maps.LatLng )=>{
        const lat:number=centro!.lat();
        const lng:number=centro!.lng();
       
        setMarker({
            lat: lat,
            lng: lng
        })
        
            const geocoder = new google.maps.Geocoder();
            geocoder.geocode({
                'location': {
                    lat:lat,
                    lng:lng
                }
            }, function (results, status) {
                if (status == "OK") {
                    let place=results![0];
                    var locality=place.address_components.find(a=>a.types.includes("locality")||a.types.includes("political"))
                    const street = place.address_components[1].short_name;
                    const address: IAddress = {
                        street: street,
                        city: 0,
                        state: 0,
                        country: 0,
                        zipCode: "none",
                        latitude: place.geometry.location.lat(),
                        longitude: place.geometry.location.lng(),
                        description: locality!.short_name
                    }
                    sendLocation(address);
                }
                else {

                }
            });
        
    }
    return <Map 
            onClick={onDblClickMap} 
            actionBeforeFind={actionBeforeFind}
            getIsLoaded={LoadedStatus} 
            centerTo={marker}                     
            >
                {isLoaded && <Marker onLoad={onLoadMarker}
                    position={{
                        lat: marker.lat,
                        lng: marker.lng
                    }} />}
        </Map>;
}

export default GeoCode;
