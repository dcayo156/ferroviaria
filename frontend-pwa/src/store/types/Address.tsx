export interface IAddressPerson {
  latitude: number;
  longitude: number;
  description: string;
  personId: string;
  firstName: string;
  secondName: string;
  lastName: string;
  name: string;
}

export interface IAddressRequest {
  latitudeFrom: number;
  longitudeFrom: number;
  latitudeTo: number;
  longitudeTo: number;
}

export interface IAddressLocation {
  lat: number;
  lng: number;
}
