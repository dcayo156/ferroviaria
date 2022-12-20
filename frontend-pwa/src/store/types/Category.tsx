import {ITag} from './delete-Tag'
export interface ITagCategory{
    id:string
    description:string
    tags:ITag[]
}
export interface ICategoryData{
    name:string;
    id:string;
    tags:ITagItem[];
   }
   
  export interface ITagItem{
    name: string;
    id: string;
    numberOfPeople: number;
    selected:boolean;
   }
//Deprecate
export interface IRequestPeopleAddressByTag
{
    fromLatitude:number;
    fromLongitude:number;
    ToLatitude:number;
    ToLongitude:number;
    Tags: string[];
}
interface CardinalPoint
{
  latitud: number
  longitud: number
} 
export interface Category
{
  id: string
  tagIds: string[]
}
export interface IRequestPeopleAddressByTagV2
{
  from:CardinalPoint
  to:CardinalPoint
  categories?:Category[]
  fromLucene:boolean
}


export interface IResultPeopleAddressByTag
{
       /* name: string;
        id: string;
        categoryId: string;
        categoryName: string;
        addresses: [
          {*/
            id: string;
            latitude: number;
            longitude: number;
            street: string;
            personId: string;
            personName:string;
         /* }
        ];*/
}