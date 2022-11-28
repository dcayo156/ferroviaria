export interface IRelationshipByPerson {
  id: string;
  personId: string;
  firstName: string;
  secondName: string;
  lastName: string;
  relationshipTypeDescription: string;
  relationshipTypeDescriptionId: string;
}

export interface IRelationShip {
  personId: string;
  relation: {
    personID: string;
    relationshipTypeID: string;
    isNeutral: boolean;
  };
}

export interface IRelationShipTypes {
  id: string;
  relationshipName: string;
}
