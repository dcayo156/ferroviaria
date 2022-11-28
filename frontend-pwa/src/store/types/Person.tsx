import { ITag } from "./Tag"
export enum Gender {
    Femenino = "Femenino",
    Masculino = "Masculino",
    Personalizado = "Personalizado"
  }
export interface IAddress{
  street:string
  city: number
  state: number
  country: number
  zipCode: string
  latitude: number
  longitude: number
  description: string
}

export interface IMail{
  email:string
  emailDescription: string
}
export interface IPhone{
  phoneNumber:string
  phoneDescription:string
}
export interface ITagInfo{
  name:string
  description:string
  tagCategoryDescription?:string|null
}
export interface IPerson {
    id: string
    firstName: string|undefined
    secondName: string|undefined
    lastName: string|undefined
    birthDate: string
    gender: Gender
    pronounPreference?: string
    nationalId?:string
    addresses: IAddress[]
    mails: IMail[]
    phones:IPhone[]
    tags:ITagInfo[],
    communicationChannels?: ICommunicationChannels[]
}
export interface IPersonWithTag {
  person:IPerson,
  tags:ITag[]
}

// Person with single email and phone
export interface IPersonSimple {
  id?: string
  firstName: string|undefined
  secondName: string|undefined
  lastName: string|undefined
  birthDate?: string
  gender?: Gender
  pronounPreference?: string
  nationalId?:string
  addresses?: IAddress[]
  email?: string|undefined
  phone?:string|undefined
  tags?:ITagInfo[]
}

export interface IPersonWithTagSimple {
  person:IPersonSimple,
  tags:ITag[]
}

export interface ICommunicationChannels{
  email:string,
  emailDescription: string,
  phoneNumber:string,
  phoneDescription:string
}
