export interface ICreateRelationshipType {
   items:IRelationshipType[]
  }

  export interface IRelationshipType {
    id: string|null,
    relationshipTypeRequiredID: string|null,
    femaleDescription: string,
    maleDescription: string,
    neutralDescription: string
  }

  export interface IRelationshipTypesGrouped{
    id:string,
    idRelationType1:string,
    idRelationType2:string,
    relationShipName1:string,
    relationShipName2:string 
  }